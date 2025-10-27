using System;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private float ticksPerSecond;
    [SerializeField] private float speed;
    [SerializeField] private Transform spawnPoint;

    private List<ISpeedObserver> speedObservers;

    public Action OnMapChunkDeleted;

    private void Awake()
    {
        speedObservers = new List<ISpeedObserver>();
    }

    private void FixedUpdate()
    {
        float speed = this.speed * Time.deltaTime;
        NotifyObservers(speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Floor") == true)
        {
            MapChunk chunk = other.GetComponent<MapChunk>();
            speedObservers.Remove(chunk);
            chunk.gameObject.SetActive(false);
            OnMapChunkDeleted?.Invoke();
        }
    }

    public void NotifyObservers(float speed)
    {
        for (int i = 0; i < speedObservers.Count; i++) 
        {
            speedObservers[i].OnSpeedUpdate(speed);
        }
    }

    public void AddMapChunk(MapChunk chunk)
    {
        MapChunk lastChunk = (speedObservers[speedObservers.Count - 1] as MapChunk);
        Vector3 colliderSize = lastChunk.GetComponent<BoxCollider>().size;
        chunk.transform.position = lastChunk.transform.position + (Vector3.forward * colliderSize.z);

        AddMapChunk(chunk as ISpeedObserver);
    }

    public void AddMapChunk(ISpeedObserver observer)
    {
        if (speedObservers.Contains(observer) == false)
        {
            speedObservers.Add(observer);
        }
    }
}
