using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : Singleton<MenuManager>
{

    [SerializeField] private CardHolder missionCardHolder;
    [SerializeField] private List<DifficultyInfoView> difficultyBtns = new();



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

            });

        }

        HandleClick(difficultyBtns[0].difficulty);


    }




    private void HandleClick(DifficultyEnum difficulty)
    {
        var info = levelData.GetLevelInfo(difficulty);

        missionCardHolder.HideAll();
        var cards = missionCardHolder.Cards;
        for (int i = 0; i < info.levelCardCounts.Count; i++)
        {
            var ii = i;
            var card = cards[ii];
            card.Show();
            card.UpdateCard(info.difficulty.ToString(), null, () =>
            {
                Debug.Log("Start Mission");
                LevelManager.Instance.StartLevel(info.difficulty, info.levelCardCounts[ii]);
            });
        }
    }



}


[System.Serializable]
public struct DifficultyInfoView
{
    public DifficultyEnum difficulty;
    public Button btn;
}
