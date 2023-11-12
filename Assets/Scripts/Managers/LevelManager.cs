using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : Singleton<LevelManager>
{
    [Header("Components")]
    [SerializeField] private GameObject root;
    [SerializeField] private CardHolder cardHolder;
    [SerializeField] private LevelData levelData;
    [SerializeField] private PauseView pauseView;

    [Header("Header")]
    [SerializeField] private Button pauseBtn;
    [SerializeField] private TMP_Text timeTxt;

    [Header("Current Level Info")]
    [SerializeField] private DifficultyEnum currentLevelDifficulty;
    [SerializeField] private int currentLevelCardCount;
    [SerializeField] private float startTime;
    [SerializeField] private bool isLevelStart;






    public void Init()
    {
        int largestCount = levelData.LargestCardCount;
        root.SetActive(false);
        cardHolder.CreateCards(largestCount);
        pauseView.InitView();
        pauseBtn.onClick.RemoveAllListeners();
        pauseBtn.onClick.AddListener(() =>
        {
            pauseView.Show();

        });

    }

    public void StartLevel(DifficultyEnum difficulty, int count)
    {
        currentLevelDifficulty = difficulty;
        currentLevelCardCount = count;
        startTime = 0;
        isLevelStart = true;
        root.SetActive(true);
        HandleCards();
    }
    public void RestartLevel()
    {
        startTime = 0;
        HandleCards();
    }

    public void EndLevel()
    {
        isLevelStart = false;
        startTime = 0;
        root.SetActive(false);
    }

    private void Update()
    {
        if (isLevelStart)
        {
            startTime += Time.deltaTime;
            timeTxt.text = Utility.Timer(startTime);
        }
    }

    private void HandleCards()
    {
        cardHolder.HideAll();
        var cards = cardHolder.Cards;
        for (int i = 0; i < currentLevelCardCount; i++)
        {
            var c = cards[i];
            c.Show();

        }
    }



}

