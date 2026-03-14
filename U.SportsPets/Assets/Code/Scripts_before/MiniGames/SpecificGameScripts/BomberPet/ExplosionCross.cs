using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCross : MonoBehaviour
{

    public LayerMask limitLayer;
    [SerializeField] GameObject[] fronts;
    [SerializeField] GameObject[] backs;
    [SerializeField] GameObject[] left;
    [SerializeField] GameObject[] right;

    bool absurd;
    
    private void OnEnable()
    {
       
    }

    private void Update()
    {
        if (!absurd)
        {
            Ray rayR = new Ray(transform.position, Vector3.right);
            Ray rayL = new Ray(transform.position, Vector3.left);
            Ray rayF = new Ray(transform.position, Vector3.forward);
            Ray rayB = new Ray(transform.position, Vector3.back);

            RaycastHit hitR = new RaycastHit();
            RaycastHit hitL = new RaycastHit();
            RaycastHit hitF = new RaycastHit();
            RaycastHit hitB = new RaycastHit();

            if (Physics.Raycast(rayR, out hitR, 40f, limitLayer))
            {
                int iR= (int)Vector3.Distance(transform.position,hitR.point);
                iR = Mathf.Clamp(iR, 0, right.Length);
                //print(iR);
                for (int i = 0; i < iR; i++)
                {
                    right[i].SetActive(true);
                }
            }

            if (Physics.Raycast(rayL, out hitL, 40f, limitLayer))
            {
                int iL = (int)Vector3.Distance(transform.position, hitL.point);
                iL = Mathf.Clamp(iL,0,left.Length);
                //print(iL);
                for (int i = 0; i < iL; i++)
                {
                    left[i].SetActive(true);
                }
            }

            if (Physics.Raycast(rayF, out hitF, 40f, limitLayer))
            {
                int iF = (int)Vector3.Distance(transform.position, hitF.point);
                iF = Mathf.Clamp(iF, 0, fronts.Length);
                //print(iF);
                for (int i = 0; i < iF; i++)
                {
                    fronts[i].SetActive(true);
                }
            }

            if (Physics.Raycast(rayB, out hitB, 40f, limitLayer))
            {
                int iB = (int)Vector3.Distance(transform.position, hitB.point);
                iB = Mathf.Clamp(iB, 0, backs.Length);
                //print(iB);
                for (int i = 0; i < iB; i++)
                {
                    backs[i].SetActive(true);
                }
            }

            absurd = true;
        }
    }
    
}
