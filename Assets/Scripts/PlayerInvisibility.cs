using UnityEngine;
using System.Collections;

public class PlayerInvisibility : MonoBehaviour
{
    public float duration = 5f;
    public float fadeAlpha = 0.35f;

    private SpriteRenderer sr;
    private bool isInvisible = false;
    public bool IsInvisible => isInvisible;

    private PipeCollisionController[] allPipes;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // Get all existing pipes (and future ones)
        allPipes = FindObjectsOfType<PipeCollisionController>();
    }

    public void ActivateInvisibility(float dur)
    {
        duration = dur;
        StartCoroutine(InvisibilityRoutine());
    }

    private IEnumerator InvisibilityRoutine()
    {
        isInvisible = true;

        // fade player
        if (sr != null)
        {
            var c = sr.color;
            c.a = fadeAlpha;
            sr.color = c;
        }

        // disable collisions on all pipes
        UpdatePipeList();
        foreach (var p in allPipes)
            p.DisableCollisions();

        yield return new WaitForSeconds(duration);

        // re-enable pipe collisions
        foreach (var p in allPipes)
            p.EnableCollisions();

        // restore alpha
        if (sr != null)
        {
            var c = sr.color;
            c.a = 1f;
            sr.color = c;
        }

        isInvisible = false;
    }

    // refresh list for newly spawned pipes
    void UpdatePipeList()
    {
        allPipes = FindObjectsOfType<PipeCollisionController>();
    }
}
