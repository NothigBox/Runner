using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class MapChunk : PoolObject, ISpeedObserver
{
    new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void OnSpeedUpdate(float distance)
    {
        rigidbody.linearVelocity = Vector3.back * distance;
    }
}
