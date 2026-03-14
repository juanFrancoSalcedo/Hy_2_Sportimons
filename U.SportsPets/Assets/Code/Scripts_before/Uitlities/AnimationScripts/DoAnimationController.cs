using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;


public abstract class DoAnimationController : MonoBehaviour
{
    public Vector3 targetPosition;
    public Vector3 targetScale;
    protected Vector3 originPosition;
    protected Vector3 originScale;
    public float timeAnimation;
    public float delay;
    public float coldTime;
    public Ease animationCurve;
    public bool playOnAwake;
    public TypeAnimation animationType;
    public Transform worldPoint;
    public Color colorTarget;
    public UnityEvent OnCompletedCallBack;
    public UnityEvent OnStartedCallBack;
    public event System.Action OnCompleted;

    public abstract void ActiveAnimation();
   

    protected void OnEnable()
    {
        if (playOnAwake)
        {
            ActiveAnimation();
        }
    }

    protected void CallBacks()
    {
         OnCompleted?.Invoke();
         OnCompletedCallBack?.Invoke();
    }

    public void SetColdTime(float _time)
    {
        coldTime = _time;
    }
}

public enum TypeAnimation
{
    Move,
    MoveReturnOrigin,
    MoveFadeOut,
    MoveBack,
    MoveScale,
    MoveUIPoint,
    MoveScaleUIPoint,
    MoveWorldPoint2D,
    MoveWorldPoint2DScale,
    MoveScaleWorldPoint2D,
    Scale,
    ScaleReturnOriginScale,
    ScaleFadeOut2D,
    FadeOut,
    ColorChange,
    MoveScaleAT,
    FadeIn2D,
    SwitchSprite,
    RotateFadeLoop
}


