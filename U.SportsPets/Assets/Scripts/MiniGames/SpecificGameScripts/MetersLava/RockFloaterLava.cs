using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace InGame.MetersLava
{
    public class RockFloaterLava : MonoBehaviour
    {

         private Vector3 startPosi;
         private Vector3 targetLocalPosition;

        void Start()
        {
            startPosi = transform.position;
            startPosi.y -= 3;
            transform.position = startPosi;
        }


        public void Animate()
        {
            transform.DOMoveY(transform.position.y+3f, 0.6f,false).SetEase(Ease.OutBack);

        }
        

    }
}


