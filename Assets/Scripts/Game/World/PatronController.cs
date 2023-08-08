using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void FixedUpdate() {
        if(isMoving) transform.Translate(-Vector3.forward * speedPatron * Time.deltaTime);
    }

    public void SetEnablePatron(bool enable) {
        if (enable) EnablePatron();
        else DisablePatron();
    }

    void EnablePatron() {
        gameObject.SetActive(true);
    }

    void DisablePatron() {
        gameObject.SetActive(false);
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
}
