using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    [SerializeField]
    private bool isActive = false;

    [SerializeField]
    private GameObject optionObject = null;

    void Start()
    {
        optionObject.SetActive(isActive);
    }
    void Update()
    {
        Option();
        optionObject.SetActive(isActive);
    }

    private void Option()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isActive == false)
            {
                optionObject.SetActive(true);

                Time.timeScale = 0;

                isActive = true;
            }
            else if (isActive == true)
            {
                optionObject.SetActive(false);

                Time.timeScale = 1;

                isActive = false;
            }
        }
    }
    public void Resume()
    {
        if (isActive == true)
        {
            optionObject.SetActive(false);

            Time.timeScale = 1;

            isActive = false;
        }
    }
}
