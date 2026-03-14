using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGame;

public class TouchManager : MonoBehaviour,ITouchable,ICountFinger,IPreWarmingObject
{

    public int fingerNumbers { get; set; } = 1;
    public float timeTouching { get; set; }
    public event System.Action OnTouchBegin;
    public event System.Action OnTouching;
    public event System.Action OnTouchEnd;


    private bool readyTouch;

    private void OnEnable()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.OnPreGameCountEnded += ActiveWarmingBehaviour;
        }
        else
        {
            ActiveWarmingBehaviour();
        }
    }

    private void Update()
    {
        if (!readyTouch)
        {
            return;
        }


#if UNITY_EDITOR
        // all change must be make for mobiles too
        if (Input.GetMouseButtonDown(0))
        {
            OnTouchBegin?.Invoke();
        }

        if (Input.GetMouseButton(0))
        {
            timeTouching+= Time.deltaTime;
            OnTouching?.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
        {
            timeTouching = 0;
            OnTouchEnd?.Invoke();
        }
#else
        if (Input.touchCount == 0 || timeTouching > fingerNumbers) { return; }


        foreach (Touch tap in Input.touches)
        {
            if (tap.phase == TouchPhase.Began)
            {
                OnTouchBegin?.Invoke();
            }

            if (tap.phase == TouchPhase.Stationary || tap.phase == TouchPhase.Moved)
            {
                timeTouching += Time.deltaTime;
                OnTouching?.Invoke();
            }

            if (tap.phase == TouchPhase.Ended)
            {
                timeTouching = 0;
                OnTouchEnd?.Invoke();
            }

        }

#endif
    }


    public void ActiveWarmingBehaviour()
    {

        readyTouch = true;
    }


}
