using System.Collections;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float maxRotation = 30f;
    [SerializeField] private float minRotation = -90f;

    private Rigidbody2D rb;
    private bool isDead = false;

    private PlayerInvisibility invisibility;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogError("BirdController: Rigidbody2D missing on " + gameObject.name);

        invisibility = GetComponent<PlayerInvisibility>();
        if (invisibility == null)
            Debug.LogWarning("BirdController: PlayerInvisibility component missing. Add PlayerInvisibility to player for power-up support.");
    }

    void Update()
    {
        if (isDead) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            Jump();

        RotateBird();
    }

    void Jump()
    {
        if (rb == null) return;
        rb.velocity = Vector2.zero;
        rb.velocity = Vector2.up * jumpForce;
    }

    void RotateBird()
    {
        if (rb == null) return;

        float rotation = 0f;
        if (rb.velocity.y > 0) rotation = maxRotation;
        else rotation = Mathf.Lerp(maxRotation, minRotation, -rb.velocity.y / 10f);

        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If invisibility exists and is active, ignore death
        if (invisibility != null && invisibility.IsInvisible)
        {
            Debug.Log("BirdController: Collision while invisible - ignoring death.");
            return;
        }

        if (!isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        GameManager.Instance.GameOver();
    }
}
