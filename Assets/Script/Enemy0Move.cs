using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0Move : MonoBehaviour
{
    private Vector3 pos = Vector3.zero;

    private float position = 0f;

    [SerializeField]
    private float limitMax = 2.0f;
    [SerializeField]
    private float speed = 3.0f;

    private SpriteRenderer spriteRenderer = null;

    void Start()
    {
        pos = transform.position;
        position = transform.position.x;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 currntPosition = pos;

        currntPosition.x += limitMax * Mathf.Sin(Time.time * speed);
        transform.position = currntPosition;

        if (position - limitMax + 0.1f >= currntPosition.x)
        {
            spriteRenderer.flipX = true;
        }
        else if (position + limitMax -0.1f <= currntPosition.x)
        {
            spriteRenderer.flipX = false;
        }
    }
}
