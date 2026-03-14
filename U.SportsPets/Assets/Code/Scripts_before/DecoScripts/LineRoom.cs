using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRoom : MonoBehaviour
{
    LineRenderer lineRend;
    Vector3[] salary;
    bool move = true;
    float so = 1;
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        so = Random.Range(0.5f,4f);
        Color _color =new Color ((127f/255f)*so,(47f/255f)*so, (47f/255f)*so,1);
        lineRend.material.SetColor("_EmissionColor", _color);
        salary = new Vector3[2];
        salary[0] = lineRend.GetPosition(0);
        salary[1] = lineRend.GetPosition(1);
        RandomBehaviour();
    }

    float prito;
    Vector3 pos;
    float t;
    bool dirDer;

    void Update()
    {
        t += Time.deltaTime*0.6f;
        if (move)
        {
            if (dirDer)
            {
                pos = Vector3.Lerp(salary[0], salary[1], t);
            }
            else
            {
                pos = Vector3.Lerp(salary[1], salary[0], t);
            }

            lineRend.SetPosition(0,(salary[0].normalized*3)+pos);
            lineRend.SetPosition(1, (salary[1].normalized * 3) + pos);
            if (t > 1) { t = 0; }
        }
        else
        {
            lineRend.SetPosition(0,salary[0]);
            lineRend.SetPosition(1,salary[1]);
            prito = Mathf.Sin(Time.time);
            float tre = so + Mathf.Abs(prito);
            Color _color = new Color((127f / 255f) * tre, (47f / 255f) * tre, (47f / 255f) * tre, 1);
            lineRend.material.SetColor("_EmissionColor", _color);
        }
    }

    void RandomBehaviour()
    {
        int i = Random.Range(-3, 3);
        int m = Random.Range(-3, 3);
        dirDer = (m > 0) ? true : false;
        move = (i> 0) ? true: false ;
        Invoke("RandomBehaviour",Random.Range(3f,6f));
    }
}
