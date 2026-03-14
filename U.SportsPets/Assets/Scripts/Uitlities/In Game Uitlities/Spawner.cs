using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int countPrefabs = 1;
        [Range(0.1f, 1)]
        public float delayBetwenSpawn = 0.1f;
        private bool shoted;
        private int indexCurrent;
        private GameObject[] instances;
        private GameObject parent;

        public event System.Action OnBulletsLacked;
        public event System.Action<int,GameObject> OnShoted;
        
        private void Start()
        {
            parent = new GameObject("ammo");
            instances = new GameObject[countPrefabs];

            if (countPrefabs != 0)
            {
                for (int i = 0; i < instances.Length; i++)
                {
                    instances[i] = Instantiate(prefab);
                    instances[i].transform.SetParent(parent.transform);
                    instances[i].SetActive(false);
                }
            }
            prefab.SetActive(false);
        }

        public void Shot()
        {
            shoted = true;
            StartCoroutine(SpawnPrefab());
        }

        public void EndShot()
        {
            shoted = false;
            StopCoroutine(SpawnPrefab());
        }
        
        private IEnumerator SpawnPrefab()
        {
            int i = indexCurrent;
            while (i < countPrefabs && shoted)
            {
                instances[i].SetActive(true);
                instances[i].transform.position = transform.position + new Vector3(Random.Range(-0.3f, 0.3f), 0, 0);
                i++;
                indexCurrent = i;
                OnShoted?.Invoke(countPrefabs - indexCurrent,instances[i-1]);

                yield return new WaitForSeconds(delayBetwenSpawn);
            }

            if (indexCurrent>= countPrefabs)
            {
                OnBulletsLacked?.Invoke();
            }
        }

        public void Reload()
        {
            indexCurrent = 0;
        }
    }
}

