using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class TriggerMessenger : MonoBehaviour
    { 
        private TriggerDetector detectorTri;
        [SerializeField] private string[] messages;

        void Start()
        {
            detectorTri = GetComponent<TriggerDetector>();
            detectorTri.OnTriggerEntered += SendMessageOnTrigger;
        }
        
        void SendMessageOnTrigger(Transform other)
        {
            foreach (string fic in messages)
            {
                other.SendMessage(fic, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
