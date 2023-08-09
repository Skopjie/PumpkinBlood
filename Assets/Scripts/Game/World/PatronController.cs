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
    [SerializeField] Rigidbody rgbd;
    public Transform plartformScale;

    [SerializeField] bool isMoving = false;

    private void Start() {
        rgbd = GetComponent<Rigidbody>();

        GameManager.Instance.OnGameIsOver += StopMovement;
        GameManager.Instance.OnGameStart += StartMovement;
    }

    private void FixedUpdate() {
        if(isMoving) transform.Translate(-Vector3.forward * speedPatron * Time.deltaTime);
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
}
