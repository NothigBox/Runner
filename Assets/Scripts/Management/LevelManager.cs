using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TouchManager touch;
    [SerializeField] private CharacterManager character;
    [SerializeField] private FactoryController factory;
    [SerializeField] private MapController map;

    [SerializeField] private List<MapChunk> chunks;


    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        for (int i = 0; i < chunks.Count; i++) 
        {
            map.AddMapChunk(chunks[i] as ISpeedObserver);
        }
    }

    private void OnEnable()
    {
        touch.OnMove += character.Move;
        touch.OnJump += character.Jump;

        map.OnMapChunkDeleted += SpawnMap;
    }

    private void OnDisable()
    {
        touch.OnMove -= character.Move;
        touch.OnJump -= character.Jump;

        map.OnMapChunkDeleted -= SpawnMap;
    }

    private void SpawnMap()
    {
        map.AddMapChunk(factory.GetMapChunk());
    }
}
