using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Pulse
{
    public class PulsePlayer : MonoBehaviour, IPreWarmingObject, IScoringPartner, ISportCreatureCompetitor, IKillable, IEndMatchAffiliable, IFeastable
    {
        public SportCreature athlete { get; set; }
        public bool Ia;
        public PulseController pulseControl;
        public ScoringObject scoreObject { get; set; }
        public PoolScoreBooster poolScore { get; set; }
        public int colorIndex { get; set; }
        public CharacterAnimationManager animatorControl { get; set; }
        public System.Action OnMyMatchEnded { get; set;}
        private int countCompetitors =3;
        private List<GameObject> listCompetitors= new List<GameObject>();

        void Start()
        {
            if (GetComponent<ScoringObject>())
            {
                scoreObject = GetComponent<ScoringObject>();
            }

            if (GameController.Instance != null)
            {
                GameController.Instance.OnPreGameCountEnded += ActiveWarmingBehaviour;
                GameController.Instance.OnMatchEnded += EndMatchDelegate;
            }
            else
            {
                ActiveWarmingBehaviour();
            }
            poolScore = GetComponent<PoolScoreBooster>();
            CalculateFeatures();
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public IEnumerator Push()
        {
            while (true)
            {
                CalculateForce(pulseControl.CalculatePlayersForce(athlete));
                float azar = Random.Range(0.6f, 1.2f);
                yield return new WaitForSeconds(azar);
            }
        }

        public void CalculateForce(float force)
        {
            animatorControl.SetAnimationCharacter("Run");
            if (Ia)
            {
                force = force * Random.Range(0.9f,1.1f);
                pulseControl.ShowPulse(0, force);
            }
            else
            {
                pulseControl.ShowPulse(force,0);
            }
        }

        public void CalculateFeatures()
        {
            ConfigureCreature();
            ConfigureCreature();
            ConfigureCreature();
            countCompetitors = 2;
        }

        public void ActiveWarmingBehaviour()
        {
            if (Ia)
            {
                StartCoroutine(Push());
            }
        }

        public int scoreSum { get; set;}

        public void AddScore()
        {
            if (scoreObject != null)
            {
                scoreObject.IncreseScore(scoreSum);
            }
        }

        public void Dead()
        {
            transform.GetChild(countCompetitors).GetComponent<CharacterAnimationManager>().SetAnimationCharacter("Dead");
            Invoke("DisableVisual",0.9f);
        }

        public void EnableVisual(bool enabled)
        {
            for (int i = 0; i < listCompetitors.Count; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            if (transform.childCount >0)
            transform.GetChild(countCompetitors).gameObject.SetActive(enabled);
        }

        public void DisableVisual()
        {
            for (int i = 0; i < listCompetitors.Count; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            transform.GetChild(countCompetitors).gameObject.SetActive(false);
            countCompetitors++;
            if (countCompetitors > 2)
            {
                countCompetitors = 0;
            }
        }

        public void ConfigureCreature()
        {
            countCompetitors--;

            if (Ia)
            {
                athlete = MeshCreatureConfigurator.Instance.GetRandomCreature();
            }
            else
            {
                athlete = MeshCreatureConfigurator.Instance.GetPlayerCreature(countCompetitors);
            }

            colorIndex = scoreObject.indexScoring;
            GameObject waitCreature = Instantiate(athlete.meshCreature, transform);
            waitCreature.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            if (Ia) { waitCreature.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 180)); }
            else { waitCreature.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0)); }
            animatorControl = waitCreature.GetComponent<CharacterAnimationManager>();
            animatorControl.textureControl.SetTextures(athlete, colorIndex);
            listCompetitors.Add(animatorControl.gameObject);
            if (GetComponent<Renderer>()) { GetComponent<Renderer>().enabled = false; }
        }

        public void EndMatchDelegate()
        {
            Celebrate();
            this.enabled = false;
        }

        public void Celebrate()
        {
            transform.GetChild(countCompetitors).GetComponent<CharacterAnimationManager>().SetAnimationCharacter("Celebration",true);
        }
    }

}

