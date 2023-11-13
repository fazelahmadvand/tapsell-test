using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : View
{

    [SerializeField] private CardView card;
    [SerializeField] private Transform parent;


    private readonly List<CardView> cards = new();

    public List<CardView> Cards => cards;

    public void CreateCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var c = Instantiate(card, parent);
            c.InitView();
            cards.Add(c);
        }
    }

    public void HandleInteractable(bool isActive)
    {
        foreach (var c in cards)
        {
            c.HandleInteractable(isActive);
        }
    }

    public void MakeThemWhite()
    {
        foreach (var item in cards)
        {
            item.SetColor(Color.white);
        }
    }

    public void HideAll()
    {
        foreach (var c in cards)
            c.Hide();
    }

}
