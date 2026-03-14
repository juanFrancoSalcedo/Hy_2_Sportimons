using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InGame
{
    public class DisableByTime : MonoBehaviour
    {
        [SerializeField] private bool beginInAwake;
        public float timeDisab;

        private void Start()
        {
            if (beginInAwake)
            {
                BeginDisableTime();
            }
        }

        public void BeginDisableTime()
        {
            Invoke("DisableThisObject", timeDisab);
        }

        private void DisableThisObject()
        {
            gameObject.SetActive(false);
        }

    }
}

