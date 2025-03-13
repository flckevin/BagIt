using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData 
{
    //==================================== LEVEL DATA =====================================
    public static int maxLevel = 20;
    private static int level = 0;
    public static int Level 
    {
        get 
        {
            return PlayerPrefs.GetInt("UnlockedLevel", 1);
        }
        set 
        {
            if (level > maxLevel)
            {
                level = 1;
            }
            else 
            {
                level = value;
                PlayerPrefs.SetInt("UnlockedLevel", value);
            }
        }
    }
    //=====================================================================================

    //==================================== PLAYER AUTH DATA =====================================
    public static bool loggedIn_PlayerData;
    public static string playerID_PlayerData;
    public static int playerCoin;
    //=====================================================================================

    //==================================== PLAYER GAME DATA =====================================
    public static Dictionary<string, object> playerData = new Dictionary<string, object>() 
    {
        {"UnlockedLevel",PlayerPrefs.GetInt("UnlockedLevel",0)},
        {"Coin",PlayerPrefs.GetInt("Coin",0)}
    };
    //=====================================================================================


    //================================= DATA SAVE FUNCTION =================================
    public static void SaveReachedLevel()
    {
        // playerData["UnlockedLevel"] = Level;        
        // ServiceManager.Instance.SaveGame_GameData();
    }
    //=====================================================================================
}
