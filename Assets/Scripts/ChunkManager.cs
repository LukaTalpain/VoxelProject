using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private GameObject _Chunk;
    public int chunkSize;
    public int mapSize;
    public int seed;
    public float scale;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SelectCoord();
        }
    }
    private void Start()
    {
        if (seed == 0)
        {
            seed = Random.Range(0, 1000);
        }

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

    //un chunk par un chunk pour le moment 

    private void GenerateChunk(Vector2Int pos)
    {
        Vector3 chunkPos = new Vector3(pos.x* chunkSize, 0, pos.y* chunkSize);
        GameObject _chunkGenerator = Instantiate(_Chunk, chunkPos, Quaternion.Euler(new Vector3(0,0,0)),this.transform);
        _chunkGenerator.GetComponent<ChunkComponent>().GenerateItsChunk(pos,seed,scale);

    }
}
