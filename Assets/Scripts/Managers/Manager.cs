using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Singleton<Manager>
{

    [SerializeField] private LevelData levelData;

    void Start()
    {
        levelData.SetIds();
        MenuManager.Instance.Init();
        LevelManager.Instance.Init();
    }



}
