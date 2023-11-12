using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseView : View
{

    [SerializeField] private Button backToMenuBtn;
    [SerializeField] private Button restartLevel;
    [SerializeField] private Button resumeLevel;





    public override void InitView()
    {
        base.InitView();

        backToMenuBtn.onClick.RemoveAllListeners();
        backToMenuBtn.onClick.AddListener(() =>
        {
            LevelManager.Instance.EndLevel();
            Hide();
        });


        restartLevel.onClick.RemoveAllListeners();
        restartLevel.onClick.AddListener(() =>
        {
            LevelManager.Instance.RestartLevel();
            Hide();
        });

        resumeLevel.onClick.RemoveAllListeners();
        resumeLevel.onClick.AddListener(() => Hide());

    }

    public override void Show()
    {
        base.Show();
        Utility.Pause();
    }

    public override void Hide()
    {
        base.Hide();
        Utility.Resume();
    }



}
