using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BakcgroundMove : MonoBehaviour
{
    [SerializeField]
    protected float speed = 0.5f;

    private MeshRenderer mr = null;
    private Material material = null;

    private float offsetX = 0f;
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        material = mr.material;
    }

    void Update()
    {
        offsetX += Time.deltaTime * speed;
        material.SetTextureOffset("_MainTex", new Vector2(offsetX, 0f));
        
    }
}
