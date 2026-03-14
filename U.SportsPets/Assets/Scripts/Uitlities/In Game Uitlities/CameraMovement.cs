using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    Vector3 beginOffset;
    [SerializeField]GameObject target;
    

    Vector3 viewPoint;
    float timeMove;

    void Awake()
    {

        beginOffset = offset;
    }
    void LateUpdate()
    {
        transform.position = Vector3.Slerp(transform.position,target.transform.position+offset,Time.deltaTime);

        transform.LookAt(target.transform.position);
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    public void Zoom(Transform zoomPoint)
    {
      float distLastGift = Vector3.Distance(target.transform.position, zoomPoint.position);
        offset.y= beginOffset.y + distLastGift;
    }
}
