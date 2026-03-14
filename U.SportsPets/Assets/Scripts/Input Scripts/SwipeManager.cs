using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour,ISwipeable,ICountFinger
{

    public int fingerNumbers { get; set; }
    public float magnitudSwipe { get; set; }
    public event System.Action<Vector3> OnSwipedMagnitud;
    public event System.Action<SwipeDirection> OnSwiped;
    public float MinSwipeMagnitude = 100;
    private Vector3 startPos = Vector3.zero;
    public float scaleScreen;

    private void Awake()
    {
        CalculateScreenScale();
    }

    private void CalculateScreenScale()
    {
        float benevolenteX;
        float benevolenteY;
        benevolenteX = Mathf.Abs(Screen.width - 1440);
        benevolenteY = Mathf.Abs(Screen.height - 2960);
        float scaleScreenFromOrigX = (benevolenteX / 1440) + 1;
        float scaleScreenFromOrigY = (benevolenteY / 2960) + 1;
        scaleScreen = (scaleScreenFromOrigX + scaleScreenFromOrigY) / 2;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnSwipedMagnitud?.Invoke(GetSwipeMagnitud(startPos,Input.mousePosition));
            OnSwiped?.Invoke(GetSwipeDirection(Input.mousePosition - startPos));
        }
#else
        if (Input.touchCount == 0) return;
        if (Input.GetTouch(0).phase == TouchPhase.Began) { startPos = Input.GetTouch(0).position; }
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            OnSwiped?.Invoke(GetSwipeDirection((Vector3)Input.GetTouch(0).position - startPos));
            OnSwipedMagnitud?.Invoke(GetSwipeMagnitud(startPos, (Vector3)Input.GetTouch(0).position));
        }
#endif
    }

    public SwipeDirection GetSwipeDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);


        if (positiveX + positiveY < MinSwipeMagnitude)
        {
            return SwipeDirection.None;

        }

        SwipeDirection draggedDir = SwipeDirection.None;

        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? SwipeDirection.Right : SwipeDirection.Left;
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? SwipeDirection.Up : SwipeDirection.Down;
        }

        return draggedDir;
    }

    public Vector3 GetSwipeMagnitud(Vector3 i,Vector3 f)
    {
       
        return f-i;
    }

}