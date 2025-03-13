using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**********************************************
 * CLASS FOR INFORMATION ABOUT BLOCK ON TRAY
 **********************************************/
public class BlockTray : MonoBehaviour
{
    //public Transform arrowTransform; // transform of arrow since we going to rotate it
   // public SpriteRenderer arrowSprite; // image of arrow
    public SpriteRenderer blockSprite; // image of block
    public bool taken; // bool to check whether the block been taken
    [HideInInspector]public Directions direct; // direcction information storage
    [HideInInspector] public BlockColor color; // color information storage
    [HideInInspector] public Sprite blockCupSprite;
    /// <summary>
    /// function to set information and setup for the block on tray
    /// </summary>
    /// <param name="_direct"> direction information </param>
    /// <param name="_blockColor"> color information </param>
    /// <param name="_arrowTarget"> arrown transform so we can copy rotation of it </param>
    /// <param name="_blockSprite"> image of the arrow so we can copy the image of it </param>
    /// <param name="_arrowSprite"> image of the block so we can copy the image of it </param>
    /// <param name="_default"> bool to identify whether player want to set block back to default | false mean yes / true mean no | </param>
    public void SetInfo( BlockColor _blockColor, Sprite _blockSprite, Sprite _cupSprite = null ,bool _default = false) 
    { 
        //setting rotation of the arrow
        //arrowTransform.localEulerAngles = _arrowTarget.transform.localEulerAngles;
        //storing direction information
        //direct = _direct;
        //storing color information
        color = _blockColor;
        //copying arrow image
        //arrowSprite.sprite = _arrowSprite;
        //coppying block image
        blockSprite.sprite = _blockSprite;
        //get cup sprite from target
        blockCupSprite = _cupSprite;
        //set taken back to default so it can be detect
        taken = _default;
    }
}
