using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Meteors
{
    [RequireComponent(typeof(Rigidbody))]

    public class Meteor : MonoBehaviour, IScoringPartner, IEndMatchAffiliable
    {
        private new Rigidbody rigidbody;
        private int collisionCount =0;
        public int limitCollision=30;
        public TriggerDetector triggerDetector;
        private Vector3 velocity;
        public PoolScoreBooster poolScore { get; set; }
        [SerializeField]private GameObject[] partsMeteor;
        public ScoringObject scoreObject { get; set; }
        [SerializeField] private PoolObjects poolObj;
        [SerializeField] DragTutorial dragTuto;

        private void Start()
        {
            if (GameObject.Find("SCORE"))
            {
                scoreObject = GameObject.Find("SCORE").GetComponent<ScoringObject>();
            }
            
            poolScore = GetComponent<PoolScoreBooster>();
            rigidbody = GetComponent<Rigidbody>();
            triggerDetector.OnTriggerEntered += AddCollision;
            InitForce();
            
            for (int i = 0; i < partsMeteor.Length; i++)
            {
                partsMeteor[i].SetActive(true);
            }
        }

        private void OnEnable()
        {
            if (GameController.Instance != null)
            {
                GameController.Instance.OnMatchEnded += EndMatchDelegate;
            }
        }

        public void EndMatchDelegate()
        {
            GetComponent<Collider>().enabled = false;
            transform.position = Vector3.zero;
            rigidbody.isKinematic = true;
            rigidbody.linearVelocity = Vector3.zero;
        }

        private void InitForce()
        {
            float factor = Random.Range(-0.5f / transform.localScale.x, 0.5f / transform.localScale.x);
            velocity = new Vector3(factor, factor, 0);
            rigidbody.linearVelocity = velocity;
        }
        
        private void AddCollision(Transform other)
        {
            poolObj.PushParticles(other.position);
            collisionCount++;
            AddScore();
            other.gameObject.SetActive(false);
            rigidbody.AddForce(Vector3.up*0.6f,ForceMode.Impulse);

            if (collisionCount%5 ==0)
            {
                int bar = Random.Range(0,partsMeteor.Length);
                partsMeteor[bar].SetActive(false);
            }

            if (collisionCount > limitCollision)
            {
                Divide();
                gameObject.SetActive(false);
            }
        }

        private void Divide()
        {
            if (transform.localScale.x <= 0.5) { return; }

            GameObject divided1 =  Instantiate(gameObject,transform.position+Vector3.up/2,Quaternion.identity);
            GameObject divided2 = Instantiate(gameObject,transform.position+Vector3.right/2, Quaternion.identity);
        
            divided1.transform.localScale = divided1.transform.localScale * 0.7f;
            divided2.transform.localScale = divided2.transform.localScale * 0.7f;
            divided1.GetComponent<Meteor>().limitCollision = limitCollision - 6;
            divided2.GetComponent<Meteor>().limitCollision = limitCollision - 6;
            divided1.SetActive(true);
            divided2.SetActive(true);
        }

        public void AddScore()
        {
            if (scoreObject != null)
            {
                scoreObject.IncreseScore(1);
            }

            if (dragTuto != null)
            {
                dragTuto.PlusMission();
            }
        }
    }
}

