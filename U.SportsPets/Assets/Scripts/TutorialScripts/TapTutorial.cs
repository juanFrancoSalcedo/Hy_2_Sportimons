using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapTutorial : MonoBehaviour, ITutoriable
{
    [SerializeField] LevelLoader loader;
    [SerializeField] int levelReal;
    public int countMissions { get; set; }
    [SerializeField] int limitMission;

    public void PlusMission()
    {
        countMissions++;
        if (countMissions > limitMission)
        {
            loader.LoadSpecificSceneTransition(levelReal);
        }
    }
}
