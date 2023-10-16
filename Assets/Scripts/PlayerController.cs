using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Pumpkin")]
    [SerializeField] GameObject ParticleDeath;
    [SerializeField] GameObject PumkinGO;

    [Header("Componentes Pumpkin")]
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerRotation playerRotation;
    [SerializeField] SphereCollider playerCollider;

    [Header("Variables")]
    public bool playerIsActive = false;
    [SerializeField] int scorePlayer = 0;

    Vector3 deathPlayerInitPos = Vector3.zero;

    private void Start() {
        GameManager.Instance.OnGameExit += SpawnPlayer;
        GameManager.Instance.OnGameStart += SpawnAndStartPlayer;

        deathPlayerInitPos = ParticleDeath.transform.position;
    }

    public void Death() {
        playerCollider.enabled = false;
        DisactivePumpkinMovement();

        PumkinGO.SetActive(false);
        ParticleDeath.SetActive(true);

        GameManager.Instance.GameIsOver();
    }

    public void SpawnPlayer() {
        playerCollider.enabled = true;
        scorePlayer = 0;
        playerMovement.ResetInitialPosition();
        ParticleDeath.transform.position = deathPlayerInitPos;

        PumkinGO.SetActive(true);
        ParticleDeath.SetActive(false);
    }

    public void SpawnAndStartPlayer() {
        SpawnPlayer();
        ActivePumpkinMovement();
    }


    public void AddScore() {
        scorePlayer += 1;
    }


    //cORRUTINA QUE ACTIVE UNA ESPECIE DE CUENTA ATRAS PARA INICIAR 
    //FADE CUANDO SE DA A VOLVER A JUGAR, INICIAR CUENTA Y UNA VEZ ACABADO INICIAR 



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
