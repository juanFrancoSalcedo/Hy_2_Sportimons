using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace InGame
{
    public class EventTriggerListener : MonoBehaviour
    {
        public UnityEvent OnTriggerEnterCallBack;

        void Start()
        {
            GetComponent<TriggerDetector>().OnTriggerEntered += OnEnterObject;
        }

        private void OnEnterObject(Transform other)
        {
            OnTriggerEnterCallBack?.Invoke();
        }
    }


}


