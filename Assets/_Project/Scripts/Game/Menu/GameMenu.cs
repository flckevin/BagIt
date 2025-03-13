using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    private bool isPausing; // bool to identify if the game is pausing

    [Header("Item_UI_SWAP")]
    public TextMeshProUGUI swapAmountText;
    public GameObject adIconSwap;

    [Space(10)]
    [Header("Item_UI_SWAP")]
    public TextMeshProUGUI slotAmountText;
    public GameObject adIconSlot;


    #region ================================ MAIN MENU ======================================

    /// <summary>
    /// function to load scene
    /// </summary>
    /// <param name="_sceneIndex"> scene id </param>
    public void LoadScene(int _sceneIndex) 
    {
        //load to that scene
        SceneManager.LoadScene(_sceneIndex);
        //reset time incase if game is freezing
        Time.timeScale = 1.0f;  
    }

    public void MoveToNextLevel() 
    {
        //Increase level have reached
        GameData.Level += 1;
        //save data
        GameData.SaveReachedLevel();
        //reload current scene with next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //reset time to normal
        Time.timeScale = 1;
    }

    /// <summary>
    /// function to pause game
    /// </summary>
    public void PauseGame() 
    {
        //check if game is pausing

        //if it not
        if (!isPausing)
        {
            //activate pause menu
            GameManager.Instance.pauseMenu.SetActive(true);
            //freeze time
            Time.timeScale = 0;
            //set is pause to true so it can identify
            isPausing = true;
        }
        else // if it ise
        {
            //deactivate pause menu
            GameManager.Instance.pauseMenu.SetActive(false);
            //freeze time
            Time.timeScale = 1;
            //set os pausing to false so it can identify
            isPausing = false;
        }
    }

    /// <summary>
    /// function to restart game
    /// </summary>
    public void RestartGame() 
    {
        //set time scale to 1 incase if the game is freezing
        Time.timeScale = 1.0f;
        //reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion

    #region ============================= ITEMS ===================================

    /// <summary>
    /// function to call swap item
    /// </summary>
    public void ItemSwap() 
    {
        //if swap item amount less than 0
        if(ItemData.SwapAmount <= 0) 
        {
            //call pannel of confirmation
            return;
        }
        
        //setup call back
        ConsumableItems.callBackOnUse = () => 
        {
            //decrease amount of swap item
            ItemExtension.DecreaseSwap(swapAmountText);
            //if swap amount less than 0
            if(ItemData.SwapAmount <= 0) 
            {
                //deactivate swap amount text
                swapAmountText.gameObject.SetActive(false);
                //activate icon swap
                adIconSwap.SetActive(true);
            }
            
        };

        //set item type to swap
        ConsumableItems.itemType = ItemType.Swap;
        //set player controller is using item to true so they can switch to item controller
        GameManager.Instance.playerController.isUsingItem = true;
        //activate text
        GameManager.Instance.itemInstruction.gameObject.SetActive(true);
        //disply instrction
        GameManager.Instance.itemInstruction.text = "SELECT 2 FOOD TO SWAP";
    }

    /// <summary>
    /// function to call item slot
    /// </summary>
    public void ItemAddSlot() 
    {
        //if slot item amount less than 0
        if(ItemData.SlotAmount <= 0) 
        {
            //call pannel of confirmation
            return;
        }

         //setup call back
        ConsumableItems.callBackOnUse = () => 
        {
            //decrease amount of swap item
            ItemExtension.DecreaseSlot(slotAmountText);
            //if swap amount less than 0
            if(ItemData.SwapAmount <= 0) 
            {
                //deactivate swap amount text
                slotAmountText.gameObject.SetActive(false);
                //activate icon swap
                adIconSlot.SetActive(true);
            }
            
        };

        //set item type to slot 
        ConsumableItems.itemType = ItemType.Slot;
        //set player controller is using item to true so they can switch to item controller
        GameManager.Instance.playerController.isUsingItem = true;
    }

    /// <summary>
    /// reset item function
    /// </summary>
    /// <param name="_itemType"> item type </param>
    public void ItemReset(ItemType _itemType) 
    {
        
        //set item type to be the same
        ConsumableItems.itemType = _itemType;
        //reset item
        ConsumableItems.Reset();
    }

    #endregion


    
}
