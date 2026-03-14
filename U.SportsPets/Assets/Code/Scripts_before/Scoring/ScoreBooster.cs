using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ScoreBooster : MonoBehaviour
{
    public TextMeshPro textMesh { get; set;}
    private RectTransform leader;
    [SerializeField] private Vector3 rotationForward;
    public Vector3 originalPos { get; set;}

    void Start()
    {
        leader = GameController.Instance.leaderBoard.GetComponent<RectTransform>();
        originalPos = transform.position;
    }

    private void OnEnable()
    {
        textMesh = GetComponent<TextMeshPro>();
        transform.rotation = Quaternion.Euler(rotationForward);
        transform.localScale = Vector3.zero;
        textMesh.DOFade(1, 0);
    }

    public void SetText(string txt)
    {
        textMesh.text = txt;
        Animate();
    }
    
    public void SetText()
    {
        Animate();
    }

    public void Animate()
    {
        leader = GameController.Instance.leaderBoard.GetComponent<RectTransform>();
        Sequence sequence = DOTween.Sequence();
        Vector3 scaleTo = new Vector3(0.6f, 0.6f, 0.6f);
        float z = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(leader.position + new Vector3(0, 0, z));
        sequence.Append(transform.DOScale(scaleTo, 0.1f)).SetEase(Ease.InOutCirc);
        sequence.Append(transform.DOMove(screenPos, 0.5f));
        sequence.Insert(0.6f, textMesh.DOFade(0, 1)).OnComplete(Disable);
    }

    public void Shake()
    {
        StopCoroutine(Shaking());
        StartCoroutine(Shaking());
    }

    IEnumerator Shaking()
    {
        int countVibra = 0;
        while (countVibra <9)
        {
            countVibra++;
            transform.position = originalPos + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f,0.1f));
            yield return new WaitForEndOfFrame();
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
