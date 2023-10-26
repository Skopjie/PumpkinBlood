using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChickenUIAnim : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] Image chickenImage;
    [SerializeField] BridgeConnection python;

    [Header("Variables")]
    [SerializeField] float maxRotation = 15f;

    float angleX = 0;
    float angleY = 0;
    float angleYActual = 0;

    float num;

    private void Awake() {
        python.OnMessageReceived += RotateChicken;
    }

    void FixedUpdate() {
        transform.localEulerAngles = new Vector3(0, 0, -angleY);
    }

    public void RotateChicken(string direction) {
        num = float.Parse(direction, CultureInfo.InvariantCulture);
        RotateY(num);
    }

    void RotateY(float direction) {
        angleYActual = maxRotation * direction;
        angleYActual = Mathf.Clamp(angleYActual, -maxRotation, maxRotation);
        angleY = angleYActual;
    }

}
