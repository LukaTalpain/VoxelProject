using System;
using UnityEngine;
using Voxels;

[CreateAssetMenu]
public class RSO_VoxelData : ScriptableObject
{
    public BlockData[] allBlocks;

    public BlockData[] groundBlocks;
}

[Serializable]
public class BlockData
{
    public Blocks blockType;

}
