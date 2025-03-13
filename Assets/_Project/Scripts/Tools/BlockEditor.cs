using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEditor : MonoBehaviour
{
    public Directions direction;
    public BlockColor blockColor;
    private BlockCore blockCore;
    //========================== NOTE -> MOVE THIS FUNCTION TO TOOL LATER ON =================================

    private void OnEnable()
    {
        blockCore = this.gameObject.GetComponent<BlockCore>();
    }

    private void CheckColor()
    {
        string colorName = Enum.GetName(typeof(BlockColor),blockColor);

        Sprite _arrowSprite = Resources.Load<Sprite>($"ArrowColor/{colorName}");
        blockCore.arrowImage.sprite = _arrowSprite;

        Sprite _blockSprite = Resources.Load<Sprite>($"Blocks_Sprite/{colorName}");
        blockCore.gameObject.GetComponent<SpriteRenderer>().sprite = _blockSprite;

        Debug.Log($"COLOR {colorName}");

        blockCore.blockColor = blockColor;

    }

    private void OnCheckArrowDirection()
    {
        switch (direction)
        {
            case Directions.up:
                blockCore.arrowImage.transform.eulerAngles = new Vector3(0, 0, -90);
                break;

            case Directions.down:
                blockCore.arrowImage.transform.eulerAngles = new Vector3(0, 0, 90);
                break;

            case Directions.left:
                blockCore.arrowImage.transform.eulerAngles = new Vector3(0, 0, 0);
                break;

            case Directions.right:
                blockCore.arrowImage.transform.eulerAngles = new Vector3(0, 0, 180);
                break;

        }

        blockCore.directToGo = direction;
    }
    //========================== NOTE -> MOVE THIS FUNCTION TO TOOL LATER ON =================================

    private void OnValidate()
    {
        if(blockCore == null ) 
        {
            blockCore = this.gameObject.GetComponent<BlockCore>();
        }

        CheckColor();
        OnCheckArrowDirection();
        
    }
}
