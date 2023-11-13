using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = Utility.SCRIPTABLE_PATH + "LevelData", fileName = "Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private List<LevelDetals> levels = new();

    public int AllLevelsCount
    {
        get
        {
            int count = 0;
            foreach (var level in levels)
            {
                count += level.levels.Count;
            }
            return count;
        }
    }

    public LevelDetals GetLevelInfo(DifficultyEnum diff) => levels.FirstOrDefault(l => l.difficulty == diff);
    public int LargestCardCount => levels.SelectMany(l => l.levels).Select(l => l.cardCount).Max();
    public int GetTime(int level) => levels.SelectMany(l => l.levels).Where(i => i.level == level).First().time;
    public void SetScore(int level, int score)
    {
        var info = levels.SelectMany(l => l.levels).Where(i => i.level == level).First();
        info.lastScore = score;
        if (score > info.highestScore)
            info.highestScore = score;
    }

    public void SetIds()
    {
        int id = 1;
        foreach (var level in levels)
        {
            foreach (var item in level.levels)
            {
                item.level = id;
                id++;
            }
        }
    }


}

public enum DifficultyEnum
{
    Easy = 0,
    Normal = 1,
    Hard = 2,
}

[System.Serializable]
public struct LevelDetals
{
    public DifficultyEnum difficulty;
    public List<LevelInfo> levels;

}

[System.Serializable]
public class LevelInfo
{
    [HideInInspector] public int level;
    public int cardCount;
    public int time;
    public int lastScore;
    public int highestScore;
}
