
using UnityEngine;

public class InvisibilityPowerUp : MonoBehaviour
{
    [Tooltip("Seconds of invisibility to give when collected")]
    public float duration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("InvisibilityPowerUp: Player touched power-up.");

            PlayerInvisibility p = other.GetComponent<PlayerInvisibility>();
            if (p != null)
            {
                p.ActivateInvisibility(duration);
                Debug.Log("InvisibilityPowerUp: Activated PlayerInvisibility on player.");
            }
            else
            {
                Debug.LogWarning("InvisibilityPowerUp: PlayerInvisibility component not found on Player!");
            }

            Destroy(gameObject);
        }
    }
}
