using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    [Header("SLOT GENERAL INFO"),Space(10)]
    public BlockCore blockHolding;

    [Header("SLOT UNLOCK - ONLY FILL IN IF YOU WANT TO LOCK THIS SLOT"), Space(10)]
    public GameObject locker;

    /// <summary>
    /// function to unlock slot
    /// </summary>
    public void Unlock() 
    { 
        //deactivate locker
        locker.SetActive(false);
        //set scalre to zero as value of start
        this.transform.localScale = Vector3.zero;
        //do animation of unlocking slot
        this.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(() => 
        {
            //add slot to level
            GameManager.Instance.level.slots.Add(this);
        });
        
        
    }
}
