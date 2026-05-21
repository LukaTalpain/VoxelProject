using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private GameObject _Chunk;
    public int chunkSize;
    public int mapSize;
    public int seed;
    public float scale;
    [SerializeField] private RSO_VoxelData voxelData;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            voxelData.mapSize = mapSize;
            voxelData.chunkGeneratorList.Clear();
            SelectCoord();
        }
    }
    private void Start()
    {
        if (seed == 0)
        {
            seed = Random.Range(0, 1000);
        }
        voxelData.mapSize = mapSize;
        voxelData.chunkGeneratorList.Clear();
        SelectCoord();
    }

    private void SelectCoord ()
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                GenerateChunk(new Vector2Int(x,y));
            }
        }
    }


    private void GenerateChunk(Vector2Int pos)
    {
        Vector3 chunkPos = new Vector3(pos.x* chunkSize, 0, pos.y* chunkSize);
        GameObject _chunkGenerator = Instantiate(_Chunk, chunkPos, Quaternion.Euler(new Vector3(0,0,0)),this.transform);
        _chunkGenerator.GetComponent<ChunkComponent>().GenerateItsChunk(pos,seed,scale);
        voxelData.chunkGeneratorList.Add(_chunkGenerator);

    }
}
