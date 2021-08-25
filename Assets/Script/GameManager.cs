using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lifeText = null;
    [SerializeField]
    private TextMeshProUGUI scoreText = null;

    private GameObject player = null;

    private PlayerScript PlayerScript;

    public int life = 3;

    public int score = 0;

    void Start()
    {
        player = GameObject.Find("Player");

        PlayerScript = player.GetComponent<PlayerScript>();

        lifeText.text = "x" + life.ToString();
        scoreText.text = "x" + score.ToString();
    }

    void Update()
    {

        if (player.transform.position.y <= -12)
        {
            Die();

            player.transform.position = new Vector3(-2, 0, 0);

            PlayerScript.canJump = true;
        }
    }

    public void Die()
    {
        if (life > 0)
        {
            life--;

            lifeText.text = "x" + life.ToString();
        }
        else
        {
            SceneManager.LoadScene(4);
        }
    }

    public void UpdateScore()
    {
        score++;

        scoreText.text = "x" + score.ToString();
    }
}