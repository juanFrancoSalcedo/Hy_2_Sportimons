using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.LaserRay
{
   [RequireComponent(typeof(LineRenderer))]
   [RequireComponent(typeof(ScoringObject))]
    public class LaserRay : MonoBehaviour,IPreWarmingObject,ISportCreatureCompetitor, IScoringPartner, IEndMatchAffiliable, IFeastable
    {
        public ScoringObject scoreObject { get; set; }
        private LineRenderer visualLaser;
        public LayerMask warmingLayer;
        public LayerMask specifLayers;
        public int colorIndex { get; set; }
        public SportCreature athlete { get; set; }
        public CharacterAnimationManager animatorControl { get; set; }
        private Ray ray;
        private RaycastHit hit;
        [SerializeField] private Transform laserCanonDirection;
        private bool allowShot;
        private float powerRay;
        private float gap;
        private float pointsWarm;
        [SerializeField] private ParticlesSettings particles;
        [SerializeField] private bool forTuto = false;
        public PoolScoreBooster poolScore { get; set; }
        private Vector3 lastHitPoint;
        [SerializeField] int seat= 1090;

        private void Start()
        {
            poolScore = GetComponent<PoolScoreBooster>();
            scoreObject = GetComponent<ScoringObject>();
            visualLaser = GetComponent<LineRenderer>();
            visualLaser.positionCount = 2;
            ConfigureCreature();
            if(GameController.Instance != null)
            {
                GameController.Instance.OnPreGameCountEnded += ActiveWarmingBehaviour;
                GameController.Instance.OnMatchEnded += EndMatchDelegate;
            }
            else
            {
                ActiveWarmingBehaviour();
            }
            CalculateFeatures();
        }

        private void Update()
        {
            if (!allowShot) { return; }

            ray = new Ray(transform.position, transform.up+ transform.right*((gap-1.5f)*0.06f)*Mathf.Sin(Random.Range(-1f,1f)));

            if (Physics.Raycast(ray, out hit, 80f, specifLayers))
            {
                visualLaser.SetPosition(0, transform.position);
                visualLaser.SetPosition(1, hit.point);
                particles.transform.position = hit.point;
                if (LayerDetection.DetectContainedLayers(warmingLayer, hit.collider.gameObject))
                {
                    lastHitPoint = hit.point;
                    hit.collider.SendMessage("WarmTarget", powerRay);
                    GainWarmPoints();
                }
            }
        }

        private void GainWarmPoints()
        {
            pointsWarm +=(powerRay*0.09f);
            if (pointsWarm >= 1)
            {
                AddScore();
                pointsWarm = 0;
            }
        }

        public void AddScore()
        {
            if (!forTuto)
            {
                scoreObject.IncreseScore(1);
                if(poolScore != null)
                {
                    poolScore.PushBooster(lastHitPoint,1);
                }
            }
        }

        public void ActiveWarmingBehaviour()
        {
            allowShot = true;
        }

        public void CalculateFeatures()
        {
            float a = (float)athlete.accuracy / 5f;
            float b = (float)(5 - athlete.accuracy) / 10f;
            gap = (float)(a + b);
            float c = (float)(athlete.spirit / 10f);
            float d = (float)((athlete.spirit / 10f) * 0.5f);
            float e = (float)((5 - athlete.spirit) / 30f);
            powerRay = c - d + e;
        }

        public void ConfigureCreature()
        {
            if (seat > 4) { athlete = MeshCreatureConfigurator.Instance.GetRandomCreature(); }
            else { athlete = MeshCreatureConfigurator.Instance.GetPlayerCreature(seat); }
            colorIndex = scoreObject.indexScoring;
            GameObject waitCreature = Instantiate(athlete.meshCreature, transform.parent);
            waitCreature.transform.localPosition = new Vector3(0, -0.5f, 0);
            waitCreature.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            waitCreature.transform.rotation = laserCanonDirection.rotation;
            animatorControl = waitCreature.GetComponent<CharacterAnimationManager>();
            animatorControl.textureControl.SetTextures(athlete, colorIndex);
            visualLaser.material = animatorControl.textureControl.powMaterial;
            visualLaser.material.SetColor("_EmissionColor",Constants.teamColor[colorIndex].colorTeam);
            particles.SetColorMainParticles(Constants.teamColor[colorIndex].colorTeam);
            particles.transform.position = Vector3.one * 5000;
            if (transform.parent.GetComponent<Renderer>())
            {
                transform.parent.GetComponent<Renderer>().enabled = false;
            }
        }

        public void EndMatchDelegate()
        {
            allowShot = false;
            visualLaser.enabled = false;
            particles.StopParticles();
            laserCanonDirection.gameObject.SetActive(false);
            Celebrate();
        }

        public void Celebrate()
        {
            animatorControl.transform.rotation = Quaternion.Euler(0,180,0);
            animatorControl.SetAnimationCharacter("Celebration",true);
        }
    }
}



