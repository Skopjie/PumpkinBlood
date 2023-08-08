using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerRotation : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] BridgeConnection python;

    [SerializeField] float rotationSpeed = 100f;
    public float rotationSpeedY = 5f;
    [SerializeField] float maxRotationY = 35;

    private float t = 0f;

    [SerializeField] bool moveX = false;

    float angleX = 0;

    float angleY = 0;
    float angleYActual = 0;

    private void Start() {
        python.OnMessageReceived += MoveDirection;
    }

    void FixedUpdate()
    {
        if(GameManager.Instance.gameState == GameState.Game) {
            angleX += rotationSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(angleX, angleY, 0);
        }
    }

    public void MoveDirection(string direction) {
        float num = float.Parse(direction, CultureInfo.InvariantCulture);
        RotateY(num);
    }

    public float MapNumber(float inputNumber, float inputMin, float inputMax, float outputMin, float outputMax) {
        // Asegurarse de que el valor esté dentro del rango de entrada
        inputNumber = Mathf.Clamp(inputNumber, inputMin, inputMax);

        // Realizar el mapeo lineal del rango de entrada al rango de salida
        float inputRange = inputMax - inputMin;
        float outputRange = outputMax - outputMin;
        float mappedNumber = ((inputNumber - inputMin) / inputRange) * outputRange + outputMin;

        return mappedNumber;
    }

    void RotateY(float direction) {
        angleYActual = maxRotationY * direction;
        angleYActual = Mathf.Clamp(angleYActual, -maxRotationY, maxRotationY);
        angleY = angleYActual;
        //angleY = Mathf.Lerp(angleY, angleYActual, rotationSpeedY * Time.deltaTime);
    }
}
