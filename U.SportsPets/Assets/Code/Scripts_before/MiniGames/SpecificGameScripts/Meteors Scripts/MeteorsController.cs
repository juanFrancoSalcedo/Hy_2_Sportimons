using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGame.Meteors;



namespace InGame.Meteors
{
    public class MeteorsController : MonoBehaviour, IPreWarmingObject
    {

        [SerializeField] private PortalInstanciator [] meterosPortal;

        void Start()
        {
            GameController.Instance.OnPreGameCountEnded += ActiveWarmingBehaviour;
        }

        // Update is called once per frame
        public void ActiveWarmingBehaviour()
        {
            foreach (PortalInstanciator mete in meterosPortal)
            {
                mete.AppearPrefab();
            }

        }
    }

}



