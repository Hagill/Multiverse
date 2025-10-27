using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using TMPro;

public class Portal : MonoBehaviour
{
    public GameScene scene = GameScene.MinigameZoneScene;

    [SerializeField] private GameObject uiPanel;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private MinigameType minigameType;
    private void Awake()
    {
        if (uiPanel != null)
            uiPanel.SetActive(false);

        if (scorePanel != null)
            scorePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.SetCurrentPortal(this);

            if (uiPanel != null)
                uiPanel.SetActive(true);

            if (scorePanel != null)
            {
                scorePanel.SetActive(true);

                if (bestScoreText != null && GameDataManager.Instance != null)
                {
                    if(minigameType != MinigameType.None)
                    {
                        int minigameBestScore = GameDataManager.Instance.GetHIghScore(minigameType);
                        bestScoreText.text = ($"{minigameBestScore}");
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.UnSetCurrentPortal();

            if (uiPanel != null)
                uiPanel.SetActive(false);

            if (scorePanel != null)
                scorePanel.SetActive(false);
        }
    }
}
