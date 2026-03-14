using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class AnimationController : DoAnimationController
{
    private SpriteRenderer _spriteRender;

    private new void OnEnable()
    {
        if (GetComponent<SpriteRenderer>())
        {
            _spriteRender = GetComponent<SpriteRenderer>();
        }
        originPosition = transform.position;
        originScale = transform.localScale;

        base.OnEnable();
    }
    
    public override void ActiveAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        
        switch (animationType)
        {
            case TypeAnimation.Move:
                transform.DOMove(targetPosition, timeAnimation, false).SetEase(animationCurve).SetDelay(delay).OnComplete(CallBacks);
                break;

            case TypeAnimation.MoveWorldPoint2D:
                transform.DOMove(worldPoint.position, timeAnimation).SetDelay(delay).SetEase(animationCurve).OnComplete(CallBacks);
                break;

            case TypeAnimation.MoveReturnOrigin:
                sequence.Append(transform.DOMove(targetPosition, timeAnimation, false).SetEase(animationCurve).SetDelay(delay));
                sequence.AppendInterval(coldTime);
                sequence.Append(transform.DOMove(targetPosition, timeAnimation, false).SetEase(animationCurve));
                break;

            case TypeAnimation.Scale:
                transform.localScale =  originScale;
                transform.DOScale(targetScale,timeAnimation).SetEase(animationCurve).SetDelay(delay).OnComplete(CallBacks);
                break;

            case TypeAnimation.ScaleFadeOut2D:
                transform.localScale = originScale;
                _spriteRender.DOFade(1,0);
                sequence.Append(transform.DOScale(targetScale, timeAnimation).SetEase(animationCurve).SetDelay(delay));
                sequence.AppendInterval(coldTime);
                sequence.Append(_spriteRender.DOFade(0,timeAnimation).SetEase(animationCurve).OnComplete(CallBacks));
                break;
                
            case TypeAnimation.MoveScaleAT:
                transform.DOMove(targetPosition,timeAnimation).SetEase(animationCurve).SetDelay(delay);
                transform.DOScale(targetScale,timeAnimation).SetEase(animationCurve).OnComplete(CallBacks);
                break;

            case TypeAnimation.FadeIn2D:
                _spriteRender.DOFade(0, 0);
                _spriteRender.DOFade(1, timeAnimation).SetEase(animationCurve).SetDelay(delay).OnComplete(CallBacks);
                break;
                
        }
    }
}
