using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PatronPrefabData {
    public Vector3 initialPos;
    public PatronController patron;
}

public class PatronController : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float speedPatron = 5;

    [Header("Componentes")]
    public Transform plartformScale;

    public Transform objectPlatformParent;
    List<IObjectPlatform> objectPlatformList = new List<IObjectPlatform>();

    [SerializeField] bool isMoving = false;


    Vector3 globalForward;
    Vector3 globalForwardMovement;
    Vector3 forward = Vector3.forward;

    private void Start() {
        //speedPatron = GameManager.Instance.actualSpeedPlatform;
        GameManager.Instance.OnGameIsOver += StopMovement;
        GameManager.Instance.OnGameStart += StartMovement;

        GetAllObjectPlatform();
    }

    private void FixedUpdate() {
        if (isMoving) {
            // Calcula el vector de dirección global hacia adelante
            globalForward = Quaternion.Euler(0, transform.eulerAngles.y, 0) * forward;

            // Calcula el movimiento hacia adelante en coordenadas globales
            globalForwardMovement = globalForward * -speedPatron * Time.deltaTime;

            // Aplica el movimiento en coordenadas globales
            transform.position += globalForwardMovement;
        } 
    }

    public void SetEnablePatron(bool enable) {
        if (enable) EnablePatron();
        else DisablePatron();
    }

    public void EnablePatron() {
        gameObject.SetActive(true);
        if (GameManager.Instance.gameState == GameState.Game) StartMovement();
    }

    public void DisablePatron() {
        ResetAllObjectPlatform();
        gameObject.SetActive(false);
    }

    void StartMovement() {
        isMoving = true;
    }

    void StopMovement() {
        isMoving = false;
    }

    public bool IsActive() {
        return gameObject.activeSelf;
    }

    public void SetNewPosition(Vector3 newPosition) {
        transform.position = newPosition;
    }

    public void SetNewPositionZ(float newPosition) {
        transform.position = new Vector3(transform.position.x,transform.position.y, newPosition);
    }

    void ResetAllObjectPlatform() {
        foreach (IObjectPlatform obj in objectPlatformList) {
            obj.ResetObject();
        }
    }

    public void ActiveAllObjectPlatform() {
        foreach (IObjectPlatform obj in objectPlatformList) {
            obj.ActiveObject();
        }
    }

    void GetAllObjectPlatform() {
        foreach (Transform obj in objectPlatformParent) {
            IObjectPlatform objControl = obj.GetComponent<IObjectPlatform>();
            if (objControl != null) {
                objectPlatformList.Add(objControl);
            }
        }
    }
}
