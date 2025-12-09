using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    // Speed of the scrolling. 
    // Positive (0.5) moves left (texture moves right). 
    // Negative (-0.5) moves right.
    public float scrollSpeed = 0.5f;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // This calculates the offset based on time
        float offset = Time.time * scrollSpeed;

        // This moves the texture on the X axis
        rend.material.mainTextureOffset = new Vector2(offset, 0);
    }
}