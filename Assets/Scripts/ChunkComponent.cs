using UnityEngine;
using Voxels;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class ChunkComponent : MonoBehaviour
{
    private Chunk m_Chunk;

    private void Awake()
    {
        m_Chunk = new Chunk();

        for (uint z = 0; z < Chunk.ChunkSize; z++)
        {
            for (uint y = 0; y < Chunk.ChunkSize; y++)
            {
                for (uint x = 0; x < Chunk.ChunkSize; x++)
                {
                    ref Voxel voxel = ref m_Chunk.GetVoxelAt(x, y, z);
                    if (y < 6)
                    {
                        if (y == 5 && Random.value < 0.1f)
                        {
                            voxel.block = Blocks.Grass;
                        }
                        else if (y < 5)
                        {
                            if (y < 3)
                                voxel.block = Blocks.Stone;
                            else if (y < 4)
                                voxel.block = Random.value < 0.5f ? Blocks.Stone : Blocks.Dirt;
                            else
                                voxel.block = Blocks.Grass;
                        }
                    }
                }
            }
        }

        var mesh = ChunkMeshGenerator.GenerateMesh(m_Chunk);
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(8, 8, 8), new Vector3(16, 16, 16));
    }
}
