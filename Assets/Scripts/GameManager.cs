using System;
using System.Collections;
using UnityEngine;

public enum GameState {
    Loading,
    Menu,
    Game,
    GameOver, 
    Instructions
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instace; } }
    private static GameManager instace;

    [SerializeField] BridgeConnection bridgePython;
    [SerializeField] GameCanvas gameCanvas;


    public int score = 0;

    private void Awake() {
        instace = this;
    }

    public GameState gameState = GameState.Loading;

    public Action OnGameStart = delegate { };
    public Action OnGameIsOver = delegate { };
    public Action OnGameExit = delegate { };
    public Action OnCameraDetected = delegate { };

    public void CameraDetected() { 
        OnCameraDetected?.Invoke();
        MusicManager.Instance.ChangeMusicState(GameState.Menu);
    }

    public void StartGame() {
        gameState = GameState.Game;
        MusicManager.Instance.ChangeMusicState(GameState.Game);
        OnGameStart?.Invoke();
        ResetScore();
    }

    public void GameIsOver() {
        gameState = GameState.GameOver;
        MusicManager.Instance.ChangeMusicState(GameState.GameOver);
        OnGameIsOver?.Invoke(); 
    }

    public void GameExit() {
        StartCoroutine(CorrutineGameExit());
    }

    IEnumerator CorrutineGameExit() {
        gameCanvas.PlayFadeOn();
        yield return new WaitForSeconds(gameCanvas.totalTimeFadeAnim);

        gameState = GameState.Menu;
        MusicManager.Instance.ChangeMusicState(GameState.Menu);
        OnGameExit?.Invoke();
        yield return new WaitForSeconds(0.25f);

        gameCanvas.PlayFadeOff();
        yield return new WaitForSeconds(gameCanvas.totalTimeFadeAnim);
    }

    public void AddScore() {
        score++;
        gameCanvas.ShowScore(score);
    }

    public void ResetScore() {
        score = 0;
        gameCanvas.ShowScore(score);
    }



    public void ChangeGameCanvas(GameState gameState) {
        gameCanvas.EnableCanvas(gameState);
    }
}
