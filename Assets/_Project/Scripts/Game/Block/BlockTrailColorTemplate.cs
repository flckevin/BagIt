using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockColorTemplate", menuName = "ScriptableObjects/BlockColorTemplate")]
public class BlockTrailColorPalate : ScriptableObject
{
    public BlockTrailColorPalateTemplate[] _block = new BlockTrailColorPalateTemplate[System.Enum.GetValues(typeof(BlockColor)).Length];
}

[Serializable]
public class BlockTrailColorPalateTemplate
{
   public BlockColor blockcolor;
   public Material colorM;
}
