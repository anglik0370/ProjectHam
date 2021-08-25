using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LordScene : MonoBehaviour
{
    [SerializeField]
    protected GameObject button = null;

    [SerializeField]
    protected int scene = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneMove()
    {
        SceneManager.LoadScene(scene);

        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            SceneManager.LoadScene(scene);
        }

    }
}
