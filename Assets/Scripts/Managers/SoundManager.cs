using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource click;
    [SerializeField] private AudioSource perfectChoice;
    [SerializeField] private AudioSource win;
    [SerializeField] private AudioSource lose;


    public void Click()
    {
        click.Play();
    }

    public void Perfect()
    {
        perfectChoice.Play();
    }

    public void Win() => win.Play();
    public void Lose() => lose.Play();

}
