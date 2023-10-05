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
    [SerializeField] bool activeMovement = false;

    Vector3 initialPos = Vector3.zero;

    float num;
    Vector3 movimiento;

    private void Start() {
        rgbd = GetComponent<Rigidbody>();
        python.OnMessageReceived += MoveDirection;

        initialPos = transform.localPosition;
    }

    public void SetActiveMovement(bool active) {
        rgbd.velocity = Vector3.zero;
        activeMovement = active;
    }

    public void MoveDirection(string direction) {
        if (activeMovement) {
            num = float.Parse(direction, CultureInfo.InvariantCulture);
            movimiento = new Vector3(num, 0f, 0f);
            rgbd.velocity = movimiento * speedMovement;
        }
    }

    public void ResetInitialPosition() {
        rgbd.velocity = Vector3.zero;
        transform.localPosition = initialPos;
    }
}
