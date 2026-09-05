using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallPrefab : MonoBehaviour
{
    // Balls clean themselves up so misses don't pile up in the scene.
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
