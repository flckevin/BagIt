#if UNITY_EDITOR

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using QuocAnh.Editor;

/*******************************
 * AUTHOR : QUOC ANH - 03/06/2024 
 *******************************/

public class LevelEditor : OdinEditorWindow
{
    #region WINDOW
    [MenuItem("TB Tool/TB Level Editor")]
    private static void OpenWindow()
    {
        GetWindow<LevelEditor>().Show();
    }
    #endregion

    [Title("TB LEVEL EDITOR", null, TitleAlignments.Centered, true, true)]

    [Space(10)]
    [Title("OPTIONS", null, TitleAlignments.Centered, true, true)]
    public MapTool mapTool;
    private bool newMap; // bool of decesion spawning new map
    private bool editMap; // bool of decision to edit map

    #region NEW MAP VAR

    [Title("TAO MAP MOI", null, TitleAlignments.Centered, true, true)]
    
    [ShowIf("newMap")]
    public Vector2 dimension; // dimension to spawn board
    
    [ShowIf("newMap")]
    public Vector2 startOffset; // offset of block start position
    
    //[ShowIf("newMap")]
    //public Transform mapParent;//parent to hold all of the block 
    
    [ShowIf("newMap")]
    public int levelNo;//level number

    [ShowIf("newMap")]
    public float blockSpace;
    private BlockCore rootTile; // tile that going to be apprea in game

    private float startPosX;
    private float startPosY;
    #endregion


    #region EDIT OLD MAP VAR

    [Title("CHINH MAP CU", null, TitleAlignments.Centered, true, true)]
    
    [ShowIf("editMap")]
    [ValueDropdown("_allMap")]
    public GameObject mapToEdit;

    private GameObject[] _allMap;

    private GameObject _rootLevel;
    #endregion

    #region VAR FOR SPAWN BLOCK
    [ShowIf(nameof(bothOption))]
    [ValueDropdown("emptyPos")]
    [PropertyOrder(1)]
    public Vector2 posToSpawn; // position to spawn
    [ShowIf(nameof(bothOption))]
    private List<Vector2> emptyPos = new List<Vector2>(); // store all transform
    
    [ShowIf(nameof(bothOption))]
    public float backGroundY = -0.7f;
    #endregion

    #region VAR FOR BOTH
    private bool bothOption => newMap == true || editMap == true;
    private GameObject _mainParent;
    #endregion


    private void OnEnable()
    {
        newMap = false;
        editMap = false;
        //get root block
        rootTile = Resources.Load<BlockCore>("BlockCore/Block");
        //get all map
        _allMap = Resources.LoadAll<GameObject>("Levels");
    }

    //===================================== CHINH LEVEL ====================================================

    /// <summary>
    /// function to build map
    /// </summary>
    [ShowIf("newMap")]
    [Button(ButtonSizes.Large)]
    [GUIColor(0.53f, 0.83f, 0.9f, 1f)]
    public void Build() 
    {
        // if dimension not been filled warning then stop execution
        if (dimension == Vector2.zero) { Debug.LogWarning("PLEASE ENTER DIMENSTION YOU DESIRED"); return; }
        //if block parent not been filled warning then stop execution
        //if (mapParent == null) { Debug.LogWarning("PLEASE FILL IN MAP PARENT"); return; }
        //if there is a map already existing then delete it
        if(_mainParent != null) { DestroyImmediate(_mainParent); }
        //create new block parent
        _mainParent = new GameObject($"Level {levelNo}");
        //set main parent to be at center of view
        _mainParent.transform.position = Vector2.zero;
        //make map object as parent
        //_mainParent.transform.parent = mapParent;

        //loop on x axis
        for (int row = 0; row < dimension.y; row++) 
        { 
            //loop on y axis
            for(int col = 0; col< dimension.x; col++) 
            {
                //calculate starting point of x and y
                startPosX = (dimension.x / -2f);
                startPosY = -(dimension.y - 1) / 2f;
                //spawn block at given position
                BlockCore _block = (BlockCore)PrefabUtility.InstantiatePrefab(rootTile, _mainParent.transform);
                //relocate block position
                _block.transform.localPosition = new Vector2(startPosX, startPosY);
                //calculate center position
                Vector3 centeredPos = new Vector3((startPosX + col)*blockSpace, startPosY + row, 0);
                //set block position to be at looped x and y
                _block.transform.localPosition = centeredPos;
                //add script to edit block
                _block.gameObject.AddComponent<BlockEditor>();
            }
        }
        //centering the map parent
        _mainParent.transform.position = new Vector3(_mainParent.transform.position.x + 0.5f, backGroundY, 0);

    }

    /// <summary>
    /// function to build map
    /// </summary>
    [ShowIf("editMap")]
    [Button(ButtonSizes.Large)]
    [GUIColor(0.53f, 0.83f, 0.9f, 1f)]
    public void SpawnExistedLevel()
    {
        List<BlockCore> avalibleBlocks = new List<BlockCore>();
        //if map to edit does not exist then stop execute
        if (mapToEdit == null) return;
        //if main parent does exis then destroy it
        if (_mainParent != null) { Destroy(_mainParent); }
        //spawn edit map
        GameObject _spawnedMap = Instantiate(mapToEdit);
        //set main parent to be spawned map
        _mainParent = _spawnedMap;
        _mainParent.transform.position = new Vector3(_mainParent.transform.position.x, backGroundY,_mainParent.transform.position.z);
        _rootLevel = mapToEdit;
        //  !!!!!!!!!!!!!!! NOTE READ THIS PLEASE !!!!!!!!!!!!!!!
        //THE REASON WE HAVE TO SPLIT THE BLOCK AND STORE TO A LIST
        //INSTEAD OF USING IT DIRECTLY IS BECAUSE OF PERFORMANCCE REASON
        //DO NOT CHANGE THE CONCEPT OF THE WAY THESE WORKS OR ELSE IT GOING
        //TO BE A MESS AND IT WON'T RUN AT ALLLLLLLL !!!!!!
        //loop all main parent block
        for (int i = 0; i < _mainParent.transform.childCount; i++) 
        {
            //storage all of those
            avalibleBlocks.Add(_mainParent.transform.GetChild(i).GetComponent<BlockCore>());
        }


        //loop all main parent
        for (int i = 0; i < avalibleBlocks.Count; i++)
        {
            //spawn block
            BlockCore _blockPrefab = (BlockCore)PrefabUtility.InstantiatePrefab(rootTile, _mainParent.transform);
           //store block root so we can transfer information
            BlockCore _RootblockCore = avalibleBlocks[i];
            //add block editor into that block so we can edit in editor
            BlockEditor _blockEdit = _blockPrefab.AddComponent<BlockEditor>();

            //transfer direction
            _blockPrefab.directToGo = _RootblockCore.directToGo;
            //transfer blocktype
            _blockPrefab.blockColor = _RootblockCore.blockColor;
            //transfer food image
            _blockPrefab.arrowImage.sprite = _RootblockCore.arrowImage.sprite;
            //change direction on arrow image
            OnCheckArrowDirection(_blockPrefab.directToGo, _blockPrefab.arrowImage.transform);
            //transfer sprite image of block itself
            _blockPrefab.GetComponent<SpriteRenderer>().sprite = _RootblockCore.GetComponent<SpriteRenderer>().sprite;
            //transfer the informaion of location of the block
            _blockPrefab.transform.localPosition = _RootblockCore.transform.localPosition;

            //set block edit to have same block type
            _blockEdit.blockColor = _blockPrefab.blockColor;
            //set block edit direction to have same direction
            _blockEdit.direction = _blockPrefab.directToGo;

            Debug.Log(_blockPrefab.name);
            //destroy root block to replace with prefab block
            DestroyImmediate(_RootblockCore.gameObject);
        }
    }

    /// <summary>
    /// fuction to save map as prefab
    /// </summary>
    [ShowIf(nameof(bothOption))]
    [GUIColor(0.61f, 0.91f, 0.73f)]
    [Button(ButtonSizes.Large)]
    public void Save() 
    {
        //loop all block
        for(int i =0;i<_mainParent.transform.childCount;i++) 
        {
            //get editor block
            BlockEditor _editorBlock = _mainParent.transform.GetChild(i).GetComponent<BlockEditor>();
            
            //check if editor block does exist
            if (_editorBlock != null) 
            {
                //destroy block editor class
                DestroyImmediate(_editorBlock);
            }
            
        }

        string _savePath;

        if (editMap == true)
        {
            AssetDatabase.DeleteAsset($"Assets/_Project/Resources/Levels/{_rootLevel.gameObject.name}.prefab");
            _mainParent.name = _mainParent.name.Replace("(Clone)", string.Empty);
        }
      
        _savePath = $"Assets/_Project/Resources/Levels/{_mainParent.gameObject.name}.prefab";
        _savePath = AssetDatabase.GenerateUniqueAssetPath(_savePath);
        PrefabUtility.SaveAsPrefabAsset(_mainParent.gameObject, _savePath);
        Debug.Log($"SAVE AT {_savePath}");
        //destroy final version on screen
        DestroyImmediate(_mainParent);
        //update all map
        _allMap = Resources.LoadAll<GameObject>("Levels");

    }
    //===================================== CHINH LEVEL ====================================================


    //===================================== TAO BLOCK MOI ====================================================

    [Title("", null, TitleAlignments.Centered, true, true)]

    [Title("TAO BLOCK NOI", null, TitleAlignments.Centered, true, true)]

    [ShowIf(nameof(bothOption))]
    [Button(ButtonSizes.Large)]
    [GUIColor(0.91f, 0.91f, 0.61f)]
    public void GetEmptyBlockPos() 
    {
        //clear whole empty position list
        emptyPos.Clear();

        if (_mainParent == null) return;

        //loop on x axis
        for (int x = 0; x < dimension.x; x++)
        {
            //loop on y axis
            for (int y = 0; y < dimension.y; y++)
            {
                //create new axis
                Vector2 pos = new Vector2(x + startOffset.x, y + startOffset.y);
                //store that position into list
                emptyPos.Add(pos);
            }
        }

        //loop all block parent position
        for (int i = 0; i < emptyPos.Count; i++) 
        {
            Debug.Log($"{_mainParent.transform.GetChild(i).name} + {_mainParent.transform.GetChild(i).position} + {emptyPos[i]}");
            
            //loop all child in main parent
            for (int y = 0; y < _mainParent.transform.childCount; y++) 
            {
                //if block parent position is equal to position that in storage
                if ((Vector2)_mainParent.transform.GetChild(y).position == emptyPos[i])
                {
                    //remove that
                    emptyPos.Remove(emptyPos[i]);
                }

            }
        }    
    }

    [ShowIf(nameof(bothOption))]
    [Button(ButtonSizes.Large)]
    [GUIColor(0.91f, 0.91f, 0.61f)]
    public void SpawnBlock() 
    {
        if (_mainParent == null) return;
        //spawn block and attack it to prefab
        BlockCore _block = (BlockCore)PrefabUtility.InstantiatePrefab(rootTile, _mainParent.transform);
        //set block position to be at looped x and y
        _block.transform.position = posToSpawn;
        //add script to edit block
        _block.AddComponent<BlockEditor>();
        //check empty position again
        GetEmptyBlockPos();
    }

    //===================================== TAO BLOCK MOI ====================================================

    //===================================== HELPER FUNCTIONS ====================================================

    public void OnCheckArrowDirection(Directions _direct , Transform _target)
    {
        switch (_direct)
        {
            case Directions.up:
                //rotate up
                _target.transform.eulerAngles = new Vector3(0, 0, -90);
                break;

            case Directions.down:
                //rotate down
                _target.transform.eulerAngles = new Vector3(0, 0, 90);
                break;

            case Directions.left:
                //rotate left
                _target.transform.eulerAngles = new Vector3(0, 0, 0);
                break;

            case Directions.right:
                //rotate right
                _target.transform.eulerAngles = new Vector3(0, 0, 180);
                break;

        }

    }

    //===================================== OTHERS ====================================================

    //===================================== OTHERS ====================================================

    private void OnValidate()
    {
        if (mapTool == MapTool.CreateNewMap)
        {
            newMap = true;
            editMap = false;
        }
        else if (mapTool == MapTool.EditOldMap)
        {
            newMap = false;
            editMap = true;
        }
        else 
        {
            newMap = false;
            editMap = false;
        }

    }

    //===================================== OTHERS ====================================================
}

namespace QuocAnh.Editor 
{
    public enum MapTool 
    { 
        SelectATool,
        CreateNewMap,
        EditOldMap
    
    }
}

#endif