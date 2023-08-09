using System;
using UnityEngine;

public enum GameState {
    Loading,
    Menu,
    Game,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instace; } }
    private static GameManager instace;

    [SerializeField] BridgeConnection bridgePython;
    [SerializeField] GameCanvas gameCanvas;

    private void Awake() {
        instace = this;
    }

    public GameState gameState = GameState.Loading;

    public Action OnGameStart = delegate { };
    public Action OnGameIsOver = delegate { };
    public Action OnGameExit = delegate { };
    public Action OnCameraDetected = delegate { };

    public void CameraDetected() { OnCameraDetected?.Invoke(); }
    public void StartGame() {
        gameState = GameState.Game;
        OnGameStart?.Invoke(); 
    }
    public void GameIsOver() {
        print("deathqsqsqsq");
        gameState = GameState.GameOver; 
        OnGameIsOver?.Invoke(); 
    }
    public void GameExit() { OnGameExit?.Invoke(); }



    public void ChangeGameCanvas(GameState gameState) {
        gameCanvas.EnableCanvas(gameState);
    }
}
