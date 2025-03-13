using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Quocanh.pattern;

public class EventManger : QuocAnhSingleton<EventManger>
{
    public bool playerWon;
    public void OnWin() 
    { 
        GameManager.Instance.winMenu.SetActive(true);
    }

    public void OnLoose() 
    {
        GameManager.Instance.looseMenu.SetActive(true);
    }

    public void CheckForWin() 
    {
        if (GameManager.Instance.level.avalibleBlock.Count <= 0)
        {
            playerWon = true;
            EventManger.Instance.OnWin();
            
        }
    }
}
