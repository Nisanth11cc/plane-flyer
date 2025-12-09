using UnityEngine;

public class PlaneDash : MonoBehaviour
{
    public float dashSpeed = 12f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 1f;
    public GameObject dashBlastFX;

    private Rigidbody2D rb;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float cooldownTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        // DASH INPUT — double tap, or just single tap
        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer <= 0f)
        {
            StartDash();
        }

        // DASH ACTIVE
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
            {
                EndDash();
            }
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimer = dashDuration;
        cooldownTimer = dashCooldown;

        // Spawn blast effect behind plane
        if (dashBlastFX != null)
        {
            Instantiate(dashBlastFX, transform.position + new Vector3(-0.5f, 0, 0), Quaternion.identity);
        }

        // Apply dash force (to the right)
        rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
    }

    void EndDash()
    {
        isDashing = false;
        // stop horizontal boost (go back to normal)
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
}
