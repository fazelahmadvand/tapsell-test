using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : Singleton<MenuManager>
{

    [SerializeField] private CardHolder missionCardHolder;
    [SerializeField] private List<DifficultyInfoView> difficultyBtns = new();
    [SerializeField] private DifficultyEnum lastSelectedPage;
    [SerializeField] private Color selectCardColor;

    [SerializeField] private LevelData levelData;


    public void Init()
    {
        missionCardHolder.CreateCards(levelData.AllLevelsCount);


        foreach (var item in difficultyBtns)
        {

            item.btn.onClick.RemoveAllListeners();
            item.btn.onClick.AddListener(() =>
            {
                HandleClick(item.difficulty);
                MakeButtonsWhite();
                lastSelectedPage = item.difficulty;
                item.img.color = Color.yellow;
            });

        }

        HandleClick(difficultyBtns[0].difficulty);
        difficultyBtns[0].img.color = Color.yellow;

    }


    private void HandleClick(DifficultyEnum difficulty)
    {
        var info = levelData.GetLevelInfo(difficulty);

        missionCardHolder.HideAll();
        var cards = missionCardHolder.Cards;
        for (int i = 0; i < info.levels.Count; i++)
        {
            var ii = i;
            var card = cards[ii];
            var currentLevel = info.levels[ii];
            card.Show();
            string name = $"{info.difficulty} \n Level: {currentLevel.level} \n Target Time: {currentLevel.time}s \n Cards: {currentLevel.cardCount} \n Last Score: {currentLevel.lastScore} \n Highest Score: {currentLevel.highestScore}";
            card.UpdateCard(name, null, () =>
            {
                missionCardHolder.MakeThemWhite();
                card.SetColor(selectCardColor);

                LevelManager.Instance.StartLevel(info.difficulty, currentLevel);

            });
        }
    }

    private void MakeButtonsWhite()
    {
        foreach (var item in difficultyBtns)
        {
            item.img.color = Color.white;
        }
        missionCardHolder.MakeThemWhite();
    }


    public void UpdateView()
    {
        HandleClick(lastSelectedPage);

    }



}


[System.Serializable]
public struct DifficultyInfoView
{
    public DifficultyEnum difficulty;
    public Image img;
    public Button btn;
}
