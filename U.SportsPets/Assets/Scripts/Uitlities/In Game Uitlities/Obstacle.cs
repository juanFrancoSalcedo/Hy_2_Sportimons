using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InGame
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private TriggerDetector triggerDetec;
        private void Start()
        {
            triggerDetec.OnTriggerEntered += DisableEnemy;
        }
        
        void DisableEnemy(Transform obj)
        {
            gameObject.SetActive(false);
            obj.GetComponent<IRespawneableCompetitor>().SetRespawnPosition(obj.position - transform.position.normalized);
            obj.GetComponent<IKillable>().Dead();
            obj.gameObject.SetActive(false);
        }
    }

}



