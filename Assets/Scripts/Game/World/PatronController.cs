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

    public Transform ChickenParentTransform;
    [SerializeField] List<ChickenController> chickenControllers = new List<ChickenController>();

    [SerializeField] bool isMoving = false;

    private void Start() {
        GameManager.Instance.OnGameIsOver += StopMovement;
        GameManager.Instance.OnGameStart += StartMovement;

        GetAllChickens();
    }

    private void FixedUpdate() {
        if (isMoving) {
            // Calcula el vector de direcci�n global hacia adelante
            Vector3 globalForward = Quaternion.Euler(0, transform.eulerAngles.y, 0) * Vector3.forward;

            // Calcula el movimiento hacia adelante en coordenadas globales
            Vector3 globalForwardMovement = globalForward * -speedPatron * Time.deltaTime;

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
        ResetAllChickens();
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

    void ResetAllChickens() {
        foreach (ChickenController chicken in chickenControllers) {
            chicken.ResetChicken();
        }
    }

    void GetAllChickens() {
        foreach (Transform child in ChickenParentTransform) {
            ChickenController chikenControl = child.GetComponent<ChickenController>();
            if (chikenControl != null) {
                chickenControllers.Add(chikenControl);
            }
        }
    }
}
