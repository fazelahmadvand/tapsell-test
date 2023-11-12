using System;
using System.Collections;
using System.Collections.Generic;
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
        btn.onClick.AddListener(() => OnClick?.Invoke());



    }


}
