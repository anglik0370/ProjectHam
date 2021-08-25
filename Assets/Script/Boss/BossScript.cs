using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions.Must;

public class BossScript : MonoBehaviour
{
    private BossAppear appear = null;
    private PlayerScript player = null;

    [SerializeField]
    private Transform attackPos = null;

    [SerializeField]
    private GameObject bullet = null;

    [SerializeField]
    private GameObject raider = null;

    private Animator animator = null;
    private SpriteRenderer spriteRenderer = null;

    [SerializeField]
    private float speed = 0.5f;

    private Vector2 direction = Vector2.left;

    private bool canMove = false;

    private float timer = 0f;
    [SerializeField]
    private float repeat = 1f;

    private float bossTimer = 0;

    void Start()
    {
        appear = FindObjectOfType<BossAppear>();
        player = FindObjectOfType<PlayerScript>();

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        bossTimer = Time.deltaTime;

        if (bossTimer >= 180)
        {
            SceneManager.LoadScene("7");
        }

        if (appear.isDetect == true)
        {
            appear.Lock();

            if (transform.position.y >= appear.maxUp.y)
            {
                animator.Play("Boss_Wakeup");

                raider.SetActive(false);

                Invoke("SetDetectFalse", 1.6f);

                canMove = true;
            }
            else
            {
                animator.Play("Boss_Wakedown");

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, appear.maxUp.y), appear.upSpeed * Time.deltaTime);
            }
        }
        else if (canMove == true)
        {
            timer += Time.deltaTime;
            if(timer > repeat)
            {
                Instantiate(bullet, attackPos.position, Quaternion.identity);
                timer = 0f;
            }

            animator.Play("Boss_Charge");

            appear.JumpOn();

            transform.Translate(direction * speed * Time.deltaTime);

            if (transform.position.x <= 10)
            {
                direction = Vector2.right;
            }
            if (transform.position.x >= 40)
            {
                direction = Vector2.left;
            }

            if (transform.position.x - player.transform.position.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (transform.position.x - player.transform.position.x < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    private void SetDetectFalse()
    {
        appear.isDetect = false;
    }
}
