using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class BridgeConnection : MonoBehaviour
{
    private Stack<string> msgs = new Stack<string>();

    public Action<string> OnMessageReceived = delegate { };
    public Action OnConnected = delegate { };

    #region private members 	
    private TcpClient socketConnection;
    private Thread clientReceiveThread;
    #endregion

    [SerializeField] private string ip;
    [SerializeField] private int port;

    [SerializeField] private int maxTimeOuts;

    bool connected = false;

    private IEnumerator Start() {
        ConnectToTcpServer();
        yield return new WaitUntil(() => connected);
        OnConnected();
    }

    private void Update() {
        if (msgs.Count > 0) {
            OnMessageReceived(msgs.Pop());
        }
    }

    public IEnumerator RunWithDelay(int delay, Action action) {
        yield return new WaitForSeconds(delay);
        action();
    }

    private void ConnectToTcpServer() {
        try {
            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        } catch (Exception e) {
            socketConnection.Close();
            socketConnection.Dispose();
            Debug.Log("On client connect exception " + e);
        }
    }

    private void ListenForData() {
        var timeOuts = maxTimeOuts;
        connected = false;

        while (!connected && timeOuts > 0) {
            try {
                socketConnection = new TcpClient(ip, port);
                connected = true;

                Debug.Log("Connected");
                Byte[] bytes = new Byte[1024];

                while (socketConnection.Connected) {
                    // Get a stream object for reading 				
                    using (NetworkStream stream = socketConnection.GetStream()) {
                        int length;
                        // Read incomming stream into byte arrary. 					
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            // Convert byte array to string message. 						
                            string serverMessage = Encoding.UTF8.GetString(incommingData);
                            msgs.Push(serverMessage);
                        }
                        stream.Close();
                    }
                }
            } catch (SocketException socketException) {
                connected = false;
                Debug.Log("Socket exception: " + socketException);
            }

            timeOuts--;
            Debug.Log($"Timeouts: {timeOuts}");
            Thread.Sleep(5000);
        }

        CloseConnection();
    }

    public void SendMessageToServer(string data) {
        if (socketConnection == null || !socketConnection.Connected) {
            Debug.Log("Connection is closed");
            return;
        }
        try {
            // Get a stream object for writing. 			
            NetworkStream stream = socketConnection.GetStream();
            if (stream.CanWrite) {
                Debug.Log("SENT");
                // Convert string message to byte array.                 
                byte[] clientMessageAsByteArray = Encoding.UTF8.GetBytes(data);
                // Write byte array to socketConnection stream.                 
                stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
            }
        } catch (SocketException socketException) {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    private void CloseConnection() {

        if (socketConnection != null) {
            socketConnection.Client.Close();
            socketConnection.Client.Dispose();

            socketConnection.Close();
            socketConnection.Dispose();
            Debug.Log("closed");
        }

        if (clientReceiveThread.IsAlive) {
            clientReceiveThread.Abort();
        }

        socketConnection = null;
        clientReceiveThread = null;
    }

    private void OnDestroy() {
        CloseConnection();
        OnMessageReceived = delegate { };
    }
}
