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
    public string actionText;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instace; } }
    private static GameManager instace;

    [Header("Componentes")]
    [SerializeField] BridgeConnection bridgePython;
    [SerializeField] GameCanvas gameCanvas;
    [SerializeField] Animator coffinAnim;

    [Header("Variables")]
    public int score = 0;

    public LevelData actualLevelData;

    private void Awake() {
        instace = this;
    }

    private void Start() {
        actualLevelData = levelData[0];
    }

    public GameState gameState = GameState.Loading;
    [SerializeField] LevelData[] levelData;

    public Action OnGameStart = delegate { };
    public Action OnGameIsOver = delegate { };
    public Action OnGameExit = delegate { };
    public Action OnCameraDetected = delegate { };
    public Action OnNewLevel = delegate { };


    public void CameraDetected() { 
        OnCameraDetected?.Invoke();
        MusicManager.Instance.ChangeMusicState(GameState.Menu);
    }

    public void StartGame() {
        startTime = DateTime.Now;
        actualLevelData = levelData[0];
        OnNewLevel?.Invoke();
        gameState = GameState.Game;
        MusicManager.Instance.PlaySFXSound(SoundEffects.Laught);
        MusicManager.Instance.ChangeMusicState(GameState.Game);
        OnGameStart?.Invoke();
        ResetScore();
    }

    public void GameIsOver() {
        endTime = DateTime.Now;
        GetTimePerGame();
        gameState = GameState.GameOver;
        MusicManager.Instance.ChangeMusicState(GameState.GameOver);
        OnGameIsOver?.Invoke(); 
    }

    public void GameExit() {
        StartCoroutine(CorrutineGameExit());
    }

    public DateTime startTime;
    public DateTime endTime;
    public TimeSpan duracion;
    public string duracionFormateada;
    public void GetTimePerGame() {
        duracion = endTime - startTime;
        duracionFormateada = string.Format("{0:00}:{1:00}", (int)duracion.TotalMinutes, duracion.Seconds);
    }

    IEnumerator CorrutineGameExit() {
        gameCanvas.DisableAllCanvas();
        coffinAnim.SetTrigger("Death");

        yield return new WaitForSeconds(1.25f);

        ChangeGameCanvas(GameState.ReturnToMenu);
        gameCanvas.PlayFadeOn();
        yield return new WaitForSeconds(gameCanvas.totalTimeFadeAnim);

        gameState = GameState.Menu;
        MusicManager.Instance.ChangeMusicState(GameState.Menu);
        OnGameExit?.Invoke();
        yield return new WaitForSeconds(0.5f);

        gameCanvas.PlayFadeOff();
        yield return new WaitForSeconds(gameCanvas.totalTimeFadeAnim);
    }

    public void AddScore() {
        score++;
        gameCanvas.ShowScore(score);
        CheckNewLevel();
    }

    public void ResetScore() {
        score = 0;
        gameCanvas.ShowScore(score);
    }

    public void CheckNewLevel() {
        foreach(LevelData level in levelData)
            if (level.numberChickens == score) {
                actualLevelData = level;
                StartCoroutine(gameCanvas.ShowTextGame(level.actionText));
                MusicManager.Instance.PlaySFXSound(SoundEffects.Laught);
                OnNewLevel?.Invoke();
            }
    }

    public void UpdateSpreed() { }

    public void ChangeGameCanvas(GameState gameState) {
        gameCanvas.EnableCanvas(gameState);
    }
}
