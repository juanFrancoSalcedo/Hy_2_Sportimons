using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateObject : MonoBehaviour, IPreWarmingObject
{
    public Vector3 movementVector;
    public Vector3 limitPosition;
    public Vector3 bufferMoveVector { get; private set; }
    public bool freezed= true;
    bool warmed;

    private void Start()
    {
        bufferMoveVector = movementVector;
        if (GameController.Instance != null)
        {
            GameController.Instance.OnPreGameCountEnded += ActiveWarmingBehaviour;
        }
    }

    public void ActiveWarmingBehaviour()
    {
        warmed = true;
    }
    
    void Update()
    {
        if (!warmed) { return; }
        if (freezed) { return; }

        if (Vector3.Distance(transform.position, limitPosition) > 0.5f)
        {
            transform.Translate(movementVector);
        }
    }

    public void Stop(bool stoped)
    {
        freezed = stoped;
    }
}
