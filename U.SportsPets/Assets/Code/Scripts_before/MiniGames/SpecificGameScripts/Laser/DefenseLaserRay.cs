using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.LaserRay
{
    public class DefenseLaserRay : MonoBehaviour, ISportCreatureCompetitor
    {
        public Vector3 targetPosition { get; private set; }
        private GameObject[] targetsDestroyable;
        [SerializeField] private ScoringObject scor;
        public SportCreature athlete { get; set; }
        public int colorIndex { get; set; }
        public CharacterAnimationManager animatorControl { get; set; }
        private float reflexesDef;
        private float velocityDef;
        [SerializeField] private string tagSearch;
        [SerializeField] bool Ai = true;

        void Start()
        {
            ConfigureCreature();
            StartCoroutine(SearchAndAim());
        }

        private void Update()
        {
             transform.position = Vector3.Lerp(transform.position,targetPosition,0.006f);
        }

        IEnumerator SearchAndAim()
        {
            while (true)
            {
                targetsDestroyable = GameObject.FindGameObjectsWithTag(tagSearch);
                if (targetsDestroyable.Length > 0)
                { 
                    targetPosition = targetsDestroyable[Random.Range(0, targetsDestroyable.Length)].transform.position + 
                        new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
                }
                Vector3 direction = targetPosition - transform.position;
                float angle = (Mathf.Atan2(direction.x,direction.y) * Mathf.Rad2Deg);
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                animatorControl.textureControl.CoolTexture();
                yield return new WaitForSeconds(reflexesDef);
            }
        }

        public void CalculateFeatures()
        {
            reflexesDef =Mathf.Abs(athlete.reflexes - 12) /2;
            //print(reflexesDef);
        }

        public void ConfigureCreature()
        {
            if (Ai) { athlete = MeshCreatureConfigurator.Instance.GetRandomCreature(); }
            else { athlete = MeshCreatureConfigurator.Instance.GetPlayerCreature(2); }
            GameObject ter = Instantiate(athlete.meshCreature, transform);
            animatorControl = ter.GetComponent<CharacterAnimationManager>();
            animatorControl.textureControl.SetTextures(athlete, scor.indexScoring);
            ter.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            ter.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            if (GetComponent<Renderer>())
            {
                GetComponent<Renderer>().enabled = false;
            }
            CalculateFeatures();
        }
    }
}




