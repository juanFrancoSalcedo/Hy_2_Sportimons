using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.LaserRay
{
    public class AILaserRay : MonoBehaviour, IEndMatchAffiliable
    {
        public CharacterAnimationManager animatorControl { get; set; }
        public AILaserRayManager manager;
        [SerializeField] bool useXAxis;
        private bool stop;

        void Start()
        {
            GameController.Instance.OnMatchEnded += EndMatchDelegate;
        }
        
        private void Update()
        {
            if (stop) { return; }
            Vector3 futurePos = manager.targetPosition;

            if (useXAxis)
            { 
                futurePos.y = transform.position.y;
                transform.position =  Vector3.Lerp(transform.position, futurePos,0.3f);
            }
            else
            {
                futurePos.x = transform.position.x;
                transform.position = Vector3.Lerp(transform.position, futurePos,0.3f);
            }

        }

        public void EndMatchDelegate()
        {
            stop = true;
        }

    }

}

