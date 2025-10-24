using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum GameState
{
    Ready,
    Playing,
    GameOver
}

public class FlappyGameManager : MonoBehaviour
{
    static FlappyGameManager gameManager;

    public static FlappyGameManager Instance { get { return gameManager; } }

    private int currentScore = 0;

    int bestScore = 0;
    public int BestScore { get => bestScore; }
    private const string BestScoreKey = "BestScore";

    FlappyUIManager uiManager;
    public FlappyUIManager UIManager { get { return uiManager; } }
    
    private GameState currentGameState = GameState.Ready;
    public GameState GetGameState() { return currentGameState; }

    private FlappyPlayer player;

    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<FlappyUIManager>();
        player = FindObjectOfType<FlappyPlayer>();

        LoadHighScore();
    }

    private void Start()
    {
        SetGameState(GameState.Ready);
    }

    // 게임 상태 설정 함수
    public void SetGameState(GameState state)
    {
        if (currentGameState == state) return;

        currentGameState = state;

        switch (currentGameState)
        {
            case GameState.Ready:
                Time.timeScale = 0f;

                uiManager.ShowReadyUI();
                player.ResetPlayerState();
                currentScore = 0;

                break;

            case GameState.Playing:
                Time.timeScale = 1f;

                uiManager.ShowScoreUI();
                player.StartFlapping();

                break;

            case GameState.GameOver:
                uiManager.ShowPopUpUI();
                break;
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");

        UpdateHighScore();

        uiManager.UpdateEndScore(currentScore, bestScore);

        SetGameState(GameState.GameOver);
    }

    public void RestartGame()
    {
        SceneManager.Instance.LoadScene(GameScene.MinigameScene);
    }

    public void AddScore(int score)
    {
        currentScore += score;
        Debug.Log("Score: " + currentScore);
        uiManager.UpdateScore(currentScore);
    }

    private void LoadHighScore()
    {
        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
    }

    private void UpdateHighScore()
    {
        if (bestScore < currentScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt(BestScoreKey, bestScore);
        }
    }
}
