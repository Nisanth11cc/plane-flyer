using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    private float width;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // When moved left beyond the width, reset
        if (transform.position.x < startPos.x - width)
        {
            transform.position = startPos;
        }
    }
}
