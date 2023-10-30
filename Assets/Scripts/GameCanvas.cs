using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using Febucci.UI;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;

public class GameCanvas : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] GameObject LoadingGO;
    [SerializeField] GameObject MenuGO;
    [SerializeField] GameObject GameGO;
    [SerializeField] GameObject GameOverGO;
    [SerializeField] GameObject InstructionsGO;

    [Header("Cameras")]
    [SerializeField] CinemachineVirtualCamera menuCamera;
    [SerializeField] CinemachineVirtualCamera gameCamera;
    [SerializeField] CinemachineVirtualCamera loadingCamera;
    [SerializeField] CinemachineVirtualCamera instructionsCamera;
    [SerializeField] CinemachineVirtualCamera gameOverCamera;

    [Header("Fade")]
    public RectTransform fadeRectTransform;
    public Image fadeImage;

    public RectTransform fadeImageRectTransform;
    public Image iconImage;

    public RectTransform fadeOnRectTransform;
    public Image iconOnImage;

    [Header("Fade Variable")]

    [SerializeField] float initialFadeTimmer = 0.2f;
    [SerializeField] float fadeTimmer = 1;

    [SerializeField] float fadeIcon1Timmer = 0.5f;
    [SerializeField] float fadeIcon2Timmer = 0.25f;
    [SerializeField] float fadeIcon3Timmer = 1;


    public float totalTimeFadeAnim = 0;

    [Header("Game Menu")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TypewriterByCharacter typewriter;

    [Header("Game Over")]
    [SerializeField] TextMeshProUGUI scoreGameOverText;
    [SerializeField] TextMeshProUGUI timmerText;


    private void Start() {
        GameManager.Instance.OnCameraDetected += EnableMenu;
        GameManager.Instance.OnGameExit += EnableMenu;
        GameManager.Instance.OnGameStart += EnableGame;
        GameManager.Instance.OnGameIsOver += EnableGameOver;

        totalTimeFadeAnim = initialFadeTimmer + fadeIcon1Timmer + fadeIcon2Timmer + fadeIcon3Timmer;

        PlayFadeOff();
    }


    public void EnableGameOver() {
        typewriter.ShowText("<wave></wave>");
        typewriter.StartDisappearingText();
        EnableCanvas(GameState.GameOver); 
    }
    public void EnableMenu() { EnableCanvas(GameState.Menu); }
    public void EnableGame() { 
        EnableCanvas(GameState.Game);
        StartCoroutine(ShowTextGame("GET SOULS!"));
    }

    public void EnableInstructions() { EnableCanvas(GameState.Instructions); }

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
            case GameState.Instructions:
                instructionsCamera.Priority = 11;
                break;
            case GameState.ReturnToMenu:
                gameOverCamera.Priority = 11;
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
                scoreGameOverText.text = GameManager.Instance.score.ToString();
                timmerText.text = GameManager.Instance.duracionFormateada;
                GameOverGO.SetActive(true);
                break;
            case GameState.Instructions:
                InstructionsGO.SetActive(true);
                break;
            default:
                break;
        }
        
        ActiveCamera(gameState);
    }




    public void DisableAllCanvas() {
        LoadingGO.SetActive(false);
        MenuGO.SetActive(false);
        GameGO.SetActive(false);
        GameOverGO.SetActive(false);
        InstructionsGO.SetActive(false);
    }

    void DisableAllCameras() {
        menuCamera.Priority = 10;
        gameCamera.Priority = 10;
        loadingCamera.Priority = 10;
        instructionsCamera.Priority = 10;
        gameOverCamera.Priority = 10;
    }

    public IEnumerator ShowTextGame(string newText) {
        typewriter.ShowText("<wave>" + newText + "</wave>");
        yield return new WaitForSeconds(2.5f);
        typewriter.StartDisappearingText();
    }

    public void ShowScore(int score) {
        scoreText.text = "<wave>X"+score+"</wave>";
    }



    [ContextMenu("FadeOff")]
    public void PlayFadeOff() {
        ResetFadePanel();
        iconImage.color = Color.black;
        fadeImage.color = Color.black;
        fadeImageRectTransform.localScale = Vector3.one;
        fadeImageRectTransform.localPosition = Vector2.zero;

        fadeImage.DOFade(0.99f, initialFadeTimmer).SetEase(Ease.OutQuad).OnComplete(() => {
            fadeImage.DOFade(0.0f, fadeTimmer).SetEase(Ease.OutQuad);
            fadeImageRectTransform.DOScale(new Vector3(2, 2, 2), fadeIcon1Timmer).OnComplete(() => {
                fadeImageRectTransform.DOScale(new Vector3(1, 1, 1), fadeIcon2Timmer).OnComplete(() => {
                    fadeImageRectTransform.DOScale(new Vector3(18, 18, 18), fadeIcon3Timmer);
                    fadeImageRectTransform.DOAnchorPos(new Vector2(0, 1575), fadeIcon3Timmer);
                    iconImage.DOFade(0.0f, fadeIcon1Timmer + fadeIcon2Timmer + fadeIcon3Timmer).SetEase(Ease.OutQuad);
                });
            });
        });
    }


    [ContextMenu("FadeOn")]
    public void PlayFadeOn() {
        ResetFadePanel();
        MusicManager.Instance.PlaySFXSound(SoundEffects.Laught);
        fadeOnRectTransform.DOScale(new Vector3(2, 2, 2), fadeIcon1Timmer).OnComplete(() => {
            fadeOnRectTransform.DOScale(new Vector3(1, 1, 1), fadeIcon2Timmer).OnComplete(() => {
                fadeOnRectTransform.DOScale(new Vector3(30, 30, 30), fadeIcon3Timmer);
                fadeImage.DOFade(1.0f, fadeIcon3Timmer).SetEase(Ease.OutQuad);
                fadeOnRectTransform.DOAnchorPos(new Vector2(0, 1600), fadeIcon3Timmer);
            });
        });
    }

    void ResetFadePanel() {
        //Off


        //On
        iconOnImage.color = Color.black;
        fadeOnRectTransform.localScale = Vector3.zero;
        fadeOnRectTransform.localPosition = Vector2.zero;
    }
}
