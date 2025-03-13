using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.State;

public class NPCBehaviour : MonoBehaviour,IState
{
    public BlockTray[] blockTray; // all of the block that currently on tray
    public float npcSpeed;
    
    [HideInInspector] public bool ordering; // boolean to detect whether they are ordering
   
    #region AI STATES
    [HideInInspector]public OnEnterState _onEnterState = new OnEnterState(); // AI STATE _ when enter to restaurant
    [HideInInspector]public OnFind _onFindState = new OnFind();// AI STATE _ when finding correct pieces
    [HideInInspector] public OnendState _onEndState = new OnendState();// AI STATE_when end their ordering
    #endregion

    public int reqBlockCount = 0; // type of order that customer want
    //[HideInInspector] public List<int> reqID = new List<int>(); // store order ID so AI state can remove correct order later on
    private IState _state; // store state to execute
    //[HideInInspector] public int blockReceived; // received block

    // Start is called before the first frame update
    void Start()
    {
        //adding customer into the level
        GameManager.Instance.level.avalibleNpc.Add(this);
        //calling state excution function
        DoState(_onEnterState);
    }

    /// <summary>
    /// function to execute state
    /// </summary>
    /// <param name="_stateToDo"></param>
    public void DoState(IState _stateToDo) 
    { 
        _state = _stateToDo; // set state to execute
        _state.ExecuteState(this); // execute new state
    }

    /// <summary>
    /// function to execute state from interface
    /// </summary>
    /// <param name="_npc"> npc class </param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void ExecuteState(NPCBehaviour _npc)
    {
        throw new System.NotImplementedException();
    }
}
