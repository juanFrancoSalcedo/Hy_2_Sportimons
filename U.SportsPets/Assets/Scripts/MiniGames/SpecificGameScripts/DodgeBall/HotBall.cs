using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.DodgeBall
{
    public class HotBall : MonoBehaviour,IEndMatchAffiliable
    {
        public CollisionDetector collisionDetector;
        public LayerMask enemyLayer;
        public float force = 10;
        [SerializeField] GameObject gift;

        private void Start()
        {
            collisionDetector.OnCollisionEntered += Bounce;
            GetComponent<Rigidbody>().linearVelocity = new Vector3(Random.Range(-1.5f,1.5f), 0, Random.Range(-1.5f, 1.5f)) * force;
            if (GameController.Instance != null){ GameController.Instance.OnMatchEnded += EndMatchDelegate; }
        }

        private void Bounce(Collision other)
        {
            Vector3 dir = transform.position- other.contacts[0].point;
           
            GetComponent<Rigidbody>().linearVelocity = dir.normalized*force;

            if (LayerDetection.DetectContainedLayers(enemyLayer, other.gameObject))
            {
                other.gameObject.SendMessage("Dead");
                int randoDiv = Random.Range(-2,2);
                if (randoDiv > 0)
                {
                    Instantiate(gameObject, other.transform.position, Quaternion.identity);
                }
                else
                {
                    if (gift != null)
                    {
                        GameObject instanc =  Instantiate(gift, other.transform.position, Quaternion.identity);
                        instanc.gameObject.SetActive(true);
                    }
                }
            } 
        }

        public void EndMatchDelegate()
        {
            collisionDetector.enabled = false;
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
    }


}


