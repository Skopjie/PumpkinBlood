using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Pumpkin")]
    [SerializeField] GameObject ParticleDeath;
    [SerializeField] GameObject PumkinGO;

    [Header("Componentes Pumpkin")]
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerRotation playerRotation;

    public bool playerIsActive = false;

    Vector3 deathPlayerInitPos = Vector3.zero;

    private void Start() {
        GameManager.Instance.OnGameExit += SpawnPlayer;
        GameManager.Instance.OnGameStart += SpawnAndStartPlayer;

        deathPlayerInitPos = ParticleDeath.transform.position;
    }

    public void Death() {
        DisactivePumpkinMovement();

        PumkinGO.SetActive(false);
        ParticleDeath.SetActive(true);

        GameManager.Instance.GameIsOver();
    }

    public void SpawnPlayer() {
        playerMovement.ResetInitialPosition();
        ParticleDeath.transform.position = deathPlayerInitPos;

        PumkinGO.SetActive(true);
        ParticleDeath.SetActive(false);
    }

    public void SpawnAndStartPlayer() {
        SpawnPlayer();
        ActivePumpkinMovement();
    }



    public void ActivePumpkinMovement() {
        SetActivePumpkinMovement(true);
        ActivePumpkinMovement();
    }

    public void DisactivePumpkinMovement() {
        SetActivePumpkinMovement(false);
    }

    public void SetActivePumpkinMovement(bool active) {
        playerIsActive = active;

        playerMovement.SetActiveMovement(active);
        playerRotation.SetActiveMovement(active);
    }
}
