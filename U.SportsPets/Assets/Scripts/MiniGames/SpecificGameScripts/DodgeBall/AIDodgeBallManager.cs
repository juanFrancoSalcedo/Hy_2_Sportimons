using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDodgeBallManager : MonoBehaviour
{
    
    public event System.Action<Vector3> OnParticipantThreatened;
    
    public void ReceiveDangerComing( Vector3 playerPos, Vector3 dangerPos)
    {
        OnParticipantThreatened?.Invoke(playerPos - dangerPos);
    }
}
