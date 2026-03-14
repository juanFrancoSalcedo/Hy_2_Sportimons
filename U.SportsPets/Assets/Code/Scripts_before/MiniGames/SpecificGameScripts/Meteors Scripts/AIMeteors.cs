using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Meteors
{
    public class AIMeteors : MonoBehaviour,IScoringPartner,IPreWarmingObject, ISportCreatureCompetitor, IEndMatchAffiliable
    {
        public int colorIndex { get; set; }
        public SportCreature athlete { get; set; }
        public ScoringObject scoreObject { get; set; }
        public CharacterAnimationManager animatorControl { get; set; }
        private float delayShot;
        public PoolScoreBooster poolScore { get; set; }
        [SerializeField] private Spawner playerSpawn;
        private float pointsRest;

        void Start()
        {
            poolScore = GetComponent<PoolScoreBooster>();
            if (GetComponent<ScoringObject>())
            {
                scoreObject = GetComponent<ScoringObject>();
            }

            if (GameController.Instance != null)
            {
                GameController.Instance.OnPreGameCountEnded += ActiveWarmingBehaviour;
                GameController.Instance.OnMatchEnded += EndMatchDelegate;
            }

            ConfigureCreature();
        }

        public void CalculateFeatures()
        {
            delayShot = 1 - ((float)(athlete.accuracy + athlete.reflexes) / 20) + 0.15f;
            pointsRest = 30 * athlete.accuracy;
        }

        private IEnumerator SimulShot()
        {
            while (true)
            {
                yield return new WaitForSeconds(delayShot);
                AddScore();
            }
        }

        public void AddScore()
        {
            if (scoreObject != null)
            {
                float posibility = Random.Range(-(int)pointsRest/athlete.accuracy, pointsRest);
                if (posibility > 0)
                {
                    scoreObject.IncreseScore(Random.Range(0, 5));
                    pointsRest--;
                }
            }
        }

        public void ActiveWarmingBehaviour()
        {
            StartCoroutine(SimulShot());
        }

        public void ConfigureCreature()
        {
            athlete = MeshCreatureConfigurator.Instance.GetRandomCreature();
            CalculateFeatures();
        }

        public void EndMatchDelegate()
        {
            StopAllCoroutines();
        }
    }
}

