using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class SwipeCompetitor : MonoBehaviour, ISportCreatureCompetitor, IPreWarmingObject, IKillable, IPerishable, IFeastable
    {
        [SerializeField] private SwipeableObject swipeMovement;
        public SportCreature athlete { get; set; }
        public CharacterAnimationManager animatorControl { get; set; }
        public int colorIndex { get; set; }
        public System.Action OnMyGameEnded { get; set; }
        [Range(0, 2)]
        [SerializeField] int seat = 0;

        void Start()
        {
            ConfigureCreature();
            swipeMovement.OnSwipedObject += SendDashAnimation;

            if (GameController.Instance != null)
            {
                GameController.Instance.OnPreGameCountEnded += ActiveWarmingBehaviour;
                GameController.Instance.OnMatchEnded += Celebrate;
            }
            else
            {
                swipeMovement.enabled = true;
                CalculateFeatures();
            }
        }

        public void ActiveWarmingBehaviour()
        {
            swipeMovement.enabled = true;
            CalculateFeatures();
        }

        public void CalculateFeatures()
        {
            float reflexes = (float)(21 - (athlete.reflexes + athlete.speed)) / 90;
            float much = (float)Mathf.Abs(athlete.force - 10) / 10;
            float force = (float)(Mathf.Log(athlete.force + much * 2.5f) / (60 + athlete.force * 2.5f)) - 0.006f;
            swipeMovement.forceMovement = force;
            swipeMovement.delayTime = reflexes;
        }

        void SendDashAnimation(Vector3 visionDir)
        {
            animatorControl.SetAnimationCharacter("Dash", true);
            Vector3 reformedDir = new Vector3(visionDir.x, 0, visionDir.y);
            Quaternion quaternion = Quaternion.LookRotation(reformedDir, Vector3.up);
            animatorControl.transform.rotation = quaternion;
            Invoke("EndDash", 1);
        }

        void EndDash()
        {
            animatorControl.SetAnimationCharacter("Dash", false);
        }

        public void Dead()
        {
            Camera.main.SendMessage("Shake");
            animatorControl.SetAnimationCharacter("Dead", true);
            animatorControl.animatorCharacter.GetComponent<DisableByTime>().BeginDisableTime();
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            swipeMovement.enabled = false;
            GetComponent<ScoringObject>().enabled = false;
            GetComponent<ScoringParticipantOnStage>().enabled = false;
            if (GetComponent<ScoringParticipantOnTrigger>()) { GetComponent<ScoringParticipantOnTrigger>().enabled = false; }
            if (GetComponent<TriggerDetector>()) { GetComponent<TriggerDetector>().enabled = false; }
            GetComponent<Collider>().enabled = false;
            OnMyGameEnded?.Invoke();
        }

        public void ConfigureCreature()
        {
            athlete = MeshCreatureConfigurator.Instance.GetPlayerCreature(seat);
            colorIndex = 0;
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