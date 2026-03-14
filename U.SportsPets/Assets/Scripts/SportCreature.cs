using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Creature", menuName = "Inventory/Creature")]
public class SportCreature : ScriptableObject
{
    [Range(1,10)]
    public int force;
    [Range(1, 10)]
    public int luck;
    [Range(1, 10)]
    public int speed;
    [Range(1, 10)]
    public int accuracy;
    [Range(1, 10)]
    public int spirit;
    [Range(1, 10)]
    public int reflexes;
    [Range(1, 10)]
    public int endurance;
    public GameObject meshCreature;
    public int idPlayer;
}
