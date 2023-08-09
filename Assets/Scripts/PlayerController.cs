using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject ParticleDeath;
    [SerializeField] GameObject PumkinGO;

    [Header("Componentes Pumpkin")]
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerRotation playerRotation;

    public bool playerIsActive = false;

    private void Start() {
        GameManager.Instance.OnGameStart += SpawnPlayer;
    }

    public void Death() {
        DisactivePumpkinMovement();

        PumkinGO.SetActive(false);
        ParticleDeath.SetActive(true);

        GameManager.Instance.GameIsOver();
    }

    public void SpawnPlayer() {
        playerMovement.ResetInitialPosition();
        ActivePumpkinMovement();

        PumkinGO.SetActive(true);
        ParticleDeath.SetActive(false);
    }



    public void ActivePumpkinMovement() {
        SetActivePumpkinMovement(true);
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
