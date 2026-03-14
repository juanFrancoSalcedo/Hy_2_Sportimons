using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjects : MonoBehaviour
{
    [SerializeField] private GameObject origi;
    private List<GameObject> particles = new List<GameObject>();

    public void PushParticles(Vector3 positionParti)
    {
        foreach (GameObject part in particles)
        {
            if (!part.gameObject.activeInHierarchy)
            {
                part.SetActive(true);
                part.transform.position = positionParti;
                return;
            }
        }

        GameObject sBoost = Instantiate(origi);
        sBoost.name = "parti" + name;
        particles.Add(sBoost);
        sBoost.transform.position = positionParti;
    }
}
