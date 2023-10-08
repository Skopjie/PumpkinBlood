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
    [SerializeField] CandleController[] candleList;

    public Transform ChickenParentTransform;
    [SerializeField] List<ChickenController> chickenControllers = new List<ChickenController>();

    [SerializeField] bool isMoving = false;


    Vector3 globalForward;
    Vector3 globalForwardMovement;
    Vector3 forward = Vector3.forward;

    private void Start() {
        //speedPatron = GameManager.Instance.actualSpeedPlatform;
        GameManager.Instance.OnGameIsOver += StopMovement;
        GameManager.Instance.OnGameStart += StartMovement;

        GetAllChickens();
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
        DisctiveAllCandle();
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

    public void ActiveAllCandle() {
        foreach(CandleController candel in candleList)
            candel.ActiveCandle();
    }

    public void DisctiveAllCandle() {
        foreach (CandleController candel in candleList)
            candel.DisactiveCandle();
    }
}
