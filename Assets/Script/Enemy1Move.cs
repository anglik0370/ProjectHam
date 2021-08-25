using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Enemy1Move : MonoBehaviour
{
    private Vector3 pos = Vector3.zero;

    [SerializeField]
    private Vector2 yPos = Vector2.zero;

    private float position = 0f;

    [SerializeField]
    private float limitMax = 2.0f;
    [SerializeField]
    private float speed = 3.0f;
    private float dash = 0f;
    [SerializeField]
    private float trackingSpeed = 10f;

    private Vector2 targetPosition = Vector2.zero;
    private Vector2 playerPosition = Vector2.zero;

    private GameObject player = null;

    private SpriteRenderer spriteRenderer = null;
    private Animator animator = null;

    bool isDash = false;
    bool isTracking = false;
    bool canDash = true;
    void Start()
    {
        pos = transform.position;
        position = transform.position.x;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        player = GameObject.Find("Player");
    }

    void Update()
    {
        Vector3 currntPosition = pos;
        
        if (isTracking == true)
        {
            targetPosition = player.transform.position;
            playerPosition = player.transform.position;

            if (position > targetPosition.x)
            {
                spriteRenderer.flipX = false;
            }
            else if (position < targetPosition.x)
            {
                spriteRenderer.flipX = true;
            }

            if (canDash == true)
            {
                isDash = true;
                StartCoroutine(Dash());
                isDash = false;

                if (isDash == false)
                {
                    if (transform.position.x > player.transform.position.x)
                    {
                        spriteRenderer.flipX = false;
                    }
                    else if (transform.position.x < player.transform.position.x)
                    {
                        spriteRenderer.flipX = true;
                    }
                }
            }
        }
        else
        {
            currntPosition.x += limitMax * Mathf.Sin(Time.time * speed + dash);
            transform.position = currntPosition;

            if (position - limitMax + 0.1f >= currntPosition.x)
            {
                spriteRenderer.flipX = true;
            }
            else if (position + limitMax - 0.1f <= currntPosition.x)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag.Equals("Attack"))
        {
            isTracking = true;
        }
    }

    IEnumerator Dash()
    {
        targetPosition = playerPosition;

        animator.Play("Enemy1_Charge");

        yield return new WaitForSeconds(0.5f);

        animator.Play("Enemy1_Dash");

        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, yPos.y), new Vector3(targetPosition.x, yPos.y), trackingSpeed * Time.deltaTime);

        yield return new WaitForSeconds(0.2f);

        animator.Play("Enemy1_Idle");

        canDash = false;
        isTracking = false;

        yield return new WaitForSeconds(3f);

        canDash = true;
    }
}
