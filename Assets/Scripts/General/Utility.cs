using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public const string SCRIPTABLE_PATH = "SO/";

    private const int EACH_MIN = 60;


    public const string WIN = "Win";
    public const string LOSE = "Lose";


    public static string Timer(float startedTime)
    {
        int sec = (int)startedTime % EACH_MIN;
        int min = (int)startedTime / EACH_MIN;

        string secStr = sec < 10 ? "0" + sec : sec.ToString();
        string minStr = min < 10 ? "0" + min : min.ToString();


        return minStr + " : " + secStr;
    }

    public static void Pause() => Time.timeScale = 0;
    public static void Resume() => Time.timeScale = 1;


}
