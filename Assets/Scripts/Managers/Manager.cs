using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Singleton<Manager>
{

    [SerializeField] private LevelData levelData;

    [SerializeField] private float duration = .1f;
    [SerializeField] private Vector3 rotate = Vector3.forward * 180;



    public static float Duration { get; set; }
    public static Vector3 Rotation { get; set; }


    private void Update()
    {
        Duration = duration;
        Rotation = rotate;
    }

    void Start()
    {
        levelData.SetIds();
        MenuManager.Instance.Init();
        LevelManager.Instance.Init();
    }



}
