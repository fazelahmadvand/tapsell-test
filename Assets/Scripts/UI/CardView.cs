using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : View
{
    [SerializeField] protected Button btn;
    [SerializeField] protected Image img;
    [SerializeField] protected TMP_Text txt;


    public void UpdateCard(string text, Sprite pic, Action OnClick)
    {
        txt.text = text;
        //img.sprite = pic;

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() =>
        {
            OnClick?.Invoke();
        });


    }

    public void UpdateText(string text)
    {
        txt.text = text;
    }

    public void HideText()
    {
        txt.gameObject.SetActive(false);
    }

    public void SetColor(Color col)
    {
        img.color = col;
    }
    public void ShowText()
    {
        txt.gameObject.SetActive(true);
    }

    public void HandleInteractable(bool isActive)
    {
        btn.interactable = isActive;
    }

}
