using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;


public class IBleed : MonoBehaviour
{
    [SerializeField] GameObject onjo;

    [ButtonMethod]
    void Camina()
    {
        GameObject ter =  Instantiate(onjo,transform);
        ter.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
        ter.transform.localPosition = new Vector3(0, -0.5f, 0);
    }
    
}
