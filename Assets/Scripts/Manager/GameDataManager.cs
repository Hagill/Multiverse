using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    private static GameDataManager instance;
    public static GameDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameDataManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameDataManager");
                    instance = obj.AddComponent<GameDataManager>();
                }
            }
            return instance;
        }
    }
    // 각 미니게임 별 최고 점수
    private Dictionary<MinigameType, int> minigameBestScores = new Dictionary<MinigameType, int>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadAllHighScores();
    }

    // 미니게임별 최고 점수를 반환
    public int GetHIghScore(MinigameType minigameType)
    {
        if (minigameType == MinigameType.None)
        {
            return 0;
        }
        if (minigameBestScores.ContainsKey(minigameType))
        {
            return minigameBestScores[minigameType];
        }
        return 0;
    }

    // 미니게임별 최고 점수를 재설정함
    public void SetHighScore(MinigameType minigameType, int newScore)
    {
        if (minigameType == MinigameType.None)
        {
            return;
        }

        int currentBestScore = GetHIghScore(minigameType);
        if (newScore > currentBestScore)
        {
            minigameBestScores[minigameType] = newScore;
            PlayerPrefs.SetInt($"{minigameType.ToString()}_BestScore", newScore);
            PlayerPrefs.Save();
        }
    }

    // 모든 미니 게임의 최고점수를 불러옴
    private void LoadAllHighScores()
    {
        foreach (MinigameType type in Enum.GetValues(typeof(MinigameType)))
        {
            if (type == MinigameType.None)
            {
                continue;
            }

            string key = $"{type.ToString()}_BestScore";
            int bestScore = PlayerPrefs.GetInt(key, 0);
            minigameBestScores[type] = bestScore;
        }
    }
}
