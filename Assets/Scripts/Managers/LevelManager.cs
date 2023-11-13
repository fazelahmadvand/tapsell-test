using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private TMP_Text scoreTxt;

    [Header("Current Level Info")]
    [SerializeField] private DifficultyEnum currentLevelDifficulty;
    [SerializeField] private LevelInfo currentLevelInfo;
    [SerializeField] private float startTime;
    [SerializeField] private bool isLevelStart;
    [SerializeField] private int score;

    [Header("Polish")]
    [SerializeField] private float duration = .1f;
    [SerializeField] private Vector3 rotate = Vector3.up * 90;
    [SerializeField] private float delayTime = .5f;


    public static event Action OnWin;
    public static event Action OnLose;
    public event Action OnStart;



    private int Score
    {
        get => score;
        set
        {
            score = value;
            scoreTxt.text = $"Score: {score}";
        }
    }


    private readonly Queue<CardView> clickedCards = new();
    private readonly Queue<int> randomNumber = new();

    public void Init()
    {
        int largestCount = levelData.LargestCardCount;
        root.SetActive(false);
        cardHolder.CreateCards(largestCount);
        pauseView.InitView();
        pauseBtn.onClick.RemoveAllListeners();
        pauseBtn.onClick.AddListener(() =>
        {
            pauseView.ShowPause();

        });


        OnWin += () =>
        {
            pauseView.ShowResult(true);
            isLevelStart = false;
            pauseBtn.interactable = isLevelStart;
            levelData.SetScore(currentLevelInfo.level, Score);
        };

        OnLose += () =>
        {
            startTime = 0;
            isLevelStart = false;

            pauseBtn.interactable = isLevelStart;
            pauseView.ShowResult(false);
            cardHolder.HandleInteractable(isLevelStart);
        };

        OnStart += () =>
        {
            isLevelStart = true;
            SetTime();
            clickedCards.Clear();
            pauseBtn.interactable = isLevelStart;
            HandleCards();
            Score = 0;
            cardHolder.HandleInteractable(isLevelStart);

        };

    }

    public void StartLevel(DifficultyEnum difficulty, LevelInfo info)
    {
        currentLevelDifficulty = difficulty;
        currentLevelInfo = info;
        OnStart?.Invoke();
        root.SetActive(true);
        HandleLevelInfo();
    }
    public void RestartLevel()
    {
        OnStart?.Invoke();
    }

    public void EndLevel()
    {
        isLevelStart = false;
        SetTime();
        clickedCards.Clear();

        MenuManager.Instance.UpdateView();
        root.SetActive(false);
    }

    private void Update()
    {
        if (isLevelStart)
        {
            startTime -= Time.deltaTime;
            timeTxt.text = Utility.Timer(startTime);
            if (startTime <= 0)
            {

                OnLose?.Invoke();
            }
        }


    }

    private void HandleCards()
    {
        FillRandomNumber();

        cardHolder.HideAll();
        var cards = cardHolder.Cards;
        var list = new List<CardView>();

        for (int i = 0; i < currentLevelInfo.cardCount; i++)
        {
            var c = cards[i];
            c.Show();
            c.transform.localScale = Vector3.one;
            c.HideText();
            c.UpdateCard(c.name, null, () =>
            {

                if (!clickedCards.Contains(c))
                    c.transform.DORotate(rotate, duration, RotateMode.FastBeyond360).onComplete += () =>
                    {
                        c.ShowText();
                        clickedCards.Enqueue(c);
                        StartCoroutine(ManageSelectedCards());
                    };

            });
            list.Add(c);
        }

        SetRandomNumbers(list);

    }

    private void HandleLevelInfo()
    {
        levelInfoTxt.text = $"{currentLevelDifficulty} - {currentLevelInfo.level}";
    }


    private void SetTime()
    {
        startTime = levelData.GetTime(currentLevelInfo.level);
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
                Score++;
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
        StartCoroutine(HandleWin());
    }

    private void FillRandomNumber()
    {
        randomNumber.Clear();
        var count = currentLevelInfo.cardCount / 2;

        for (var i = 0; i < count; i++)
        {
            randomNumber.Enqueue(i);
        }


    }

    private void SetRandomNumbers(List<CardView> list)
    {

        while (list.Count > 0)
        {
            int rnd = UnityEngine.Random.Range(0, list.Count);
            var first = list[rnd];
            list.RemoveAt(rnd);

            rnd = UnityEngine.Random.Range(0, list.Count);
            var second = list[rnd];
            list.RemoveAt(rnd);

            string rndName = randomNumber.Dequeue().ToString();
            first.name = rndName;
            second.name = rndName;
            first.UpdateText(rndName);
            second.UpdateText(rndName);


        }
    }

    private IEnumerator HandleWin()
    {
        yield return new WaitForSeconds(duration + .1f);
        var removed = cardHolder.Cards.Where(c => c.gameObject.activeSelf && c.transform.localScale == Vector3.zero).ToList();
        if (removed.Count == currentLevelInfo.cardCount)
        {

            OnWin?.Invoke();
        }
    }

}

