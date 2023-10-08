using System;
using System.Collections;
using UnityEngine;

public enum GameState {
    Loading,
    Menu,
    Game,
    GameOver, 
    Instructions,
    ReturnToMenu
}

[System.Serializable]
public class LevelData {
    public int numberChickens;
    public float speedPlatform;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instace; } }
    private static GameManager instace;

    [Header("Componentes")]
    [SerializeField] BridgeConnection bridgePython;
    [SerializeField] GameCanvas gameCanvas;

    [Header("Variables")]
    public int score = 0;

    public float actualSpeedPlatform = 15; //5

    private void Awake() {
        instace = this;
    }

    public GameState gameState = GameState.Loading;
    [SerializeField] LevelData[] levelData;

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
        MusicManager.Instance.PlaySFXSound(SoundEffects.Laught);
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
        ChangeGameCanvas(GameState.ReturnToMenu);
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
        actualSpeedPlatform = 2;
    }

    public void CheckNewLevel() {
        foreach(LevelData level in levelData)
            if(level.numberChickens == score)
                actualSpeedPlatform = level.speedPlatform;
    }

    public void UpdateSpreed() { }

    public void ChangeGameCanvas(GameState gameState) {
        gameCanvas.EnableCanvas(gameState);
    }
}
