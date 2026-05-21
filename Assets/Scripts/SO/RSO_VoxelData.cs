using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels;

[CreateAssetMenu]
public class RSO_VoxelData : ScriptableObject
{
    public BlockData[] allBlocks;

    public BlockData[] groundBlocks;

    public List<GameObject> chunkGeneratorList;
    public int mapSize;
}

[Serializable]
public class BlockData
{
    public Blocks blockType;

}
