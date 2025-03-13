using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace NPC.State 
{

    /// <summary>
    /// State when npc enter to restaurant
    /// </summary>
    public class OnEnterState : IState
    {
        /// <summary>
        /// State execute function
        /// </summary>
        /// <param name="_npc"> npc class </param>
        public void ExecuteState(NPCBehaviour _npc)
        {
            //_npc.blockReceived = 0;
            //move our npc to ordering position 
            _npc.transform.DOMove(GameManager.Instance.level.npcPosition.position, _npc.npcSpeed).OnComplete(() =>
            {
                _npc.reqBlockCount = 0;
                //when it moved to desiered posiiton
                //get all block that in npc slots
                for (int i = 0; i < _npc.blockTray.Length; i++)
                {
                    //Debug.Log($"NPC {GameManager.Instance.level.avalibleBlock.Count}");
                    //if the i number is larger than block avability we shall break the loop so it won't break the game
                    if (i > GameManager.Instance.level.avalibleBlock.Count - 1) break;
                    Debug.Log($"NPC CURRENT SLOT {i} AVABILITY BLOCK {GameManager.Instance.level.avalibleBlock.Count}");
                    
                    //Setting up randomized block info
                    _npc.blockTray[i].SetInfo
                    (
                        GameManager.Instance.level.avalibleBlock[i].blockColor,
                        GameManager.Instance.level.avalibleBlock[i].blockCupSpriteNoCap,
                        GameManager.Instance.level.avalibleBlock[i].blockCupSpriteWCap,
                        false
                    );

                    //activate block on block tray
                    _npc.blockTray[i].gameObject.SetActive( true );
                    //increase amount of requierment block so we can check later on
                    _npc.reqBlockCount++;
                    //scale block down to 0 so we can do scale animation
                    _npc.blockTray[i].transform.localScale = new Vector3(0, 0, 0);
                    //scale animation
                    _npc.blockTray[i].transform.DOScale(new Vector3(0.7f, 0.7f, _npc.blockTray[i].transform.localScale.z),0.2f).SetEase(Ease.InOutElastic);
                    
                    //set block and arrow oppacity back to default
                    //_npc.blockTray[i].arrowSprite.color = new Color(1, 1, 1,110f/255);
                    //_npc.blockTray[i].blockSprite.color = new Color(1, 1, 1, 110f / 255);
                  
                    #region OLD CODE
                    //random order going to be call

                    //get color name
                    //string colorName = Enum.GetName(typeof(BlockColor), GameManager.Instance.level.avalibleBlock[i].blockColor);
                    //Debug.Log($"NPC {colorName}");



                    //#region BLOCK SRPITE SETUP
                    ////scale object back to normal
                    //_npc.blockSprite[i].transform.localScale = new Vector3(0.7f, 0.7f, _npc.blockSprite[i].transform.localScale.z);
                    ////activate block
                    //_npc.blockSprite[i].gameObject.SetActive(true);
                    //#endregion

                    //#region BLOCK ARROW SETUP
                    ////change direction of arrow on block
                    //NPCStateHelperExtensions.OnCheckArrowDirection(GameManager.Instance.level.avalibleBlock[i].directToGo, _npc.blockArrow[i].gameObject);
                    //Debug.Log($"DIRECTION {GameManager.Instance.level.avalibleBlock[i].directToGo}");
                    //#endregion
                    #endregion


                    // _npc.blockReceived++;

                }
                
                _npc.ordering = true;
                Debug.Log("DONE CHECKING");
                //execute new state which is order state
                //to check if there order that already on slot that matches current order that can be taken instantly
                _npc.DoState(_npc._onFindState);

            });

            
        }

    }


    /// <summary>
    /// State when npc order
    /// </summary>
    public class OnFind : IState
    {
        /// <summary>
        /// function to execute state
        /// </summary>
        /// <param name="_npc"> npc </param>
        public void ExecuteState(NPCBehaviour _npc)
        {
            if (_npc.ordering == false) return;

            Debug.Log("ORDERING");

            //loop all of the kitchen slot to check if our order is out
            for (int i = 0; i < GameManager.Instance.level.slots.Count; i++)
            {
                //if slot have a requierment block
                if (GameManager.Instance.level.slots[i].blockHolding != null) 
                {
                    //find the block that has it on the slot
                    for(int y =0; y < _npc.blockTray.Length; y++) 
                    {
                        Debug.Log($"SLOT {GameManager.Instance.level.slots[i].name}");
                        //if the type of block match with it
                        if (_npc.blockTray[y].taken == false &&
                            GameManager.Instance.level.slots[i].blockHolding.blockColor == _npc.blockTray[y].color)
                        {
                            //set taken to be true so when checking it won't be overlap
                            _npc.blockTray[y].taken = true;

                            //remove ordered food from slot so customer won't order it again incase restaurant ran out of that food
                            GameManager.Instance.level.avalibleBlock.Remove(GameManager.Instance.level.slots[i].blockHolding);
                            //_npc.reqBlock.Remove(_npc.reqBlock[y]);
                            

                            #region SLOT BEHAVIOUR

                            //get that slot
                            Slots _slot = GameManager.Instance.level.slots[i];
                            //deactivate the food on the slot
                            _slot.blockHolding.gameObject.SetActive(false);   
                            //set slot to be empty
                            _slot.blockHolding = null;

                            #endregion

                            #region NPC REQ BLOCK

                            //_npc.block[]
                            _npc.blockTray[y].transform.localScale = new Vector3(0.9f, 0.9f, _npc.blockTray[y].transform.localScale.z);
                            //set opacity back to normal of block and arrow
                            //_npc.blockTray[y].blockSprite.color = new Color(1,1,1,1);
                            //change to cup sprite
                            _npc.blockTray[y].blockSprite.sprite = _npc.blockTray[y].blockCupSprite;
                            //_npc.blockTray[y].arrowSprite.color = new Color(1,1,1,1);

                            //small down block till it dissapeared
                            _npc.blockTray[y].transform.DOScale(new Vector3(0.7f, 0.7f, _npc.blockTray[y].transform.localScale.z), 0.3f).OnComplete(() => 
                            {
                                
                                //decrease requierment block count
                                _npc.reqBlockCount--;
                                //_npc.blockReceived--;
                                if (_npc.reqBlockCount <= 0)
                                {
                                    //re do state
                                    _npc.DoState(_npc._onEndState);
                                    //check if player have won
                                    //EventManger.Instance.CheckForWin();
                                }

                              
                            });

                            break;
                            #endregion


                        }
                    }
                }
                
               

            }

            
        }
    }


    public class OnendState : IState
    {
        public void ExecuteState(NPCBehaviour _npc)
        {
            //check if player won
            EventManger.Instance.CheckForWin();
            _npc.ordering = false;
            //do sequence
            DOTween.Sequence()
            .Append(_npc.transform.DOMove(GameManager.Instance.level.npcEndPosition.position, _npc.npcSpeed)) // move npc to position
            .SetDelay(0.3f) // delay 1 second
            .OnComplete(() => 
            {
                //if player not won yet
                if (EventManger.Instance.playerWon == false)
                {
                    //loop all block tray
                    for (int i = 0; i < _npc.blockTray.Length; i++)
                    {
                        //deactivate all of it
                        _npc.blockTray[i].gameObject.SetActive(false);
                        //set taken to be true so when checking it won't be overlap
                        _npc.blockTray[i].taken = false;
                    }
                    //set npc position to start position
                    _npc.transform.position = GameManager.Instance.level.npcStartPosition.position;
                    //repeat process
                    _npc.DoState(_npc._onEnterState);
                }
                
            });

            
        }
    }


}