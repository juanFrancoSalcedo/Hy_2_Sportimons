using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class LimitsPosition : MonoBehaviour
    {
        public Vector3 minLimits;
        public bool useXLimit;
        public bool useYLimit;
        public bool useZLimit;

        
        private Vector3 startPosition;

        private void Start()
        {
            startPosition = transform.position;
        }


        private void Update()
        {

            if (minLimits.x > transform.position.x  && useXLimit)
            {
                transform.position = startPosition;
            }

            if (minLimits.y > transform.position.y && useYLimit)
            {
                transform.position = startPosition;

            }

            if (minLimits.z > transform.position.z && useZLimit)
            {
                transform.position = startPosition;
            }
        }
    }
}


