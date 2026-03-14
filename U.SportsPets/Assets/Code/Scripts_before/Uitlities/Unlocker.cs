using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Unlocker : MonoBehaviour
{
    protected ulong initTime;
    public float timeToUnlock = 43200000.0f;

    //FORMULA PARA SABER CUANTOS TICKmILISECONDSsON
    //3600*1000*nHoras
    // 3600*1000*12 = 43200000

    //https://www.youtube.com/watch?v=Yoh6owRXCXA puta el mejor


    public abstract void BeginUnlock();

    protected abstract void UnlockReady();
    
    protected float  CalculateTime()
    {
            ulong diff = ((ulong)DateTime.Now.Ticks) - initTime;
            ulong m = diff / TimeSpan.TicksPerMillisecond;
            float timeLeft =(float)(timeToUnlock- m) / 1000.0f;

        return timeLeft;

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

   
}
