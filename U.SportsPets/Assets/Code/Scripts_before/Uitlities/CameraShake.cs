using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    [SerializeField] bool cenital;

    void Shake()
    {
        StartCoroutine(ShakeCor());
    }

    IEnumerator ShakeCor()
    {
        float ever = 0.15f;
        int countTimes = 0;
        Vector3 origPos = transform.position;
        while (countTimes < 10)
        {
            transform.position =origPos+ new Vector3(Random.Range(-ever,ever), Random.Range(-ever,ever), Random.Range(-ever, ever));
            countTimes++;
            yield return new WaitForEndOfFrame();
        }

        transform.position = origPos;
    }

    public void ZoomOut()
    {
        Vector3 final = transform.position;

        if (cenital)
        {
            transform.DOMoveY(final.y + Vector3.up.y * 3, 0.7f);
        }
        else
        {
            transform.DOMoveZ(final.z - Vector3.forward.z * 3, 0.7f);
        }
    }
}
