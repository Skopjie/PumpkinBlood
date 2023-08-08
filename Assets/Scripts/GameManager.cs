using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    Loading,
    Menu,
    Game
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instace; } }
    private static GameManager instace;

    private void Awake() {
        instace = this;
    }

    public GameState gameState = GameState.Loading;

    public Action OnGameIsOver = delegate { };
    public Action OnGameExit = delegate { };

    public Action OnCameraDetected = delegate { };

    void CameraDetected() {
        OnCameraDetected?.Invoke();
    }

    public void GameIsOver() { OnGameIsOver?.Invoke(); }
    public void GameExit() { OnGameExit?.Invoke(); }

}
