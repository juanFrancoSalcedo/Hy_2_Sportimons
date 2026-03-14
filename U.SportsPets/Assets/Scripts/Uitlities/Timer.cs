using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public float initTime;
    public bool rest;
    public event System.Action<string,float> OnCalculatedTimeString;
    public bool stopTimer { get; set; } = false;
    public TypeTimer timerType;
    public enum TypeTimer
    {
        Second,
        Thousandths,

    }

    //FORMULA PARA SABER CUANTOS TICKmILISECONDSsON
    //3600*1000*nHoras
    // 3600*1000*12 = 43200000

    //https://www.youtube.com/watch?v=Yoh6owRXCXA puta el mejor


    private void Update()
    {
        if (stopTimer) { return; }

        if (rest)
        {
            initTime -= Time.deltaTime;
        }
        else
        {
            initTime += Time.deltaTime;
        }

        CheckTimerType();
    }

    private void CheckTimerType()
    {
        switch (timerType)
        {
            case TypeTimer.Second:
                OnCalculatedTimeString?.Invoke(GetTimeOnlySeconds(initTime),initTime);
                break;
            case TypeTimer.Thousandths:
                OnCalculatedTimeString?.Invoke(GetTimeThousandths(initTime),initTime);
                break;
        }
    }


    protected string GetTime(float time)
    {
        string strHoras = "";
        int horas = (int)time / 3600;

        strHoras = "" + horas;

        if (horas < 10)
        {
            strHoras = "0" + horas;
        }
        if (horas >= 24)
        {
            strHoras = "paila";
        }
        if (horas < 1)
        {
            strHoras = "00";
        }

        string strMin = "";
        int min = (int)time / 60;
        strMin = "" + min;

        if (min < 10)
        {
            strMin = "0" + min;
        }
        if (min >= 60)
        {
            int bufMin = (int)min - horas * 60;

            if (bufMin < 10)
            {
                strMin = "0" + bufMin;
            }
            else
            {
                strMin = "" + bufMin;
            }
        }

        string strSec = "";
        int sec = (int)time - (min * 60);

        if (sec < 10)
        {
            strSec = "0" + sec;
        }
        else
        {
            strSec = "" + sec;
        }
        return strHoras + ":" + strMin + ":" + strSec;
    }

    protected string GetTimeOnlySeconds(float time)
    {
        int min = (int)time / 60;
        string strSec = "";
        int sec = (int)time - (min * 60);

        if (sec < 10)
        {
            strSec = "0" + sec;
        }
        else
        {
            strSec = "" + sec;
        }

        return strSec;
    }

    protected string GetTimeThousandths(float time)
    {
        int min = (int)time / 60;
        string strSec = "";
        int sec = (int)time - (min * 60);

        if (sec < 10)
        {
            strSec = "0" + sec;
        }
        else
        {
            strSec = "" + sec;
        }
        return time.ToString("0.000");
    }
}
