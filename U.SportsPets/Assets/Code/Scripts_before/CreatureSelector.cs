using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class CreatureSelector : MonoBehaviour
{
    public ListPets myThree;

    private void Start()
    {
        SavePets();
        LoadPets();
    }

    [ButtonMethod]
    void SavePets()
    {
        // Test------
        myThree.listPets.Add(1);
        myThree.listPets.Add(4);
        myThree.listPets.Add(6);
        DataSystem<ListPets>.ToJsonWrapper(myThree,KeyStorage.THREETEAM_C);
    }
    [ButtonMethod]
    void LoadPets()
    {
        myThree.listPets.Clear();
        myThree = DataSystem<ListPets>.FromJsonWrapper(KeyStorage.THREETEAM_C);
    }
}
