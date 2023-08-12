using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTimer {
    public float scale;
    public float time;
}

public class GameCanvas : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] GameObject LoadingGO;
    [SerializeField] GameObject MenuGO;
    [SerializeField] GameObject GameGO;
    [SerializeField] GameObject GameOverGO;

    [Header("Cameras")]
    [SerializeField] CinemachineVirtualCamera menuCamera;
    [SerializeField] CinemachineVirtualCamera gameCamera;
    [SerializeField] CinemachineVirtualCamera loadingCamera;

    [Header("Fade")]
    public RectTransform imageRectTransform;
    [SerializeField] List<FadeTimer> fadeTimers = new List<FadeTimer>();


    private void Start() {
        GameManager.Instance.OnCameraDetected += EnableMenu;
        GameManager.Instance.OnGameExit += EnableMenu;
        GameManager.Instance.OnGameStart += EnableGame;
        GameManager.Instance.OnGameIsOver += EnableGameOver;

        //StartCoroutine(AnimateImage());
    }


    public void EnableGameOver() { EnableCanvas(GameState.GameOver); }
    public void EnableMenu() { EnableCanvas(GameState.Menu); }
    public void EnableGame() { EnableCanvas(GameState.Game); }

    public void ActiveCamera(GameState gameState) {
        DisableAllCameras();
        switch (gameState) {
            case GameState.Loading:
                loadingCamera.Priority = 11;
                break;
            case GameState.Menu:
                menuCamera.Priority = 11;
                break;
            case GameState.Game:
                gameCamera.Priority = 11;
                break;
            case GameState.GameOver:
                gameCamera.Priority = 11;
                break;
            default:
                break;
        }
    }

    public void EnableCanvas(GameState gameState) {
        DisableAllCanvas();
        switch (gameState) {
            case GameState.Loading:
                LoadingGO.SetActive(true);
                break;
            case GameState.Menu:
                MenuGO.SetActive(true);
                break;
            case GameState.Game:
                GameGO.SetActive(true);
                break;
            case GameState.GameOver:
                GameOverGO.SetActive(true);
                break;
            default:
                break;
        }
        ActiveCamera(gameState);
    }




    void DisableAllCanvas() {
        LoadingGO.SetActive(false);
        MenuGO.SetActive(false);
        GameGO.SetActive(false);
        GameOverGO.SetActive(false);
    }

    void DisableAllCameras() {
        menuCamera.Priority = 10;
        gameCamera.Priority = 10;
        loadingCamera.Priority = 10;
    }

    /*
    private IEnumerator AnimateImage() {
        // Escala inicial
        float startTime = Time.time;
        float elapsedTime = 0f;

        foreach(FadeTimer fade in fadeTimers) {
            while (elapsedTime < animationDuration) {
                float t = elapsedTime / animationDuration;
                float currentScale = Mathf.Lerp(initialScale, targetScale, t);
                imageRectTransform.localScale = new Vector3(currentScale, currentScale, 1f);
                elapsedTime = Time.time - startTime;
                yield return null;
            }
            startTime = Time.time;
            elapsedTime = 0f;
        }
    }*/
}
