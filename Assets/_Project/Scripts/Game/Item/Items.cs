using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class ConsumableItems 
{
    //========================== GENERAL ITEM INFO =======================
    public static ItemType itemType;
    public static Action callBackOnUse;
    //========================== GENERAL ITEM INFO =======================

    //========================== SWAP ITEM VAR =======================
    public static List<BlockCore> _blockList = new List<BlockCore>();
    //========================== SWAP ITEM VAR =======================

    /// <summary>
    /// function to swap block
    /// </summary>
    /// <param name="_block"> block varible </param>
    public static void Swap(BlockCore _block) 
    {
        //if block not contain given block
        if (!_blockList.Contains(_block)) 
        {
            //add new block
            _blockList.Add(_block);
            //get sprite render to color it red
            _block.GetComponent<SpriteRenderer>().color = Color.red;
            Debug.Log(_block.name);
        }
        //if block list count is less than 1
        if (_blockList.Count < 2) return;

        //store first block and sedcod second block postion
        Vector3 _firstBlock = _blockList[0].transform.localPosition;
        Vector3 _secBlock = _blockList[1].transform.localPosition;

        //set sprite color back to normal
        _blockList[0].GetComponent<SpriteRenderer>().color = new Color(1,1,1);
        _blockList[1].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);

        //increase sorting order so it can go thorugh block underneath
        _blockList[0].arrowImage.sortingOrder += 2;
        _blockList[0]._blockRend.sortingOrder += 2;
        
        _blockList[1].arrowImage.sortingOrder += 2;
        _blockList[1]._blockRend.sortingOrder += 2;

        //swap block position
        _blockList[0].transform.DOLocalJump(_secBlock,1,1,0.5f);
        //swap block position
        _blockList[1].transform.DOLocalJump(_firstBlock,1,1,0.5f).OnComplete(() => 
        {
            //give it back to normal
            _blockList[0].arrowImage.sortingOrder -= 2;
            _blockList[0]._blockRend.sortingOrder -= 2;

            _blockList[1].arrowImage.sortingOrder -= 2;
            _blockList[1]._blockRend.sortingOrder -= 2;


        });

        Reset();

        if(callBackOnUse != null)
        {
            callBackOnUse();
        }
    }

    /// <summary>
    /// function to unlock another slot
    /// </summary>
    public static void Unlock() 
    {
        //if there is still slot to unlock
        if (GameManager.Instance.level.lockedSlot.Count > 0)
        {
            //unlock it
            GameManager.Instance.level.lockedSlot[0].Unlock();
            //activate locked slot
            GameManager.Instance.level.lockedSlot[0].gameObject.SetActive(true);
            // remove locked slot
            GameManager.Instance.level.lockedSlot.Remove(GameManager.Instance.level.lockedSlot[0]);
        }
        else //no lock left
        { 
            //display slot reaches limit
        }

        Reset();

        if(callBackOnUse != null)
        {
            callBackOnUse();
        }
        
    }

    /// <summary>
    /// function to reset everythin
    /// </summary>
    public static void Reset() 
    {
        //set is using item to false in controller so player can shoot block
        GameManager.Instance.playerController.isUsingItem = false;

        //check item type
        switch (itemType) 
        {
            //swap type
            case ItemType.Swap:
                //activate text
                GameManager.Instance.itemInstruction.gameObject.SetActive(false);
                //clear whole block list
                _blockList.Clear();
                break;
        }
    }
}

public enum ItemType 
{ 
    Swap,
    Slot
}

public static class ItemData
{
    public static int SwapAmount
    {
        get{return _swapAmount;}

        set
        {
            //if swap amount going to be less than 0
            if(_swapAmount < 0)
            {
                // set it to be 0
                _swapAmount = 0;
            }
            else // if it not
            {
                //set it to has new value
                _swapAmount = value;
            }
            //save it local
            PlayerPrefs.SetInt("SwapAmount",value);
        }
    }
    private static int _swapAmount;

    public static int SlotAmount
    {
        get{return _slotAmount;}

        set
        {
            //if slot amount going to be less than 0
            if(_slotAmount < 0)
            {
                // set it to be 0
                _slotAmount = 0;
            }
            else // if it not
            {
                //set it to has new value
                _slotAmount = value;
            }
            //save it local
            PlayerPrefs.SetInt("SlotAmount",value);
        }
    }
    private static int _slotAmount;
}

public static class ItemExtension
{
    public static void AddSwap(int _amountToAdd, TextMeshProUGUI _displayText = null)
    {
        ItemData.SwapAmount += _amountToAdd;
        if(_displayText == null) return;
        _displayText.text = ItemData.SwapAmount.ToString();
    }

    public static void DecreaseSwap(TextMeshProUGUI _displayText = null)
    {
        ItemData.SwapAmount--;
        if(_displayText == null) return;
        _displayText.text = ItemData.SwapAmount.ToString();
    }

    public static void AddSlot(int _amountToAdd, TextMeshProUGUI _displayText = null)
    {
        ItemData.SlotAmount += _amountToAdd;
        if(_displayText == null) return;
        _displayText.text = ItemData.SlotAmount.ToString();
    }

    public static void DecreaseSlot(TextMeshProUGUI _displayText = null)
    {
        ItemData.SlotAmount--;
        if(_displayText == null) return;
        _displayText.text = ItemData.SlotAmount.ToString();
    }
}