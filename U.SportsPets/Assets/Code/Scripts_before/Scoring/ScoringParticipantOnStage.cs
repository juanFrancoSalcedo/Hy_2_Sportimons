using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class ScoringParticipantOnStage : MonoBehaviour,IScoringPartner,IPreWarmingObject, IEndMatchAffiliable
    {
        public ScoringObject scoreObject { get; set; }
        [SerializeField] private float timeAddScore = 0.6f;
        public PoolScoreBooster poolScore { get; set; }

        private void OnEnable()
        {
            if (GetComponent<ScoringObject>())
            {
                scoreObject = GetComponent<ScoringObject>();
                poolScore = GetComponent<PoolScoreBooster>();
            }
            else
            {
                Debug.LogError("I "+name+ " NEED SCORING OBJECT COMPONENT");
            }
        }

        void Start()
        {
            if(GameController.Instance != null)
            {
                GameController.Instance.OnPreGameCountEnded += ActiveWarmingBehaviour;
                GameController.Instance.OnMatchEnded += EndMatchDelegate;
            }
        }

        public void ActiveWarmingBehaviour()
        {
            AddScore();
        }

        public void AddScore()
        {
            if (scoreObject != null)
            {
                scoreObject.IncreseScore(1);
                Invoke("AddScore",timeAddScore);
                //poolScore.PushBooster();
            }
        }

        private void OnDisable()
        {
            scoreObject = null;
        }

        public void EndMatchDelegate()
        {
            this.enabled = false;
        }
    }


}


