using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace InGame
{
    public class PortalInstanciator : MonoBehaviour
    {
        public GameObject meteorPrefab;
        [SerializeField] private float timeDisable =0.2f;
        [SerializeField] private bool disableOnAwake;
        private bool instanceOnAwake;
        [SerializeField] private float timeEnable;
        [SerializeField] private float timeInstanciate = 0;

        private void Start()
        {
            if (disableOnAwake)
            {
                GameController.Instance.StartCoroutine(GameController.Instance.ResurrectObject(timeEnable, gameObject));
                instanceOnAwake = true;
                gameObject.SetActive(false);
            }
        }

        public void AppearPrefab()
        {
            CancelInvoke("AppearPrefab");
            GameObject meteor = Instantiate(meteorPrefab, transform.position, Quaternion.identity);
            meteor.SetActive(true);
            Invoke("DisableByTime",timeDisable);
        }

        private void OnEnable()
        {
            if (instanceOnAwake)
            {
                Invoke("AppearPrefab", timeInstanciate);
            }
        }

        private void DisableByTime()
        {
            gameObject.SetActive(false);
        }

    }
}

