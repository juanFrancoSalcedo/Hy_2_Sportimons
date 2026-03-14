using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InGame.LaserRay
{
    public class LaserTargetShooting : MonoBehaviour
    {
        private float warmAmount = 100;
        public Vector3 upperLimits;
        public Vector3 lowerLimits;
        public Animator visualSphere;
        public Renderer visualRender;
        public event System.Action<GameObject> OnDisabled;
        public ScoringObject scoreObject { get; set; }
        private bool stopedWarm;
        private float timingNotWarm;
        [SerializeField] private ParticlesSettings partiDeath;
        [SerializeField] private DragTutorial tutorialDrag;

        public void WarmTarget(float _warmAmount)
        {
            warmAmount -= _warmAmount;
            Color colVivo = visualRender.material.GetColor("_Color");
            stopedWarm = false;

            if (warmAmount <= 0)
            {
                ActiveParticle();
                colVivo.g = 1;
                colVivo.b = 1;
                OnDisabled?.Invoke(gameObject);
                visualSphere.SetBool("Warming", false);
                if (tutorialDrag != null) {tutorialDrag.PlusMission();}
                gameObject.SetActive(false);
            }
            else
            {
                visualSphere.SetBool("Warming", true);
                colVivo.g = (float)warmAmount / 100;
                colVivo.b = (float)warmAmount / 100;
            }

            visualRender.material.SetColor("_Color",colVivo);
            timingNotWarm = 0;
        }

        void Update()
        {
            timingNotWarm += Time.deltaTime;

            if (timingNotWarm > 0.3f && !stopedWarm)
            {
                visualSphere.SetBool("Warming", false);
                stopedWarm = true;
            }
        }

        private void OnEnable()
        {
            warmAmount = 100;
            transform.rotation = new Quaternion(Random.Range(-1,1), Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
            if (GetComponent<ScoringObject>()) {scoreObject = GetComponent<ScoringObject>();}
            visualRender.material.SetColor("_Color", Color.white);
            visualSphere.SetBool("Warming", false);
        }

        private void ActiveParticle()
        {
            partiDeath.transform.SetParent(null);
            partiDeath.transform.position = transform.position;
            partiDeath.StartParticles();
        }
    }

}


