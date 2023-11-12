using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Singleton<Manager>
{



    void Start()
    {
        MenuManager.Instance.Init();
        LevelManager.Instance.Init();
    }



}
