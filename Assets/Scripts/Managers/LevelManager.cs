using DG.Tweening;
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
    [SerializeField] private TMP_Text levelInfoTxt;

    [Header("Current Level Info")]
    [SerializeField] private DifficultyEnum currentLevelDifficulty;
    [SerializeField] private LevelInfo currentLevelInfo;
    [SerializeField] private float startTime;
    [SerializeField] private bool isLevelStart;

    [Header("Polish")]
    [SerializeField] private float duration = .1f;
    [SerializeField] private Vector3 rotate = Vector3.up * 90;
    [SerializeField] private float delayTime = .5f;





    private readonly Queue<CardView> clickedCards = new();

    private readonly List<int> randomNumber = new();

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

    public void StartLevel(DifficultyEnum difficulty, LevelInfo info)
    {
        currentLevelDifficulty = difficulty;
        currentLevelInfo = info;
        startTime = 0;
        isLevelStart = true;
        root.SetActive(true);
        HandleCards();
        HandleLevelInfo();
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
        FillRandomNumber();

        cardHolder.HideAll();
        var cards = cardHolder.Cards;
        var list = new List<GameObject>();

        for (int i = 0; i < currentLevelInfo.cardCount; i++)
        {
            var c = cards[i];
            c.Show();
            c.transform.localScale = Vector3.one;
            c.HideText();
            c.name = name;
            c.UpdateCard(name, null, () =>
            {

                if (!clickedCards.Contains(c))
                    c.transform.DORotate(rotate, duration, RotateMode.FastBeyond360).onComplete += () =>
                    {
                        c.ShowText();
                        clickedCards.Enqueue(c);
                        StartCoroutine(ManageSelectedCards());
                    };

            });
            list.Add(c.gameObject);
        }

        SetRandomNumbers(list);

    }

    private void HandleLevelInfo()
    {
        levelInfoTxt.text = $"{currentLevelDifficulty} - {currentLevelInfo.level}";
    }

    private IEnumerator ManageSelectedCards()
    {
        yield return new WaitForSeconds(delayTime);
        if (clickedCards.Count >= 2)
        {
            var first = clickedCards.Dequeue();
            var second = clickedCards.Dequeue();

            var list = new List<CardView>() { first, second };

            if (first.name.Equals(second.name))
            {
                foreach (var card in list)
                {
                    card.transform.DOScale(Vector3.zero, duration);
                }

            }
            else
            {

                foreach (var card in list)
                {
                    card.HideText();
                    card.transform.DORotate(Vector3.zero, duration, RotateMode.FastBeyond360);
                }

            }

        }
    }

    private void FillRandomNumber()
    {
        randomNumber.Clear();
        var count = currentLevelInfo.cardCount / 2;

        for (var i = 0; i < count; i++)
        {
            randomNumber.Add(i);
        }


    }

    private void SetRandomNumbers(List<GameObject> list)
    {


    }


}

