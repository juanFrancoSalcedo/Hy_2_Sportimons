using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InGame
{
    public class ScoringParticipantOnTrigger : MonoBehaviour,IScoringPartner, IEndMatchAffiliable
    {
        [SerializeField] private bool disableObjScoring;
        public TriggerDetector triggerDetector;
        public ScoringObject scoreObject { get; set; }
        public PoolScoreBooster poolScore { get; set; }

        private void Start()
        {
            poolScore = GetComponent<PoolScoreBooster>();
            triggerDetector.OnTriggerEntered += AddPet;

            if (GetComponent<ScoringObject>())
            {
                scoreObject = GetComponent<ScoringObject>();
            }

            if (GameController.Instance != null)
            {

            }
        }
        
        private void AddPet(Transform si)
        {
            if (scoreObject != null)
            {
                AddScore();

                if (disableObjScoring)
                {
                    si.gameObject.SetActive(false);
                }
            }
        }

        public void AddScore()
        {
            scoreObject.IncreseScore(5);
            poolScore.PushBooster(transform.position,5);
        }

        public void EndMatchDelegate()
        {
            this.enabled = false;
        }
    }
}


