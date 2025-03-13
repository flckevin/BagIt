using IngameDebugConsole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugCommand : MonoBehaviour
{
    [ConsoleMethod("goto","Goto given level")]
    public static void LoadLevel(int _level) 
    {
        GameData.Level = _level;
        SceneManager.LoadScene(1);
    }

    [ConsoleMethod("StSwap","Set Swap Amount")]
    public static void IncreaseSwap(int _increase)
    {
        if(SceneManager.GetActiveScene().buildIndex != 1)
        {
            ItemExtension.AddSwap(_increase);
        }
        else if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            ItemExtension.AddSwap(_increase,GameManager.Instance.gameMenu.swapAmountText);
        }
    }

    [ConsoleMethod("ResetData","Reset all progress")]
     public static void Reset()
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.DeleteAll();
        
    }
}
