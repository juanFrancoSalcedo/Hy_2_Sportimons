using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.SwitchPricks
{
    public class AISwitchPrick : MonoBehaviour, ISportCreatureCompetitor, IPreWarmingObject,IKillable, IPerishable, IFeastable
    {
        //public TriggerDetector triggerDete;
        [SerializeField] private AISwitchPrickManager manager;
        public SportCreature athlete { get; set; }
        public int colorIndex { get; set; }
        private float forceAI;
        private float delayAI;
        private Rigidbody rb;
        public LayerMask layerCorrect;
        public LayerMask layerIncorrect;
        public CharacterAnimationManager animatorControl { get; set; }
        private bool canIFoundDanger = true;
        public System.Action OnMyGameEnded { get; set; }

        void Start()
        {
            ConfigureCreature();
            rb = GetComponent<Rigidbody>();
            CalculateFeatures();
            manager.OnDirectionCalulated += MoveBehaviour;
            manager.AddCompetitor(this);
            GameController.Instance.OnPreGameCountEnded += ActiveWarmingBehaviour;
            GameController.Instance.OnMatchEnded += Celebrate;
        }

        public void CalculateFeatures()
        {
            float much = (float)Mathf.Abs(athlete.force-10)/10;
            forceAI = (float)(Mathf.Log(athlete.force+much*2.5f) / (60+ athlete.force*2.5f))-0.006f;
            delayAI = (float)(21 - (athlete.reflexes + athlete.speed)) / 50;
        }

        public void ActiveWarmingBehaviour()
        {
            canIFoundDanger = false;
        }

        void MoveBehaviour( Vector3 direction)
        {
            if (canIFoundDanger) { return; }
            rb.AddForce(direction * forceAI, ForceMode.Impulse);
            SendDashAnimation(direction);
            Invoke("RestoreAnimationDash",1);
        }

        void RestoreAnimationDash()
        {
            animatorControl.SetAnimationCharacter("Dash", false);
        }

        private void Update()
        {
            if(canIFoundDanger) { return; }

            Ray rayfrontCenter = new Ray(transform.position + transform.forward/2, transform.TransformDirection(Vector3.forward));
            Ray rayfrontLeft = new Ray(transform.position + transform.forward/2-transform.right/2, transform.TransformDirection(Vector3.left + Vector3.forward));
            Ray rayfrontRight = new Ray(transform.position + transform.forward/2+transform.right/2, transform.TransformDirection(Vector3.right + Vector3.forward));
            
            Ray rayBackCenter = new Ray(transform.position - transform.forward / 2, transform.TransformDirection(Vector3.back));
            Ray rayBackLeft = new Ray(transform.position - transform.forward / 2 - transform.right / 2, transform.TransformDirection(Vector3.left + Vector3.back));
            Ray rayBackRight = new Ray(transform.position - transform.forward / 2 + transform.right / 2, transform.TransformDirection(Vector3.right + Vector3.back));
            
            SearchDangerComing(rayfrontCenter);
            SearchDangerComing(rayfrontLeft);
            SearchDangerComing(rayfrontRight);
            SearchDangerComing(rayBackCenter);
            SearchDangerComing(rayBackLeft);
            SearchDangerComing(rayBackRight);
        }

        private void SearchDangerComing(Ray ray)
        {
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 3.5f,layerIncorrect))
            {
                if (hit.collider != null)
                {
                    manager.MoveAll(transform.position, hit.point);
                }
            }

            if (Physics.Raycast(ray, out hit, 3.5f, layerCorrect))
            {
                if (hit.collider != null)
                {
                    manager.MoveAll(hit.point.normalized*3f,transform.position.normalized*3f);
                }
            }
            Debug.DrawRay(ray.origin,ray.direction,Color.cyan);
        }

        public void Dead()
        {
            OnMyGameEnded?.Invoke();
            animatorControl.SetAnimationCharacter("Dead", true);
            animatorControl.animatorCharacter.GetComponent<DisableByTime>().BeginDisableTime();
            rb.linearVelocity = Vector3.zero;
            manager.OnDirectionCalulated -= MoveBehaviour;
            GetComponent<ScoringObject>().enabled = false;
            GetComponent<ScoringParticipantOnStage>().enabled = false;
            GetComponent<ScoringParticipantOnTrigger>().enabled = false;
            GetComponent<TriggerDetector>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }

        void SendDashAnimation(Vector3 visionDir)
        {
            animatorControl.SetAnimationCharacter("Dash", true);
            Vector3 direction = visionDir - animatorControl.animatorCharacter.transform.position;
            float angle = (Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
            animatorControl.animatorCharacter.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }

        public void ConfigureCreature()
        {
            athlete = MeshCreatureConfigurator.Instance.GetRandomCreature();
            colorIndex = 0;
            GameObject waitCreature = Instantiate(athlete.meshCreature, transform);
            waitCreature.transform.localPosition = new Vector3(0, -0.5f, 0);
            waitCreature.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            animatorControl = waitCreature.GetComponent<CharacterAnimationManager>();
            animatorControl.textureControl.SetTextures(athlete,GetComponent<ScoringObject>().indexScoring);
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
