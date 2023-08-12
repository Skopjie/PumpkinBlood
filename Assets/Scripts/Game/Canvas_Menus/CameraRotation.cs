using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] BridgeConnection python;

    [Header("Variables")]
    [SerializeField] float rotationSpeedZ = 5f;
    [SerializeField] float maxRotationZ = 35;

    [SerializeField] bool activeMovement = false;

    float angleZ = 0;
    float angleZActual = 0;

    Vector3 initialRot = Vector3.zero;

    private void Start() {
        python.OnMessageReceived += MoveDirection;

        initialRot = transform.eulerAngles;
    }

    void FixedUpdate() {
        if (activeMovement) {
            transform.eulerAngles = new Vector3(initialRot.x, initialRot.y, angleZ);
        }
    }

    public void MoveDirection(string direction) {
        if (activeMovement) {
            float num = float.Parse(direction, CultureInfo.InvariantCulture);
            RotateY(num);
        }
    }

    void RotateY(float direction) {
        angleZActual = maxRotationZ * direction;
        angleZActual = Mathf.Clamp(angleZActual, -maxRotationZ, maxRotationZ);
        angleZ = -angleZActual;
    }

    public void SetActiveMovement(bool active) {
        transform.eulerAngles = initialRot;
        activeMovement = active;
    }
}
