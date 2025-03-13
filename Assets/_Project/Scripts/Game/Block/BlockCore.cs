using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCore : MonoBehaviour
{
    [InfoBox("THIS SCRIPT ONLY DEV CAN TOUCH DO NOT TOUCH !!!!")]

    [Header("GENERAL BLOCK INFO"),Space(10)]
    [HideInInspector] public Directions directToGo; // direction for tile to move to
    [HideInInspector] public BlockColor blockColor; // type of block

    [Tooltip("The shorter distance is more performance will take per block")]
    [Range(0.25f,1)]public float distanceToCheck; // distance to check when the block move
    public SpriteRenderer arrowImage; // image of arrow to display
    public float blockSpeed;//movement speed of block
    public LayerMask checkLayer; // layer to check

    [HideInInspector]public SpriteRenderer _blockRend;//sprite render of the block itself
    [HideInInspector]public BoxCollider2D _box2D; // block box collision
    [HideInInspector]public Sprite blockCupSpriteWCap; // cup sprite for block to switch after condition met
    [HideInInspector]public Sprite blockCupSpriteNoCap; // cup sprite wiht no cap on it
    protected TrailRenderer _trail;

    public virtual void Awake()
    {
        //----------------------------- SET --------------------------------------
        //add food to level manager
        GameManager.Instance.level.avalibleBlock.Add(this);
        //----------------------------- SET --------------------------------------
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        //----------------------------- GET --------------------------------------
        //get block sprite renderer
        SpriteRenderer _blockSprite = this.gameObject.GetComponent<SpriteRenderer>();
        //get trail render
        _trail = this.gameObject.GetComponent<TrailRenderer>();
        //get block trail color template
        BlockTrailColorPalate _blockTrailTemplate = Resources.Load<BlockTrailColorPalate>("BlockExtension/BlockColorTemplate");
        //store block sprite
        _blockRend = this.gameObject.GetComponent<SpriteRenderer>();
        //storing box collision into this class
        _box2D = this.gameObject.GetComponent<BoxCollider2D>();
        //----------------------------- GET --------------------------------------

        //----------------------------- SET --------------------------------------

        //get color name
        string colorName = Enum.GetName(typeof(BlockColor), blockColor);
        //get block sprite
        _blockSprite.sprite = Resources.Load<Sprite>($"Blocks_Sprite/{colorName}");
        blockCupSpriteWCap = Resources.Load<Sprite>($"Cups_WCaps/{colorName}");
        blockCupSpriteNoCap = Resources.Load<Sprite>($"Cups/{colorName}");
        
        //loop all blocktrail template
        for(int i =0; i<_blockTrailTemplate._block.Length; i++)
        {
            //if block color is 
            if(_blockTrailTemplate._block[i].blockcolor == blockColor)
            {
                Debug.Log($" COLOR: {_blockTrailTemplate._block[i].blockcolor}");
                //set trail color
                _trail.material = _blockTrailTemplate._block[i].colorM;
                break;
            }
            
        }

        //check arrow direction to make sure it set correctly
        OnCheckArrowDirection();
        //----------------------------- SET --------------------------------------
    }

    public virtual void OnTouch(){}

    /// <summary>
    /// function to check arrow direction to rotate
    /// </summary>
    public void OnCheckArrowDirection() 
    {
        switch (directToGo) 
        {
            case Directions.up:
                //rotate up
                arrowImage.transform.eulerAngles = new Vector3(0, 0, -90);
                break;

            case Directions.down:
                //rotate down
                arrowImage.transform.eulerAngles = new Vector3(0, 0, 90);
                break;

            case Directions.left:
                //rotate left
                arrowImage.transform.eulerAngles = new Vector3(0, 0, 0);
                break;

            case Directions.right:
                //rotate right
                arrowImage.transform.eulerAngles = new Vector3(0, 0, 180);
                break;
        
        }
    
    }

    #region HELPER FUNCTIONS

    /// <summary>
    /// function to get direction to move to
    /// </summary>
    /// <param name="_direct"> direction that been given </param>
    /// <returns></returns>
    public Vector2 GetDirection(Directions _direct)
    {
        switch (_direct)
        {
            case Directions.up:
                return new Vector2(0, distanceToCheck);
            case Directions.down:
                return new Vector2(0, -(distanceToCheck));
            case Directions.left:
                return new Vector2(-(distanceToCheck), 0);
            case Directions.right:
                return new Vector2(distanceToCheck, 0);
        }

        return new Vector2(0, 0);
    }

    /// <summary>
    /// function to change to desiered sprite
    /// </summary>
    /// <param name="_targetSprite"> sprite to change </param>
    public void OnChangeSprite(Sprite _targetSprite) 
    {
        //changing block sprite
        _blockRend.sprite = _targetSprite;
    }

    ///// <summary>
    ///// function to set offset for food
    ///// </summary>
    ///// <param name="_direct"> direction </param>
    //private void FoodOffsetter(Directions _direct) 
    //{
    //   switch(_direct) 
    //   {
    //        case Directions.up:
    //            arrowImage.transform.localPosition = new Vector2(0, -0.1f);
    //            break;
    //        case Directions.down:
    //            arrowImage.transform.localPosition = new Vector2(0, 0.1f);
    //            break;
    //        case Directions.left:
    //            arrowImage.transform.localPosition = new Vector2(0.1f, 0.05f);
    //            break;
    //        case Directions.right:
    //            arrowImage.transform.localPosition = new Vector2(-0.1f, 0.05f);
    //            break;
        
    //   }
    //}
    #endregion


}


public enum Directions
{
    up,
    down,
    left,
    right,
}

public enum BlockColor 
{ 
   D_Blue,
   Green,
   Grey,
   L_Blue,
   Lime,
   Orange,
   Pink,
   Purple,
   Red,
   Yellow,

}