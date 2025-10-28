using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FlappyUIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endScoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI tapToStartText;
    public TextMeshProUGUI uGotBestScore;

    public GameObject popUp;
    public Button retryButton;
    public Button zoneButton;

    private bool ignoreStartFlap = false;
    private float ignoreDuration = 0.1f;

    FlappyGameManager gameManager;

    public bool IgnoreStartFlap()
    {
        return ignoreStartFlap;
    }

    void Start()
    {
        gameManager = FlappyGameManager.Instance;

        if (popUp == null)
            Debug.LogError("popUp is null");

        if (titleText == null)
            Debug.LogError("titleText is null");

        if (scoreText == null)
            Debug.LogError("score text is null");

        if (endScoreText == null) 
            Debug.LogError("endScoreText is null");

        if (bestScoreText == null) 
            Debug.LogError("bestScoreText is null");

        if (retryButton == null)
            Debug.LogError("startButton is null");

        if (retryButton == null) 
            Debug.LogError("restartButton is null");

        if (zoneButton == null) 
            Debug.LogError("zoneButton is null");

        if (gameManager == null) 
            Debug.LogError("FlappyGameManager.Instance is null");

        if (tapToStartText == null)
            Debug.LogError("tapToStartText is null");

        if (uGotBestScore == null)
            Debug.LogError("uGotBestScore is null");

        if (retryButton != null)
        {
            retryButton.onClick.AddListener(OnClickReStartButton);
        }
        if (zoneButton != null)
        {
            zoneButton.onClick.AddListener(OnClickZoneButton);
        }
    }

    private void Update()
    {
        if (ignoreStartFlap)
        {
            ignoreDuration -= Time.unscaledDeltaTime;

            if(ignoreDuration <= 0)
            {
                ignoreStartFlap = false;
            }
            return;
        }
        if (gameManager.GetGameState() == GameState.Ready)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnClickStartButton();
            }
        }
    }

    public void ShowReadyUI()
    {
        titleText.gameObject.SetActive(true);
        tapToStartText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        popUp.gameObject.SetActive(false);
    }

    public void ShowScoreUI()
    {
        titleText.gameObject.SetActive(false);
        tapToStartText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
    }

    public void ShowPopUpUI()
    {
        scoreText.gameObject.SetActive(false);
        uGotBestScore.gameObject.SetActive(false);
        popUp.gameObject.SetActive(true);
    }

    public void ShowBestScorePopUpUI()
    {
        scoreText.gameObject.SetActive(false);
        uGotBestScore.gameObject.SetActive(true);
        popUp.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateEndScore(int score, int bestScore)
    {
        endScoreText.text = score.ToString();
        bestScoreText.text = bestScore.ToString();
    }

    void OnClickStartButton()
    {
        gameManager.SetGameState(GameState.Playing);

        ignoreStartFlap = true;
        ignoreDuration = 0.1f;
    }

    void OnClickReStartButton()
    {
        gameManager.RestartGame();
    }

    void OnClickZoneButton()
    {
        SceneManager.Instance.LoadScene(GameScene.MinigameZoneScene);
    }
}
