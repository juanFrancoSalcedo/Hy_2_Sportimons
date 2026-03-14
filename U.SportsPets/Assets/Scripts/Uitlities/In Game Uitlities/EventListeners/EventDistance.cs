using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace InGame
{
    public class EventDistance : MonoBehaviour
    {
        public UnityEvent OnArrived;

        [SerializeField] private float minDistance = 3f;
        [SerializeField] private Transform pointB;
        private bool arrived;
        
        void Update()
        {
            if (!arrived)
            {
                if (Vector3.Distance(transform.position, pointB.position) < minDistance)
                {
                    OnArrived?.Invoke();
                    arrived = true;
                }
            }
        }
    }
}


