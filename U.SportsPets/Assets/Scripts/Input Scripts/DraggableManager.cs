using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableManager : MonoBehaviour, IDraggable, ICountFinger
{
    public event System.Action<Vector3> OnDraggBegin;
    public event System.Action<Vector3> OnDragging;
    public event System.Action<Vector3> OnDraggEnd;
    public Vector3 currentPosition { get; set; }    
    public int fingerNumbers { get; set; } = 1;

    void Start()
    {
        
    }
    
    void Update()
    {
        float z = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, z));
    }
}
