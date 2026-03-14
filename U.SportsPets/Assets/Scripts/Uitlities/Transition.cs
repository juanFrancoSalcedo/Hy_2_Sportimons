using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public DoAnimationController animatorController;
    public static Transition Instance { get; private set; }
    private Vector3 posInitial;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
        }

        if (Instance == this)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        posInitial = animatorController.GetComponent<RectTransform>().position;

    }

    private void Update()
    {

    }

    public void BeginTransition()
    {
        animatorController.targetPosition = new Vector3(0,0,0);
        animatorController.ActiveAnimation();
        Invoke("EndTransition",animatorController.timeAnimation+ animatorController.delay+ animatorController.coldTime);
    }

    private void EndTransition()
    {
        animatorController.targetPosition = new Vector3(1500, 0, 0);
        animatorController.ActiveAnimation();
        Invoke("ReturnPosition", animatorController.timeAnimation + animatorController.delay + animatorController.coldTime);
    }

    private void ReturnPosition()
    {
        animatorController.GetComponent<RectTransform>().position = posInitial;
    }
}
