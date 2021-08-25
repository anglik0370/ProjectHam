using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class BulletMove : MonoBehaviour
{
    private Vector2 targetPos = Vector2.zero;

    private PlayerScript player = null;

    private Animator animator = null;

    [SerializeField]
    private float bulletSpeed = 5f;
    [SerializeField]
    private float distance = 2f;

    [SerializeField]
    private GameObject col = null;

    void Start()
    {
        player = FindObjectOfType<PlayerScript>();

        animator = GetComponent<Animator>();
    }
    void Update()
    {
        targetPos = player.transform.position;

        if (Vector2.Distance(transform.position , targetPos) < distance)
        {
            animator.Play("Bullet_Destroy");

            Invoke("ColliderFalse", 0.08f);
            Invoke("DestroyBullet", 0.7f);
        }
        else
        {
            animator.Play("Bullet");
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, bulletSpeed * Time.deltaTime);
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void ColliderFalse()
    {
        col.SetActive(false);
    }
}
