using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;

    private float xMove = 0f;

    [SerializeField]
    private float jumpForce = 10f;
    private float jumpLimit = 0.5f;

    private float jumpTimer = 0f;

    internal bool canJump = false;

    private GameManager gameManager = null;
    private GameObject player = null;
    private Rigidbody2D rigidbody2D = null;
    private Animator animator = null;
    private SpriteRenderer spriteRenderer = null;
    private Collider2D col = null;

    [SerializeField]
    private Collider2D attackCol = null;

    private Collider2D colli = null;
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        col = GetComponent<Collider2D>();

        col.enabled = true;
    }

    void Update()
    {

        xMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        player.transform.Translate(new Vector3(xMove, 0, 0));

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            canJump = false;
            animator.Play("Player_Idle");
        }

        if (canJump == false)
        {
            Flip();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Jump();
            Flip();
        }
        else
        {
            if (canJump == true)
            {
                if (!Input.anyKey)
                {
                    animator.Play("Player_Idle");
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    spriteRenderer.flipX = false;
                    animator.Play("Player_Move");
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    spriteRenderer.flipX = true;
                    animator.Play("Player_Move");
                }
            }
        }
    }

    private void Jump()
    {
        if (jumpTimer >= jumpLimit)
        {
            canJump = false;
            animator.Play("Player_Idle");
        }

        if (canJump == true)
        {
            jumpTimer += Time.deltaTime;

            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.AddForce(Vector2.up * jumpForce * (4 - jumpTimer), ForceMode2D.Impulse);

            animator.Play("Player_Jump");
        }
    }

    private void Flip()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            spriteRenderer.flipX = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            spriteRenderer.flipX = false;
        }
    }

    IEnumerator invincibility()
    {
        attackCol.enabled = false;

        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.enabled = false;

            yield return new WaitForSeconds(0.25f);

            spriteRenderer.enabled = true;

            yield return new WaitForSeconds(0.25f);
        }

        attackCol.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Ground") || col.gameObject.tag.Equals("Enemy"))
        {
            canJump = true;
            jumpTimer = 0;
        }

        if (col.gameObject.tag.Equals("Enemy"))
        {
            Destroy(col.transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        colli = col;

        if (col.gameObject.tag.Equals("Enemy_Body"))
        {
            if (gameManager.life > 0)
            {
                StartCoroutine(invincibility());
            }

            gameManager.Die();
        }

        if (col.gameObject.tag.Equals("Coin"))
        {
            gameManager.UpdateScore();

            Destroy(col.gameObject);
        }
    }
}


