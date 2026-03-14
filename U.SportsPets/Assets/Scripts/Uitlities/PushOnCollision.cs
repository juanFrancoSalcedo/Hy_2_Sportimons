using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGame;

public class PushOnCollision : MonoBehaviour
{
    [SerializeField] CollisionDetector detectorCollision;
    [SerializeField] private Vector3 forceCollision;

    void Start()
    {
        detectorCollision.OnCollisionEntered += PushObject;
    }

    void PushObject(Collision other)
    {
        other.transform.GetComponent<Rigidbody>().AddForce(forceCollision,ForceMode.Impulse);
    }
}
