using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace InGame.Pulse
{
    public class PulseController : MonoBehaviour, IEndMatchAffiliable
    {
        public TouchManager touchManager;
        public PulsePlayer playerLocal;
        public PulsePlayer playerAway { get; set; }
        public PulsePairingController pairingMan;
        public PulsePlayer[] enemies;
        public LineRenderer line1;
        public LineRenderer line2;
        private Vector3 midPos;
        [SerializeField] ScoreBooster counterVisual;
        private int sumPoints;
        float diferencePlayersTime;
        private bool increasingWidth;
        private bool crashing = false;
        private bool firstTouch;
        private bool thereAreWinner;
        [SerializeField] bool isTuto;
        public Transform midleSpark;
        [SerializeField] private GameObject decoParticle1;
        [SerializeField] private GameObject decoParticle2;
        [SerializeField] private TapTutorial tapTuto;

        private void Start()
        {
            touchManager.OnTouchBegin += ApplyPulse;
            ExtractEnemySelected();
            if(GameController.Instance != null)
            {
                GameController.Instance.OnMatchEnded += EndMatchDelegate;
            }
        }

        private void Update()
        {
            if (increasingWidth)
            {
                crashing = false;
                line1.widthMultiplier = Mathf.Lerp(line1.widthMultiplier, 0.5f, 0.09f);
                line2.widthMultiplier = Mathf.Lerp(line2.widthMultiplier, 0.5f, 0.09f);
                midPos = (playerLocal.transform.position + playerAway.transform.position) / 2;
                line1.SetPosition(1, midPos);
                line2.SetPosition(1, midPos);
                if (line1.widthMultiplier > 0.49f)
                {
                    increasingWidth = false;
                    crashing = true;
                }
            }

            if (crashing)
            {
                decoParticle2.SetActive(true);
                decoParticle1.SetActive(true);
                midleSpark.gameObject.SetActive(true);
                midleSpark.position = midPos;
                line1.widthMultiplier = Random.Range(0.4f, 0.7f);
                line2.widthMultiplier = Random.Range(0.4f, 0.7f);
            }
            else
            {
                decoParticle2.SetActive(false);
                decoParticle1.SetActive(false);
                midleSpark.gameObject.SetActive(false);
            }

# if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerLocal.CalculateForce(0.7F);
            }
#endif
        }
        
        void ApplyPulse()
        {
            if (thereAreWinner) { return; }
            playerLocal.CalculateForce(CalculatePlayersForce(playerLocal.athlete));
        }

        public void ShowPulse(float force1, float force2)
        {
            if (thereAreWinner || increasingWidth) { return; }
            if (isTuto)
            {
                force2 = 0;
            }
            else
            {
                counterVisual.gameObject.SetActive(true);
                counterVisual.transform.localScale = Vector3.one * 0.5f;
            }

            if (!firstTouch)
            {
                increasingWidth = true;
                line1.positionCount = 2;
                line2.positionCount = 2;
                line1.SetPosition(0, playerLocal.transform.position);
                line2.SetPosition(0, playerAway.transform.position);
                firstTouch = true;
                
            }

            //midPos = (playerLocal.transform.position + playerAway.transform.position) / 2;
            float u = midPos.y + (force1 - force2);
            midPos = new Vector3(midPos.x, u, midPos.z);
            line1.SetPosition(1, midPos);
            line2.SetPosition(1, midPos);

            if (playerLocal.transform.position.y > midPos.y && !thereAreWinner)
            {
                if (!isTuto) { StartCoroutine(NextParticipant()); }
                playerAway.scoreSum = sumPoints;
                playerAway.AddScore();
                playerLocal.Dead();
                counterVisual.textMesh.color = Constants.teamColor[playerAway.colorIndex].colorTeam;
            }

            if (playerAway.transform.position.y < midPos.y && !thereAreWinner)
            {
                if (!isTuto)
                {
                    StartCoroutine(NextParticipant());
                    playerLocal.scoreSum = sumPoints;
                    playerLocal.AddScore();
                    counterVisual.textMesh.color = Constants.teamColor[playerLocal.colorIndex].colorTeam;
                }
                else
                {
                    tapTuto.PlusMission();
                    thereAreWinner = true;
                }
                playerAway.Dead();
            }
        }

        public float CalculatePlayersForce(SportCreature creat)
        {
            float pushShot = (float)(creat.force + creat.spirit+ creat.endurance) / 3;
            float eugeane = (pushShot) * (0.3f) + (float)((11 - pushShot) * 0.9f);
            float result = eugeane *Time.deltaTime;
            //float amiw = (5 + pushShot) * Time.deltaTime; //(0.1f/Time.deltaTime);
            float amiw = (result) *(0.06f/Time.deltaTime);
            //print(amiw);
            return amiw;
        }

        private IEnumerator NextParticipant()
        {
            thereAreWinner = true;
            playerAway.StopCoroutine(playerAway.Push());
            line1.widthMultiplier = 0;
            line2.widthMultiplier = 0;
            increasingWidth = false;
            crashing = false;
            counterVisual.SetText();
            yield return new WaitForSeconds(1.2f);
            increasingWidth = true;
            thereAreWinner = false;
            ExtractEnemySelected();
            firstTouch = false;
            playerAway.ActiveWarmingBehaviour();
            counterVisual.textMesh.color = new Color(255f/255f, 189f/255f, 1f/255f,1);
            counterVisual.transform.position = counterVisual.originalPos;
        }

        private void ExtractEnemySelected()
        {
            int rando = (!isTuto)? Random.Range(0, enemies.Length - 1):0;
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].EnableVisual(false);
                enemies[i].EnableVisual(false);
                enemies[i].EnableVisual(false);

                if (i != rando)
                {
                    enemies[i].enabled = false ;
                }
            }
            playerAway = enemies[rando];
            playerAway.enabled =  true;
            playerAway.EnableVisual(true);
            playerLocal.EnableVisual(true);
            playerLocal.animatorControl.SetAnimationCharacter("Run");
            if (!isTuto) { pairingMan.PairingPulsePlayersExeption(playerLocal, playerAway); }
            diferencePlayersTime = Mathf.Abs((playerLocal.athlete.endurance - playerAway.athlete.endurance) / 2)+0.6f;
            StartCoroutine(SumPoints());
        }

        IEnumerator SumPoints()
        {
            while (!thereAreWinner)
            {
                yield return new WaitForSeconds(diferencePlayersTime);
                if (counterVisual != null)
                {
                    counterVisual.textMesh.text = "" + sumPoints;
                    sumPoints++;
                    if (counterVisual.gameObject.activeInHierarchy)
                    {
                        counterVisual.Shake();
                    }
                }
            }
            sumPoints = 1;
        }

        public void EndMatchDelegate()
        {
            StopAllCoroutines();
            this.enabled = false;
        }
    }

}

