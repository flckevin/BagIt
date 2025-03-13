using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //public Transform[] beltPath; // path of belt
    [Header("LEVEL INFO"), Space(10)]
    public List<Slots> slots = new List<Slots>(); // all slot
    public List<Slots> lockedSlot = new List<Slots>();  // all locked slots
    public List<BlockCore> avalibleBlock = new List<BlockCore>();//list of all avalible food in level
    public Transform background;
    [Header("NPC"), Space(10)]
    public List<NPCBehaviour> avalibleNpc = new List<NPCBehaviour>(); // list of all npc in line
    public Transform npcStartPosition; // npc start position
    public Transform npcPosition; // npc order position
    public Transform npcEndPosition; // npc end position

    private void Awake()
    {
        //get all correct amount of items
        ItemData.SwapAmount = PlayerPrefs.GetInt("SwapAmount",5);
        ItemData.SlotAmount = PlayerPrefs.GetInt("SlotAmount",5);
        
        //assign level manager to game mamanger
        GameManager.Instance.level = this;
        CamResize();
    }

    private void Start()
    {
        SpawnLevel();
        GameManager.Instance.levelNoDisplay.text = $"Level {GameData.Level}";
        //setting amount of slot and swap to display on text
        GameManager.Instance.gameMenu.swapAmountText.text = ItemData.SwapAmount.ToString();
        GameManager.Instance.gameMenu.slotAmountText.text = ItemData.SlotAmount.ToString();
    }

    /// <summary>
    /// function to add food into slot
    /// </summary>
    /// <param name="_block"> food going to be add </param>
    public void SlotAddBlock(BlockCore _block) 
    {
        //disable box 2D so other block raycast wont hit it
        _block._box2D.enabled = false;
        //loop all slot
        for (int i = 0; i < slots.Count; i++)
        {
            //check if that slot empty
            if (slots[i].blockHolding == null)
            {
                //set slot food to be the same as block
                slots[i].blockHolding = _block;
                
                // move the whole block to the plate position with higher position
                _block.gameObject.transform.position = new Vector2(slots[i].transform.position.x, 
                                                                    slots[i].transform.position.y);
                
                //loop for allavalible npc
                for (int n = 0; n < avalibleNpc.Count; n++)
                {
                    //tell that npc to get his order
                    avalibleNpc[n].DoState(avalibleNpc[n]._onFindState);
                    Debug.Log("ADD");
                }

                //disable block sprite
                //_block._blockRend.enabled = false;

                //position food back to default position
                //_block.arrowImage.transform.localPosition = Vector2.zero;

                //scale food back to normal
                //_block.arrowImage.transform.DOScale(new Vector3(1, 1, 1), 0.1f);

                //move the block aka food to food slot position
                //_block.gameObject.transform.DOMove(slots[i].transform.position, 0.5f).SetEase(Ease.Linear);
                _block.arrowImage.enabled = false;
                //scale block to 0
                _block.gameObject.transform.localScale = Vector3.zero;
                //apear it again to create effect
                _block.gameObject.transform.DOScale(Vector3.one, 0.3f);
                //change block sprite
                _block.OnChangeSprite(_block.blockCupSpriteWCap);
                return;
            }
        }

        //cant find any empty slot
        EventManger.Instance.OnLoose();
    }

    /// <summary>
    /// function to spawn a level
    /// </summary>
    private void SpawnLevel()
    {
        Debug.Log($"LOADING {GameData.Level} ");
        //this.transform.position = Vector3.zero; 
        GameObject _level = Resources.Load<GameObject>($"Levels/Level {GameData.Level}");
  
        //if level does exist
        if (_level != null)
        {
            //spawn level
            GameObject _spawnedLevel = Instantiate(_level);


           // _spawnedLevel.transform.position = new Vector2(_level.transform.localPosition.x, background.transform.localPosition.y * -1);
        }
       

    }

    /// <summary>
    /// function to resize cam
    /// </summary>
    public void CamResize() 
    { 
        Camera _cam = Camera.main;
        float _width = Screen.width;
        float _height = Screen.height;
        Debug.Log($"SCREEN {_width} x {_height}");
        int _finalSize = (int)(_width * _height / 4 / 100000);
        _cam.orthographicSize = _finalSize;
        _cam.orthographicSize = Mathf.Clamp(_cam.orthographicSize, 5, 6);
    }
}
