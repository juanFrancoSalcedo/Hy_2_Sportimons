using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitsPostionUpdate : MonoBehaviour
{
    public Vector3 minLimits;
    public Vector3 maxLimits;
    public bool useXLimit;
    public bool useYLimit;
    public bool useZLimit;

    private void Update()
    {
        Vector3 currentPos = transform.position; 

        if (minLimits.x > transform.position.x && useXLimit)
        {
            currentPos.x = minLimits.x;
            transform.position = currentPos;
        }

        if (minLimits.y > transform.position.y && useYLimit)
        {
            currentPos.y = minLimits.y;
            transform.position = currentPos;
        }

        if (minLimits.z > transform.position.z && useZLimit)
        {
            currentPos.z = minLimits.z;
            transform.position = currentPos;
        }

        //------------------------------------tele

        if (maxLimits.x < transform.position.x && useXLimit)
        {
            currentPos.x = maxLimits.x;
            transform.position = currentPos;
        }

        if (maxLimits.y < transform.position.y && useYLimit)
        {
            currentPos.y = maxLimits.y;
            transform.position = currentPos;
        }

        if (maxLimits.z < transform.position.z && useZLimit)
        {
            currentPos.z = maxLimits.z;
            transform.position = currentPos;
        }
    }
}
