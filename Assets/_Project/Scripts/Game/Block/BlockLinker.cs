using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockLinker : BlockCore
{
    [Header("MOVE TILE INFO")]
    public BlockLinker tileConnect; // store all of the tiles that connected

    #region TILES CONNECTION VAR
    private GameObject lockG;
    
    #endregion

    #region CONNECTED TILES VAR
    public List<BlockLinker> connectedTiles = new List<BlockLinker>(); // list of aall connected tile to this tile
    #endregion

    public override void Start()
    {
        ConnectToTiles(); 
        base.Start();
    }

    /// <summary>
    /// function to connect to other tiles
    /// </summary>
    private void ConnectToTiles() 
    { 
        if(tileConnect != null) 
        {
            //calculating correct position between this object and target
            Vector3 positionToSpawn = (this.transform.position + (tileConnect.transform.position - this.transform.position) * 0.5f);
            //spawn locker between them to display it
            lockG = Instantiate(GameManager.Instance.locker, positionToSpawn, Quaternion.identity);
            //change sorting order so the lock can use half of image to face to correct block
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder += 1;
            //add current tile to tile that being connect to this
            tileConnect.connectedTiles.Add(this);
        }
        
    }



    public override void OnTouch()
    {
        _box2D.enabled = false;
        //calculate direction to move to
        Vector2 target = (Vector2)this.transform.position + GetDirection(directToGo);
        //shoot raycast from current position to target position
        RaycastHit2D _hit = Physics2D.Raycast(this.transform.position, (target - (Vector2)this.transform.position),1,checkLayer);

        Debug.DrawRay(this.transform.position, (target - (Vector2)this.transform.position), Color.green,2f);



        //if infront of the block hit nothing and it is clear to go
        if (_hit.collider == null && connectedTiles.Count <= 1)
        {
            //if theres no other block connecting to itslef
            if (tileConnect == null)
            {
                //flee
                Flee();
                Debug.Log("MOVING");
                return;
                
            }
            else
            {
                //calculate direction to move to
                Vector2 targetReverse = (Vector2)this.transform.position + (GetDirection(directToGo)*-1);
                //shoot raycast from current position to target position
                RaycastHit2D _hitReverse = Physics2D.Raycast(this.transform.position, (targetReverse - (Vector2)this.transform.position), 1, checkLayer);
                //if the reverse raycast does hit something and that tile same as connecting to this tile 
                if (_hitReverse.collider != null && _hitReverse.transform.GetComponent<BlockLinker>() == tileConnect) 
                {
                    //flee
                    Flee();
                }
            }
            ////if there is only 1
            //else if (tilesConnected.Count == 1) 
            //{
            //    //if that tile is opposite with this tile
            //    //then move foward

            //    //move foward
            //    this.gameObject.transform.DOMove(GetDirection(directToGo) * 100, 1f);
            //    Debug.Log("MOVING");
            //}

        }

        //re enable box
        _box2D.enabled = true;
        base.OnTouch();
    }

    private void Flee() 
    {
        //move foward
        this.gameObject.transform.DOMove(GetDirection(directToGo) * 100, 1f);

        if (lockG != null) 
        {
            lockG.SetActive(false);
        }

        if (tileConnect != null) 
        {
            tileConnect.connectedTiles.Remove(this);
        }

        if(connectedTiles.Count > 0) 
        { 
            for(int i =0;i<connectedTiles.Count;i++) 
            {
                connectedTiles[i].tileConnect = null;
                connectedTiles[i].lockG.SetActive(false);
            }
        }

    }

   
}


