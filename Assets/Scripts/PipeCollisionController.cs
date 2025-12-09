using UnityEngine;

public class PipeCollisionController : MonoBehaviour
{
    private Collider2D[] allColliders;

    void Awake()
    {
        // Get all colliders in this pipe group
        allColliders = GetComponentsInChildren<Collider2D>(true);
    }

    public void DisableCollisions()
    {
        foreach (var c in allColliders)
            c.enabled = false;
    }

    public void EnableCollisions()
    {
        foreach (var c in allColliders)
            c.enabled = true;
    }
}
