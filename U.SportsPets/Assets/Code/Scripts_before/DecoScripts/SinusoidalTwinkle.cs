using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinusoidalTwinkle : MonoBehaviour
{
    Image imageComp;
    [SerializeField] bool sin;
    [Range(0f, 1f)]
    [SerializeField] float normalAlpha =0.2f;
    void Start()
    {
        imageComp = GetComponent<Image>();
    }
    
    void Update()
    {
        if (sin)
        {
            imageComp.color = new Color(imageComp.color.r, imageComp.color.g, imageComp.color.b, normalAlpha + (Mathf.Sin(Time.time) / 6));
        }
        else
        {
            imageComp.color = new Color(imageComp.color.r, imageComp.color.g, imageComp.color.b, normalAlpha + (Mathf.Cos(Time.time) / 6));
        }
    }
}
