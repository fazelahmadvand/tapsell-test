using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = Utility.SCRIPTABLE_PATH + "LevelData", fileName = "Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private List<LevelInfo> levels = new();

    public int AllLevelsCount
    {
        get
        {
            int count = 0;
            foreach (var level in levels)
            {
                count += level.levelCardCounts.Count;
            }
            return count;
        }
    }

    public LevelInfo GetLevelInfo(DifficultyEnum diff) => levels.FirstOrDefault(l => l.difficulty == diff);
    public int LargestCardCount => levels.SelectMany(l => l.levelCardCounts).Max();

}

public enum DifficultyEnum
{
    Easy = 0,
    Normal = 1,
    Hard = 2,
}

[System.Serializable]
public struct LevelInfo
{
    public DifficultyEnum difficulty;
    public List<int> levelCardCounts;
    public int startNum, endNum;


}
