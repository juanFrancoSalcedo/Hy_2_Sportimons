using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public const int LayerInputCollision = 8;
    public const int LayerIgnoreCollisions = 12;
    public static TeamInfo[] teamColor =
        {
        new TeamInfo("Blue", Color.blue),
        new TeamInfo("Cyan", Color.cyan),
        new TeamInfo("Green",Color.green),
        new TeamInfo("Red",Color.red),
        new TeamInfo("White",Color.white),
        new TeamInfo("Yellow",Color.yellow),
        };
}

public class TeamInfo
{
    public string nameTeam;
    public Color colorTeam;

    public TeamInfo(string _name, Color _color)
    {
        this.nameTeam = _name;
        this.colorTeam = _color;
    }
}
