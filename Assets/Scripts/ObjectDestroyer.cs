using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public float lifespan = 10f;

    void Start()
    {
        Destroy(gameObject, lifespan);
    }
}