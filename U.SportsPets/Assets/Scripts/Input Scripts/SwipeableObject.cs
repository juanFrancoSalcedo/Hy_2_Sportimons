using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeableObject : MonoBehaviour
{
    public SwipeManager swipeManager;
    public bool useRigidbody = false;
    public bool object2d;
    private Rigidbody rigid;
    public float forceMovement { get; set; } = 0.027f;
    public float delayTime { get; set; } = 0;
    public event System.Action<Vector3> OnSwipedObject;

    void OnEnable()
    {
        swipeManager.OnSwipedMagnitud += MoveBySwipe;

        if (useRigidbody)
        {
            rigid = GetComponent<Rigidbody>();
        }
    }

    private void OnDisable()
    {
        swipeManager.OnSwipedMagnitud -= MoveBySwipe;
    }

    void MoveBySwipe(Vector3 dir)
    {
        Vector3 patron = dir;

        if (useRigidbody)
        {
            if (object2d)
            {
                StartCoroutine(MoveRigidbody(dir));
            }
            else
            {
                StartCoroutine(MoveRigidbody(new Vector3(dir.x,0,dir.y)));
            }
        }
        else
        {
            if (object2d)
            {
                StartCoroutine(MoveTransform(dir));
            }
            else
            {
                StartCoroutine(MoveTransform(new Vector3(dir.x, 0, dir.y)));
            }
        }
        OnSwipedObject?.Invoke(dir);
    }

    private IEnumerator MoveTransform(Vector3 directionAxis)
    {
        yield return new WaitForSeconds(delayTime);
        //TODO use swipeManager scaleScreen for multi resolutions
        transform.position = Vector3.MoveTowards(transform.position, directionAxis, 2);
    }

    private IEnumerator MoveRigidbody(Vector3 directionAxis)
    {
        yield return new WaitForSeconds(delayTime);
        rigid.AddForce(directionAxis*forceMovement*swipeManager.scaleScreen,ForceMode.Impulse);
    }

}
