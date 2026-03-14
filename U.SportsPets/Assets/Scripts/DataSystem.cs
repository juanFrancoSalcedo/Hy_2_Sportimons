using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DataSystem<T>
{

    public static void SaveMoney(int newMoney)
    {
        PlayerPrefs.SetInt(KeyStorage.MONEY_I,newMoney);
    }

    public static int LoadMoney()
    {
        return PlayerPrefs.GetInt(KeyStorage.MONEY_I,0);
    }

    public static void SaveLevel(int _levelIndex)
    {
        if (_levelIndex > 0 || SceneManager.sceneCount-2 >_levelIndex)
        {
            PlayerPrefs.SetInt(KeyStorage.LASTLEVEL_I, _levelIndex);
        }
    }

    public static int LoadLevel()
    {
        return PlayerPrefs.GetInt(KeyStorage.LASTLEVEL_I,1);
    }

    public static void  SaveSoundState(bool state)
    {
        PlayerPrefs.SetInt(KeyStorage.SOUNDSTATE_I, (state)?1:0);
    }

    public static bool LoadSoundState()
    {
        if (PlayerPrefs.GetInt(KeyStorage.SOUNDSTATE_I) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void ToJsonWrapper(T arg1, string keyStorageReference)
    {
        PlayerPrefs.SetString(keyStorageReference, JsonUtility.ToJson(arg1));
        Debug.Log(PlayerPrefs.GetString(keyStorageReference));
    }

    public static T FromJsonWrapper(string keyStorageReference)
    {
        return JsonUtility.FromJson<T>(PlayerPrefs.GetString(keyStorageReference));
    }

    public static void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }
}

[System.Serializable]
public class ListPets
{
    public List<int> listPets = new List<int>();
}

public static class WrapperData<T>
{
    public static string ToJsonSimple(T arg1)
    {
        return JsonUtility.ToJson(arg1);
    }

    public static T FromJsonsimple(string arg1)
    {
        return JsonUtility.FromJson<T>(arg1);
    }
}

public static class KeyStorage
{
    public static string LASTLEVEL_I = "LASTLEVEL";
    public static string SOUNDSTATE_I = "SOUNDSTATE";
    public static string FIRSTOPEN_I = "FIRSTOPEN";
    public static string THREETEAM_C = "THREETEAM";
    public static string MONEY_I = "MONEY";
}
