using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILaserRayManager : MonoBehaviour
{
    public Vector3 targetPosition;
    private GameObject[] targetsDestroyable;
    [SerializeField] private string tagSearch;

    void Start()
    {
        StartCoroutine(SearchAndAim());
    }

    IEnumerator SearchAndAim()
    {
        while (true)
        {
            targetsDestroyable = GameObject.FindGameObjectsWithTag(tagSearch);
            if(targetsDestroyable.Length > 0)
            {
                targetPosition = targetsDestroyable[Random.Range(0, targetsDestroyable.Length)].transform.position +
                new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0);
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
