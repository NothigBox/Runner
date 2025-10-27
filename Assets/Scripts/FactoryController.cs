using UnityEngine;

public class FactoryController : MonoBehaviour
{
    [SerializeField] private MapChunk chunk;

    private ObjectsPool<MapChunk> chunksPool;

    private void Awake()
    {
        chunksPool = new ObjectsPool<MapChunk>(chunk);
    }

    public MapChunk GetMapChunk(Vector3 position)
    {
        MapChunk result = chunksPool.GetObject(position);
        return result;
    }

    public MapChunk GetMapChunk()
    {
        return GetMapChunk(new Vector3(0f, 1000f, 0f));
    }

    public void GetRandomMapChunk()
    {

    }
}
