using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//--                            _ooOoo_
//--                           o8888888o
//--                           88" . "88
//--                           (| -_ - |)
//--                            O\ = /O  
//--                        ____/`---'\____  
//--                      .   ' \\| |-- `.  
//--                       / \\||| : ||| -- \  
//--                     / _ ||||| -:- ||||| - \  
//--                       | | \\\ ---/ | |
//--                     | \_ | ''\---/ '' | |
//--                      \ .-\__ `-` ___/-. /  
//--                   ___`. .' /--.--\ `. . __  
//--                ."" '< `.___\_<|>_/___.' >'"".  
//--               | | : `- \`.;`\ _ /`;.`/ - ` : | |
//--                 \ \ `-. \_ __\ /__ _/ .-` / /  
//--         ======`-.____`-.___\_____/___.-`____.-'======  
//--                            `=---='  
//--  
//--         ............................................. 

public class Controller : MonoBehaviour
{

    public LayerMask mask; // mask so can identify main object we only need
    public bool isUsingItem; // identify whether player using item
    private Camera _cam; // camera on screen so we can get it position
    
    // Start is called before the first frame update
    void Start()
    {
        //storing camera
        _cam = Camera.main;
        //assign controller to manager
        GameManager.Instance.playerController = this;
    }

    // Update is called once per frame
    void Update()
    {
        switch (isUsingItem) 
        {
            case false:
                RaycastTouch();
                break;
            case true:
                UsingItem();
                break;
        }
        
    }

    /// <summary>
    /// function of touch controller
    /// </summary>
    private void RaycastTouch() 
    {
        //if there is no touch then stop
        if (Input.touchCount <= 0) return;

        #region LOCAL VARIBLES
        //============================ INITIATE LOCAL VARIBLES ==========================

        //get the first touch position
        Touch _touch = Input.GetTouch(0);
        //create raycast from camera shoot down to world with mask condition
        RaycastHit2D _hit = Physics2D.Raycast(_cam.ScreenToWorldPoint(_touch.position), Vector2.zero, mask);

        //============================ INITIATE LOCAL VARIBLES ==========================
        #endregion

        //if the player just only touch their finger on screen
        if (_touch.phase == TouchPhase.Began) 
        { 
            //if the touch position that the finger just touch raycast does hit something
            if(_hit.collider != null) 
            {
                //if player hit a tile
                if (_hit.collider.gameObject.CompareTag("Tile")) 
                {
                    //get block core and call on touch function
                    _hit.collider.gameObject.GetComponent<BlockCore>().OnTouch();
                }
            }
        }
    }


    /// <summary>
    /// function of using item
    /// </summary>
    private void UsingItem() 
    {
        //if there is no touch then stop
        if (Input.touchCount <= 0) return;

        switch (ConsumableItems.itemType) 
        {
            case (ItemType.Swap):

                #region SWAP ITEM

                //============================ INITIATE LOCAL VARIBLES ==========================

                //get the first touch position
                Touch _touch = Input.GetTouch(0);
                //create raycast from camera shoot down to world with mask condition
                RaycastHit2D _hit = Physics2D.Raycast(_cam.ScreenToWorldPoint(_touch.position), Vector2.zero, mask);

                //============================ INITIATE LOCAL VARIBLES ==========================

                //if the player just only touch their finger on screen
                if (_touch.phase == TouchPhase.Began)
                {
                    //if the touch position that the finger just touch raycast does hit something
                    if (_hit.collider != null)
                    {
                        //if player hit a tile
                        if (_hit.collider.gameObject.CompareTag("Tile"))
                        {
                            ConsumableItems.Swap(_hit.transform.GetComponent<BlockCore>());
                        }
                    }
                }

                #endregion

            break;

            case (ItemType.Slot):

                ConsumableItems.Unlock();

                break;
        }

    }
}
