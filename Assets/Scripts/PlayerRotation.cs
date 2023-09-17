using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerRotation : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] BridgeConnection python;

    [Header("Variables")]

    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float rotationSpeedY = 5f;

    [SerializeField] float maxRotationY = 35;
    [SerializeField] bool activeMovement = false;

    float angleX = 0;
    float angleY = 0;
    float angleYActual = 0;

    Vector3 initialRot = Vector3.zero;

    private void Start() {
        python.OnMessageReceived += MoveDirection;

        initialRot = transform.eulerAngles;
    }

    void FixedUpdate()
    {
        if (activeMovement) {
            angleX += rotationSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(angleX, angleY, 0);
        }
    }

    public void MoveDirection(string direction) {
        if (activeMovement) {
            float num = float.Parse(direction, CultureInfo.InvariantCulture);
            RotateY(num);
        }
    }

    void RotateY(float direction) {
        angleYActual = maxRotationY * direction;
        angleYActual = Mathf.Clamp(angleYActual, -maxRotationY, maxRotationY);
        angleY = angleYActual;
    }

    public void SetActiveMovement(bool active) {
        transform.eulerAngles = new Vector3(initialRot.x, initialRot.y, initialRot.z);
        activeMovement = active;
    }
}
