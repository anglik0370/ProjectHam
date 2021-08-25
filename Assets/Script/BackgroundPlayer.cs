using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class BackgroundPlayer: MonoBehaviour
{
    private Animator animator = null;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(PlayerMove());

    }

    void Update()
    {

    }

    IEnumerator PlayerMove()
    {
        while (true)
        {
            animator.Play("Player_Move");
            yield return new WaitForSeconds(0.01f);
        }
    }
}
