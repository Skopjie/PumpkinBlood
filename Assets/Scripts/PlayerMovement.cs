using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
public class PlayerMovement : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] Rigidbody rgbd;
    [SerializeField] BridgeConnection python;

    [Header("Variables")]
    [SerializeField] float speedMovement = 1;

    private void Start() {
        rgbd = GetComponent<Rigidbody>();
        python.OnMessageReceived += MoveDirection;
    }

    public void MoveDirection(string direction) {
        if (GameManager.Instance.gameState == GameState.Game) {
            float num = float.Parse(direction, CultureInfo.InvariantCulture);
            Vector3 movimiento = new Vector3(num, 0f, 0f);
            rgbd.velocity = movimiento * speedMovement;
        }
    }

    //dir = Mathf.Round(dir * 100f) / 100f;
    //Vector3 movimiento = new Vector3(dir, 0f, 0f);
    //rgbd.velocity = movimiento * speedMovement;

}
