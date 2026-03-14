using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace InGame.MetersLava
{
    public class LavaCompetitor : MonoBehaviour, ISportCreatureCompetitor, IRespawneableCompetitor,IKillable
    {
        [SerializeField] private TriggerDetector triggerDetecto;
        public int colorIndex { get; set; }
        public SportCreature athlete { get; set; }
        public CharacterAnimationManager animatorControl { get; set; }
        public TranslateObject enviromentTranslator;
        public bool invulnerable { get; private set; }
        private bool tocando;
        private bool coroutineSended;
        public bool respawnByMistake { get; set; }
        private int bufferLayer;
        [SerializeField] private Transform behindTransfom;
        [SerializeField] private int lifes = 2;
        private Vector3 lastTouched;
        [SerializeField] private TextMeshProUGUI txt;
        private GameObject lastCreature;

        private void Start()
        {
            bufferLayer = gameObject.layer;
            triggerDetecto.OnTriggerStayed += Touching;
            triggerDetecto.OnTriggerExited += TouchingExit;
        }

        private void Update()
        {
            if (!tocando)
            {
                respawnByMistake = true;
                enviromentTranslator.Stop(true);
                Dead();
                gameObject.SetActive(false);
                if (!coroutineSended)
                {
                    coroutineSended = true;
                }
            }
            animatorControl.animatorCharacter.speed = (float)Vector3.Distance(transform.position, behindTransfom.position) / 8;
        }

        public void CalculateFeatures()
        {
            Vector3 auxTranslator = enviromentTranslator.movementVector;
            float F = Mathf.Abs(athlete.speed -11);
            float S =(float) Mathf.Log(F)/(26+ athlete.speed*2);
            float C = -(float) ((athlete.speed * Time.deltaTime)*0.8f + S);
            if (C < -0.2f)
            {
                C= -0.25f;
            }
            auxTranslator.z = C;
            //print(C);
            if (txt != null)
            {
                txt.text = "" + C;
            }
            enviromentTranslator.movementVector = auxTranslator;
        }

        private IEnumerator RestartRun()
        {
            lifes--;
            if (lifes < 0)
            {
                GameController.Instance.Invoke("EndMatch", 2f);
                GameObject.FindObjectOfType<RaceController>().Invoke("LoseAddTime", 2f);
                yield break;
            }
            else
            {
                ConfigureCreature();
            }
            yield return new WaitForSeconds(0.9f);
            gameObject.layer = bufferLayer;
            coroutineSended = false;
            enviromentTranslator.Stop(false);
        }

        private void Touching(Transform other)
        {
            tocando = true;
            Vector3 floorPosition = other.position;
            floorPosition.y = transform.position.y;
            lastTouched = transform.position + (floorPosition - transform.position).normalized*1.4f;
        }

        private void TouchingExit(Transform other)
        {
            tocando = false;
        }

        private void OnEnable()
        {
            transform.position = lastTouched;
            tocando = true;
            if (animatorControl != null)
            {
                if (lifes < 3) { StartCoroutine(Twinkle()); }
                animatorControl.animatorCharacter.SetBool("Run", true);
            }
            StartCoroutine(RestartRun());
        }

        private void OnDisable()
        {
            gameObject.layer = Constants.LayerIgnoreCollisions;
            enviromentTranslator.Stop(true);
            if (respawnByMistake)
            {
                GameController.Instance.StartCoroutine(GameController.Instance.ResurrectObject(0.6f, gameObject));
                respawnByMistake = false;
            }
        }
        
        public void SetRespawnPosition(Vector3 posi)
        {
            respawnByMistake = true;
            lastTouched = posi;
            lastTouched.y = transform.position.y;
        }

        private IEnumerator Twinkle()
        {
            bool blinking = false;
            int countTwinkles =0;
            while (countTwinkles < 8)
            {
                animatorControl.textureControl.renderObjectMat.enabled = blinking;
                blinking = !blinking;
                countTwinkles++;
                yield return new WaitForSeconds(0.1f);
            }
            animatorControl.textureControl.renderObjectMat.enabled = true;
        }

        public void ConfigureCreature()
        {
            if(lastCreature  != null){ Destroy(lastCreature); }
            athlete = MeshCreatureConfigurator.Instance.GetPlayerCreature(lifes);
            colorIndex = 0;
            GameObject waitCreature = Instantiate(athlete.meshCreature, transform);
            waitCreature.transform.localPosition = new Vector3(0, -0.5f, 0);
            waitCreature.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            animatorControl = waitCreature.GetComponent<CharacterAnimationManager>();
            animatorControl.textureControl.SetTextures(athlete, colorIndex);
            animatorControl.animatorCharacter.SetBool("Run",true);
            lastCreature = animatorControl.gameObject;
            CalculateFeatures();
            if (GetComponent<Renderer>()) { GetComponent<Renderer>().enabled = false; }
        }

        public void Dead()
        {
            Camera.main.SendMessage("Shake");
        }
    }
    
}

