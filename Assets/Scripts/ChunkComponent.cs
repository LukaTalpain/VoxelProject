using System;
using UnityEngine;
using Voxels;
using static UnityEditor.PlayerSettings;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class ChunkComponent : MonoBehaviour
{
    private Chunk m_Chunk;
    public RSO_VoxelData voxelData;

    public void GenerateItsChunk(Vector2Int chunkPos , int seed, float scale)
    {
        m_Chunk = new Chunk();

        for (uint z = 0; z < Chunk.ChunkSize; z++)
        {
            for (uint y = 0; y < Chunk.ChunkSize; y++)
            {
                for (uint x = 0; x < Chunk.ChunkSize; x++)
                {
                    ref Voxel voxel = ref m_Chunk.GetVoxelAt(x, y, z);
                    voxel.block = ChunkGenerator.GetVoxelBlockType(seed, chunkPos, new Vector3Int((int)x, (int)y, (int)z), scale, voxelData);
                }
            }
        }

        var mesh = ChunkMeshGenerator.GenerateMesh(m_Chunk);
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
    public void ReGenerateItsChunk()
    {
        for (uint z = 0; z < Chunk.ChunkSize; z++)
        {
            for (uint y = 0; y < Chunk.ChunkSize; y++)
            {
                for (uint x = 0; x < Chunk.ChunkSize; x++)
                {
                    ref Voxel voxel = ref m_Chunk.GetVoxelAt(x, y, z);
                }
            }
        }

        var mesh = ChunkMeshGenerator.GenerateMesh(m_Chunk);
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    public void DestroyBlock (Vector3 pos)
    {
        pos = new Vector3Int((int)pos.x - (int)transform.position.x, (int)pos.y - (int)transform.position.y, (int)pos.z - (int)transform.position.z);
        ref Voxel voxel = ref m_Chunk.GetVoxelAt((uint)pos.x, (uint)pos.y, (uint)pos.z);
        print ("position block to destroy : x : " + pos.x + " y : " + pos.y + " z : " + pos.z);
        print ("block to destroy : " + voxel.block.ToString());
        voxel.block = Blocks.Air;
        ReGenerateItsChunk();
    }
    
    public void PlaceBlock (Vector3 _pos, Blocks block)
    {
        Vector3 pos = new Vector3Int((int)_pos.x - (int)transform.position.x, (int)_pos.y - (int)transform.position.y, (int)_pos.z - (int)transform.position.z);
        if (pos.x < 0 || pos.x >= Chunk.ChunkSize)
        {
            int index;
            if (pos.x < 0)
            {
                for ( int i = 0; i < voxelData.chunkGeneratorList.Count; i++)
                {
                    if (voxelData.chunkGeneratorList[i] == this.gameObject)
                    {
                        index = i;
                        voxelData.chunkGeneratorList[index-4].GetComponent<ChunkComponent>().PlaceBlock(new Vector3(Chunk.ChunkSize -1, _pos.y, _pos.z), block);
                    }
                }
                return;
            }
            else
            {
                for (int i = 0; i < voxelData.chunkGeneratorList.Count; i++)
                {
                    if (voxelData.chunkGeneratorList[i] == this.gameObject)
                    {
                        index = i;
                        voxelData.chunkGeneratorList[index + 4].GetComponent<ChunkComponent>().PlaceBlock(new Vector3(_pos.x, _pos.y, _pos.z), block);
                    }
                }
                return;
            }   
        }
        else if (pos.z < 0 || pos.z >= Chunk.ChunkSize)
        {
            int index;
            if (pos.z < 0)
            {
                for (int i = 0; i < voxelData.chunkGeneratorList.Count; i++)
                {
                    if (voxelData.chunkGeneratorList[i] == this.gameObject)
                    {
                        index = i;
                        voxelData.chunkGeneratorList[index - 1].GetComponent<ChunkComponent>().PlaceBlock(new Vector3(_pos.x, _pos.y, _pos.z), block);
                    }
                }
                return;
            }
            else
            {
                for (int i = 0; i < voxelData.chunkGeneratorList.Count; i++)
                {
                    if (voxelData.chunkGeneratorList[i] == this.gameObject)
                    {
                        index = i;
                        voxelData.chunkGeneratorList[index + 1].GetComponent<ChunkComponent>().PlaceBlock(new Vector3(_pos.x, _pos.y, _pos.z), block);
                    }
                }
                return;
            }
        }
        ref Voxel voxel = ref m_Chunk.GetVoxelAt((uint)pos.x, (uint)pos.y, (uint)pos.z);
        print ("position block to place : x : " + pos.x + " y : " + pos.y + " z : " + pos.z);
        print ("block to place : " + block.ToString());
        voxel.block = block;
        ReGenerateItsChunk();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(transform.position.x + 8, transform.position.y + 8, transform.position.z + 8), new Vector3(16, 16, 16));
    }


}
