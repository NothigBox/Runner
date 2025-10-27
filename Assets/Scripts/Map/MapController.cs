using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private float ticksPerSecond;
    [SerializeField] private float distance;

    private float timer;

    private List<ISpeedObserver> speedObservers;

    private void Awake()
    {
        timer = 0f;
        speedObservers = new List<ISpeedObserver>();
    }

    private void FixedUpdate()
    {
        float period = 1f / ticksPerSecond;

        if (timer >= period)
        {
            timer = 0f;
            NotifyObservers(distance);
        }

        timer += Time.fixedDeltaTime;
    }

    public void NotifyObservers(float distance)
    {
        for (int i = 0; i < speedObservers.Count; i++) 
        {
            speedObservers[i].OnSpeedUpdate(distance);
        }
    }

    public void AddObserver(ISpeedObserver observer)
    {
        if (speedObservers.Contains(observer) == false)
        {
            speedObservers.Add(observer);
        }
    }
}
