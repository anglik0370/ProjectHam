using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAppear : MonoBehaviour
{
    [SerializeField]
    public Vector3 maxUp = Vector3.zero;

    public Vector2 playerLock = Vector2.zero;

    [SerializeField]
    public float upSpeed = 0.5f;

    private PlayerScript player = null;

    public bool isDetect = false;

    void Start()
    {
        player = FindObjectOfType<PlayerScript>();
    }

    void Update()
    {
        
    }

    public void Lock()
    {
        player.transform.position = playerLock;
    }

    public void JumpOn()
    {
        player.canJump = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            playerLock = player.transform.position;

            isDetect = true;
        }
    }
}
