using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetTexture : MonoBehaviour
{

    Renderer rend;
    [SerializeField] float velocity=1;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    float offsetY;
    // Update is called once per frame
    void Update()
    {

        offsetY -= Time.deltaTime;
        Vector2 solid = new Vector2(0, offsetY * velocity);
        rend.material.SetTextureOffset("_MainTex",solid);
        
    }
}
