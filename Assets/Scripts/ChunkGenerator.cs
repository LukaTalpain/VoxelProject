using UnityEngine;
using Voxels;

public static class ChunkGenerator
{
    private static int chunkSize = 16;
    public static Blocks GetVoxelBlockType(int seed, Vector2Int chunkPos, Vector3Int voxelPos, float scale,RSO_VoxelData voxelData)
    {
        float height = GetPerlinHeight(voxelPos.x +(chunkPos.x * chunkSize), voxelPos.z + (chunkPos.y*chunkSize),scale,seed);


        return GetBlockFromHeight(height, voxelPos.y,voxelData);

    }

    public static float GetPerlinHeight(float sampleX, float sampleY, float scale, int seed) 
    {

        return Mathf.PerlinNoise(sampleX * scale + seed, sampleY * scale + seed);


    }

    private static Blocks GetBlockFromHeight (float noiseHeight,int voxelHeight , RSO_VoxelData voxelData)
    {
        if (noiseHeight* chunkSize < voxelHeight)
        {
            return Blocks.Air;
        }
        else if (noiseHeight*chunkSize >= voxelHeight && (noiseHeight * chunkSize)-1 < voxelHeight)
        {
            return Blocks.Grass;
        }
        else
        {
            float noise = noiseHeight * 100;
            float threshold = noise / voxelData.groundBlocks.Length;
            float v_height = ((float)voxelHeight / (float)chunkSize)*100;
            for (int i = 0; i < voxelData.groundBlocks.Length; i++)
            {
                if (v_height <= noise - (threshold * i) && v_height > noise - (threshold * (i + 1)))
                {
                    return voxelData.groundBlocks[i].blockType;
                }

            }

            return voxelData.groundBlocks[voxelData.groundBlocks.Length-1].blockType;
        }


    }

}
