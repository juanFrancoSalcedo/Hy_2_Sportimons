using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InGame.SwitchPricks
{
    public class Switcher : MonoBehaviour
    {
        private float minRespwZ;
        private float maxRespwZ;
        public TranslateObject movement;
        public TriggerDetector buttonSwitch;
        private TriggerDetector prickDetector;
        public bool fromBottom;

        void Start()
        {
            prickDetector = GetComponent<TriggerDetector>();

            buttonSwitch.OnTriggerEntered += SwitchPrick;
            prickDetector.OnTriggerEntered += Prick;
        }

        private void SwitchPrick(Transform other)
        {
            movement.movementVector = -movement.movementVector;
            buttonSwitch.gameObject.SetActive(false);
            Invoke("RestoreMovement", Random.Range(1.8f,3.3f));
        }

        private void RestoreMovement()
        {
            buttonSwitch.gameObject.SetActive(true);
            movement.movementVector = movement.bufferMoveVector;
        }

        private void Prick(Transform other)
        {
            other.gameObject.SendMessage("Dead");
        }

    }

}


