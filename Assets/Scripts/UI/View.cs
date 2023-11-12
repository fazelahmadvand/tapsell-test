using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] protected GameObject root;


    public virtual void InitView()
    {
        Hide();
    }


    public virtual void Hide()
    {
        if (root)
            root.SetActive(false);
    }

    public virtual void Show()
    {
        if (root)
            root.SetActive(true);
    }

}
