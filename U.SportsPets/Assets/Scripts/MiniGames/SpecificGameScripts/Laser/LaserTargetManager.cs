using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.LaserRay
{
    public class LaserTargetManager : MonoBehaviour
    {
        [SerializeField] LaserTargetShooting prefab;
        private List<LaserTargetShooting> prefabsObjects = new List<LaserTargetShooting>();

        private void Start()
        {
            prefabsObjects.Add(prefab);
            foreach (LaserTargetShooting obj in prefabsObjects)
            {
                obj.OnDisabled += ActivePrefabs;
            }
        }

        private void InstanciatePrefabs()
        {
            Vector3 posNew = new Vector3(Random.Range(prefab.lowerLimits.x, prefab.upperLimits.x), Random.Range(prefab.lowerLimits.y, prefab.upperLimits.y),0);
            LaserTargetShooting sho = Instantiate(prefab,posNew,Quaternion.identity);
            sho.transform.position = posNew;
            prefabsObjects.Add(sho);
        }

        private GameObject currentObj;

        private void ActivePrefabs(GameObject obj)
        {
            currentObj = obj;
            Invoke("CouldActivePrefab",0.1f);
        }

        private void CouldActivePrefab()
        {
            Vector3 posNew = new Vector3(Random.Range(prefab.lowerLimits.x, prefab.upperLimits.x), Random.Range(prefab.lowerLimits.y, prefab.upperLimits.y), 0);
            currentObj.transform.position = posNew;
            currentObj.SetActive(true);
            Invoke("InstanciatePrefabs", 5);
        }
    }



}

