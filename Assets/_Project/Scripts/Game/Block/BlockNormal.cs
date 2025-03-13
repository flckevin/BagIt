using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockNormal : BlockCore
{
    
    public override void OnTouch()
    {
        //disable box collision so raycast wont hit
        _box2D.enabled = false;
        //calculate direction to move to
        Vector2 target = (Vector2)this.transform.position + GetDirection(directToGo);
        //shoot raycast from current position to target position
        RaycastHit2D _hit = Physics2D.Raycast(this.transform.position, (target - (Vector2)this.transform.position), 1f, checkLayer);

        Debug.DrawRay(this.transform.position, (target - (Vector2)this.transform.position), Color.green, 2f);

        //if it does not hit anything
        if (_hit.collider == null)
        {
            //keep moving foward
            Flee();
            return;
        }
        else //it does hit something
        {
            #region OLD CODE
            //it hass tag belt which is our conveyor belt
            //if (_hit.collider.CompareTag("Belt"))
            //{
            //    _box2D.enabled = false;
            //    Debug.Log("BELT");
            //    ////set block position to be at belt
            //    //this.transform.position = new Vector3(_hit.transform.position.x, _hit.transform.position.y,this.transform.localPosition.z);
            //    //get new target position to head to
            //    Vector2 _intendedTarget = GetDirection(directToGo);
            //    Vector2 _finalTarget;

            //    //if direction of food is vertical
            //    if (directToGo == Directions.up || directToGo == Directions.down)
            //    {
            //        //only on move y axis
            //        _finalTarget = new Vector2(this.transform.localPosition.x, _hit.collider.transform.position.y);
            //    }
            //    else //direction is horizontal
            //    {
            //        //only on move x axis
            //        _finalTarget = new Vector2(_hit.collider.transform.position.x, this.transform.localPosition.y);
            //    }

            //    _blockRend.sortingOrder += 2;
            //    foodImage.sortingOrder += 2;

            //    //move foward
            //    this.gameObject.transform.DOMove(_finalTarget, blockSpeed).OnComplete(() => 
            //    {
            //        #region OLD CODE FOR BELT MOVING
            //        ////get all path of the belt
            //        //Transform[] _path = GameManager.Instance.level.beltPath;
            //        ////declare float to store nearest distance between block and path
            //        //float _closetPath = -1;
            //        ////store nearest path id in array
            //        //int nearestPathID = 0;
            //        ////loop every path
            //        //for (int i = 0; i < _path.Length - 1; i++)
            //        //{
            //        //    //get distance between block and the path that we are checking
            //        //    float _dist = Vector2.Distance(this.transform.position, _path[i].position);
            //        //    //if current path checking is closer or there been no path in storage
            //        //    if (_dist < _closetPath || _closetPath == -1)
            //        //    {
            //        //        //store closest path
            //        //        _closetPath = _dist;
            //        //        Debug.Log($"PATH {GameManager.Instance.level.beltPath[i].name} LENGTH {Vector2.Distance(this.transform.position, _path[i].position)}");
            //        //        //store that path id
            //        //        nearestPathID = i;
            //        //    }
            //        //}
            //        ////move to nearest path
            //        //BeltMove(nearestPathID);
            //        #endregion

            //        //add into food slot
            //        GameManager.Instance.level.SlotAddFood(this);
            //    });
            //    return;
            //}

            #endregion

            //if raycast hit slot, add it in instantly
            if (_hit.collider.CompareTag("Slots") || _hit.collider.CompareTag("Belt")) 
            {
                _trail.enabled = false;
                _box2D.enabled = false;
                Debug.Log("HIT SLOT");
                //add food to slot
                GameManager.Instance.level.SlotAddBlock(this);
                return;
            }
            ////enable box
            //_box2D.enabled = true;
        }

        _box2D.enabled = true;

        base.OnTouch();
    }

    /// <summary>
    /// function to keep moving foward
    /// </summary>
    private void Flee() 
    {
        //get new target position to head to
        Vector2 _intendedTarget = GetDirection(directToGo);
        //fianl target after calculation
        Vector2 _finalTarget = new Vector2(this.transform.localPosition.x + _intendedTarget.x, this.transform.localPosition.y + _intendedTarget.y);


        //move foward
        this.gameObject.transform.DOLocalMove(_finalTarget, blockSpeed).OnComplete(()=>
        {
            //repeat process again
            OnTouch();
        }).SetEase(Ease.Linear);
    }

    #region OLD CODE FOR BELT MOVING
    //private void BeltMove(int _pathID) 
    //{
    //    //********** pseudocode *******
    //    //find closet position
    //    //get the closet position to  the block
    //    //move the block but with the position that next to the closet position as target
    //    //then find next posiiotn after reach it target
    //    //then continue to move alon
    //    //********** pseudocode *******
    //    //move foward

    //    //if there is still path to go
    //    if (_pathID < GameManager.Instance.level.beltPath.Length - 1)
    //    {
    //        _pathID += 1;

    //        this.gameObject.transform.DOMove(GameManager.Instance.level.beltPath[_pathID].position, 1).OnComplete(() =>
    //        {
    //            Debug.Log(_pathID);
    //            //repeat process again
    //            BeltMove(_pathID);

    //        }).SetEase(Ease.Linear);
    //        return;
    //    }
    //    else //end of the path
    //    {
    //        //add food to slot
    //        GameManager.Instance.level.SlotAddFood(this);
    //    }


    //}
    #endregion
}
