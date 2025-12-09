// Assets/Scripts/CoinPickup.cs
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int value = 1;
    public AudioClip pickupSfx;
    public ParticleSystem pickupParticles;
    public float destroyAfterSound = 0.1f; // small default

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        // Add score
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddScore(value);

        // Play particles
        if (pickupParticles != null)
        {
            var ps = Instantiate(pickupParticles, transform.position, Quaternion.identity);
            ps.Play();
            Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
        }

        // Play sound and disable visuals/collider to avoid double pickups
        if (pickupSfx != null && audioSource != null)
        {
            spriteRenderer.enabled = false;
            col.enabled = false;
            audioSource.PlayOneShot(pickupSfx);
            Destroy(gameObject, pickupSfx.length + 0.05f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
