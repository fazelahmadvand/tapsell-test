using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseView : View
{

    [SerializeField] private Button backToMenuBtn;
    [SerializeField] private Button restartLevel;
    [SerializeField] private Button resumeLevel;


    [SerializeField] private TMP_Text resultTxt;


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
            Hide();
            LevelManager.Instance.RestartLevel();
        });

        resumeLevel.onClick.RemoveAllListeners();
        resumeLevel.onClick.AddListener(() => Hide());

    }

    public void ShowResult(bool isWin)
    {
        Show();
        resultTxt.gameObject.SetActive(true);
        resumeLevel.gameObject.SetActive(false);
        if (isWin)
        {
            resultTxt.text = Utility.WIN;
            resultTxt.color = Color.green;

        }
        else
        {
            resultTxt.text = Utility.LOSE;
            resultTxt.color = Color.red;

        }
    }

    public void ShowPause()
    {
        Show();
        resumeLevel.gameObject.SetActive(true);
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
        resultTxt.gameObject.SetActive(false);
    }



}
