using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreatureConfigurator : MonoBehaviour
{
    public SportCreature[] creaturesData;
    public static MeshCreatureConfigurator Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public SportCreature GetRandomCreature()
    {
        return creaturesData[Random.Range( 0, creaturesData.Length)];
    }

    public SportCreature GetPlayerCreature(int indexCompetitor)
    {
        ListPets rockstars = DataSystem<ListPets>.FromJsonWrapper(KeyStorage.THREETEAM_C);
        SportCreature playerCreat = null;
        foreach (SportCreature participant in creaturesData)
        {
            if (participant.idPlayer == rockstars.listPets[indexCompetitor])
            {
                playerCreat = participant;
            }
        }
        return playerCreat;
    }
}
