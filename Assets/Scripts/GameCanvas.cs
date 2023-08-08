using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] GameObject LoadingGO;
    [SerializeField] GameObject MenuGO;
    [SerializeField] GameObject GameGO;


    private void Start() {
        GameManager.Instance.OnCameraDetected += EnableMenu;
    }

    void DisableAllCanvas() {
        LoadingGO.SetActive(false);
        MenuGO.SetActive(false);
        GameGO.SetActive(false);
    }

    public void EnableMenu() {
        EnableCanvas(GameState.Menu);
    }

    private void EnableCanvas(GameState gameState) {
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

            default:
                break;
        }
    }
}
