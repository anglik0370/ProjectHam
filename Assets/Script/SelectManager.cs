using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SelectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject text = null;

    void Start()
    {
        StartCoroutine(Blink());
    }

    void Update()
    {

    }

    IEnumerator Blink ()
    {
        while (true)
        {
            text.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            text.SetActive(true);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
