using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.DodgeBall
{
    public class AIDodgeBall : MonoBehaviour, ISportCreatureCompetitor ,IKillable, IPerishable,IFeastable
    {
        [SerializeField] private AIDodgeBallManager manager;
        private float forceAI;
        private float delayAI;
        private Rigidbody rigiB;
        private GameObject[] ballsEnemies;
        private bool canDodge = true;
        public int colorIndex { get; set; }
        public SportCreature athlete { get; set; }
        public CharacterAnimationManager animatorControl { get; set; }
        public System.Action OnMyGameEnded { get; set; }
        private CollisionDetector detectorCol;
        private Transform lastCollider;

        void Start()
        {
            detectorCol = GetComponent<CollisionDetector>();
            rigiB = GetComponent<Rigidbody>();
            ConfigureCreature();
            CalculateFeatures();
            manager.OnParticipantThreatened += MoveBehaviour;
            //detectorCol.OnCollisionEntered += StockCollision;
            StartCoroutine(FoolBehaviour());
            StartCoroutine(RefreshListOfBalls());
            GameController.Instance.OnMatchEnded += Celebrate;
        }

        public void CalculateFeatures()
        {
            float much = (float)Mathf.Abs(athlete.force - 10) / 10;
            forceAI = (float)(Mathf.Log(athlete.force + much * 2.5f) / (60 + athlete.force * 2.5f)) + 6f;
            forceAI = 1+ (0.3f* athlete.force);
            delayAI = (float)(21 - (athlete.reflexes + athlete.speed)) / 50;
        }

        void Update()
        {
            foreach (GameObject badBall in ballsEnemies)
            {
                if (Vector3.Distance(badBall.transform.position, transform.position) < 3 && canDodge)
                {
                    manager.ReceiveDangerComing(transform.position, badBall.transform.position);
                    canDodge = false;
                    Invoke("RestoreReflexes", 0.6f);
                }
            }
        }

        void MoveBehaviour(Vector3 direction)
        {
            rigiB.AddForce(direction * forceAI, ForceMode.Impulse);
            SendDashAnimation(direction);
            Invoke("RestoreAnimationDash", 2);
        }

        void RestoreAnimationDash()
        {
            animatorControl.SetAnimationCharacter("Dash", false);
        }

        public void Dead()
        {
            animatorControl.SetAnimationCharacter("Dead", true);
            animatorControl.animatorCharacter.GetComponent<DisableByTime>().BeginDisableTime();
            rigiB.linearVelocity = Vector3.zero;
            GetComponent<ScoringObject>().enabled = false;
            GetComponent<ScoringParticipantOnStage>().enabled = false;
            if (GetComponent<TriggerDetector>()) { GetComponent<TriggerDetector>().enabled = false; }
            GetComponent<Collider>().enabled = false;
            manager.OnParticipantThreatened -= MoveBehaviour;
            OnMyGameEnded?.Invoke();
            //detectorCol.OnCollisionEntered -= StockCollision;
        }

        void SendDashAnimation(Vector3 visionDir)
        {
            Quaternion quaternion = Quaternion.LookRotation(visionDir,Vector3.up);
            animatorControl.transform.rotation = quaternion;
            animatorControl.SetAnimationCharacter("Dash", true);
        }

        void RestoreReflexes()
        {
            canDodge = true;
        }
        private IEnumerator RefreshListOfBalls()
        {
            while (true)
            {
                ballsEnemies = GameObject.FindGameObjectsWithTag("Obstacle");
                yield return new WaitForSeconds(0.6f);
            }
        }
        
        private IEnumerator FoolBehaviour()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(6f,21f));

                int rando = Random.Range(0,5);
                switch (rando)
                {
                    case 1:
                        manager.ReceiveDangerComing(transform.position, (transform.position + transform.forward));
                        break;

                    case 2:
                        manager.ReceiveDangerComing(transform.position, (transform.position - transform.forward));
                        break;
                    case 3:
                        manager.ReceiveDangerComing(transform.position,(transform.position + transform.right));
                        break;
                    case 4:
                        manager.ReceiveDangerComing(transform.position,(transform.position - transform.right));
                        break;
                }
            }
        }

        public void ConfigureCreature()
        {
            athlete = MeshCreatureConfigurator.Instance.GetRandomCreature();
            colorIndex = GetComponent<ScoringObject>().indexScoring;
            GameObject waitCreature = Instantiate(athlete.meshCreature, transform);
            waitCreature.transform.localPosition = new Vector3(0, -0.5f, 0);
            waitCreature.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            animatorControl = waitCreature.GetComponent<CharacterAnimationManager>();
            animatorControl.textureControl.SetTextures(athlete, colorIndex);
            if (GetComponent<Renderer>())
            {
                GetComponent<Renderer>().enabled = false;
            }
        }

        public void Celebrate()
        {
            animatorControl.SetAnimationCharacter("Celebration", true);
        }
    }


}


