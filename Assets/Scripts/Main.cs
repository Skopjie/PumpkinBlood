using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] BridgeConnection pythonConnection;
    Process cmd;

    void Start() {
        InitPowerShell();
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            QuitApliaction();
        }
    }


    void InitPowerShell() {
        cmd = new Process();
        cmd.StartInfo.FileName = @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe";

        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.Start();

        //comando activar entorno virtual
        #if UNITY_EDITOR
                cmd.StandardInput.WriteLine("cd C:\\Users\\Propietario\\OneDrive\\Escritorio\\ProyectosActuales\\python_Frontend_Games");
                //cmd.StandardInput.WriteLine("./BloodyPumpkin/Scripts/Activate");
                cmd.StandardInput.WriteLine("python head_direction.py");
        #endif
        #if UNITY_STANDALONE
                cmd.StandardInput.WriteLine("./BloodyPumpkin/Scripts/Activate");
                cmd.StandardInput.WriteLine("python head_direction.py");
                //cmd.StandardInput.WriteLine("tfod/Scripts/Python.exe program.py");
        #endif
    }

    private void OnApplicationQuit() {
        cmd.Close();
    }

    public void QuitApliaction() {
        cmd.Close();
        Application.Quit();
    }
}
