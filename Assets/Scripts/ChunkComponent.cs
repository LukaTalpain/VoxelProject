using UnityEngine;
using UnityEngine.UIElements;
using Voxels;

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
        ref Voxel voxel = ref m_Chunk.GetVoxelAt((uint)pos.x, (uint)pos.y, (uint)pos.z);
        voxel.block = Blocks.Air;
        ReGenerateItsChunk();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(transform.position.x + 8, transform.position.y + 8, transform.position.z + 8), new Vector3(16, 16, 16));
    }


}
