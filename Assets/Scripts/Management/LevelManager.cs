using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TouchManager touch;
    [SerializeField] private CharacterManager character;
    [SerializeField] private FactoryController factory;
    [SerializeField] private MapController map;

    [SerializeField] private List<MapChunk> chunks;

    private void Start()
    {
        for (int i = 0; i < chunks.Count; i++) 
        {
            map.AddObserver(chunks[i]);
        }
    }

    private void OnEnable()
    {
        touch.OnMove += character.Move;
        touch.OnJump += character.Jump;
    }

    private void OnDisable()
    {
        touch.OnMove -= character.Move;
        touch.OnJump -= character.Jump;
    }
}
