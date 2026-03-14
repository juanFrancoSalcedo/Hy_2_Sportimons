using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using MyBox;


public class AnimationUIController : DoAnimationController
{
    private RectTransform rectTransform;
    private Image image;
    public Sprite spriteToShift;
    private Sprite spriteOriginal;

    private new void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        originPosition = rectTransform.anchoredPosition;
        originScale = rectTransform.localScale;
        spriteOriginal = image.sprite;

        base.OnEnable();
    }
    
    public override void ActiveAnimation()
    {
        OnStartedCallBack?.Invoke();
        Sequence sequence = DOTween.Sequence();
        
        switch (animationType)
        {

            case TypeAnimation.Move:
                sequence.Append(rectTransform.DOAnchorPos(targetPosition, timeAnimation, false).SetEase(animationCurve).SetDelay(delay));
                sequence.AppendInterval(coldTime).OnComplete(CallBacks); 
                
                break;

            case TypeAnimation.MoveBack:
                rectTransform.DOAnchorPos(originPosition, timeAnimation,false).SetEase(animationCurve).SetDelay(delay).OnComplete(CallBacks);
                break;

            case TypeAnimation.MoveReturnOrigin:
                sequence.Append(rectTransform.DOAnchorPos(targetPosition, timeAnimation,false).SetEase(animationCurve).SetDelay(delay));
                sequence.AppendInterval(coldTime);
                sequence.Append(rectTransform.DOAnchorPos(originPosition,timeAnimation,false).SetEase(animationCurve).OnComplete(CallBacks));
                break;

            case TypeAnimation.MoveFadeOut:
                sequence.Append(image.DOFade(1, 0).SetDelay(delay));
                sequence.Append(rectTransform.DOAnchorPos(targetPosition, timeAnimation, false).SetEase(animationCurve));
                sequence.AppendInterval(coldTime);
                sequence.Append(image.DOFade(0, timeAnimation).SetEase(animationCurve).OnComplete(CallBacks));
                break;

            case TypeAnimation.MoveScale:
                sequence.Append(rectTransform.DOAnchorPos(targetPosition, timeAnimation, false).SetEase(animationCurve).SetDelay(delay));
                sequence.AppendInterval(coldTime);
                sequence.Append(rectTransform.DOScale(targetScale, timeAnimation).SetEase(animationCurve).OnComplete(CallBacks));
                break;

            case TypeAnimation.Scale:

                sequence.Append(rectTransform.DOScale(targetScale, timeAnimation).SetEase(animationCurve).SetDelay(delay));
                sequence.AppendInterval(coldTime).OnComplete(CallBacks);
                //rectTransform.DOScale(targetScale, timeAnimation).SetEase(animationCurve).SetDelay(delay).OnComplete(CallBacks);
                break;

            case TypeAnimation.ScaleReturnOriginScale:
                sequence.Append(rectTransform.DOScale(targetScale, timeAnimation).SetEase(animationCurve).SetDelay(delay));
                sequence.AppendInterval(coldTime);
                sequence.Append(rectTransform.DOScale(originScale, timeAnimation).SetEase(animationCurve).OnComplete(CallBacks));
                break;

            case TypeAnimation.MoveWorldPoint2D:
                Vector3 worldPos = worldPoint.position;
                float z = Camera.main.nearClipPlane;
                worldPos.z = z;
                targetPosition = worldPos;
                rectTransform.DOMove(targetPosition, timeAnimation, false).SetEase(animationCurve).SetDelay(delay).OnComplete(CallBacks);
                break;

            case TypeAnimation.MoveWorldPoint2DScale:
                worldPos = worldPoint.position;
                z = Camera.main.nearClipPlane;
                worldPos.z = z;
                targetPosition = worldPos;
                sequence.Append(rectTransform.DOMove(targetPosition, timeAnimation, false).SetEase(animationCurve).SetDelay(delay));
                sequence.AppendInterval(coldTime);
                sequence.Append(rectTransform.DOScale(targetScale,timeAnimation).SetEase(animationCurve).OnComplete(CallBacks));
                break;

            case TypeAnimation.MoveScaleWorldPoint2D:
                worldPos = worldPoint.position;
                z = Camera.main.nearClipPlane;
                worldPos.z = z;
                targetPosition = worldPos;
                sequence.Append(rectTransform.DOMove(targetPosition, timeAnimation, false).SetEase(animationCurve).SetDelay(delay));
                sequence.AppendInterval(coldTime);
                sequence.Append(rectTransform.DOScale(targetScale, timeAnimation).SetEase(animationCurve).OnComplete(CallBacks));
                break;

            case TypeAnimation.MoveUIPoint:
                targetPosition = worldPoint.position;
                rectTransform.DOMove(targetPosition, timeAnimation, false).SetEase(animationCurve).SetDelay(delay).OnComplete(CallBacks);
                break;

            case TypeAnimation.MoveScaleUIPoint:
                targetPosition = worldPoint.position;
                sequence.Append(rectTransform.DOMove(targetPosition, timeAnimation, false).SetEase(animationCurve).SetDelay(delay));
                sequence.AppendInterval(coldTime);
                sequence.Append(rectTransform.DOScale(targetScale, timeAnimation).SetEase(animationCurve).OnComplete(CallBacks));
                break;

            case TypeAnimation.ColorChange:
                image.DOColor(colorTarget,timeAnimation).SetEase(animationCurve).SetDelay(delay).OnComplete(CallBacks);
                break;

            case TypeAnimation.FadeOut:
                image.DOFade(1,0);
                image.DOFade(0,timeAnimation).SetEase(animationCurve).SetDelay(delay).OnComplete(CallBacks);
                break;

            case TypeAnimation.SwitchSprite:
                if (ReferenceEquals(image.sprite,spriteOriginal))
                {image.sprite = spriteToShift;}
                else
                {image.sprite = spriteOriginal;}
                break;
                
        }
    }
    
}

