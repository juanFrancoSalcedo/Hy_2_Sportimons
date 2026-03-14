using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour, IDraggable, ICountFinger, ISourceSelectable
{
    public LayerMask detectionLayer;
    public int fingerNumbers { get; set; } = 1;
   
    public Vector3 currentPosition { get; set; }
    public event System.Action<Vector3> OnDraggBegin;
    public event System.Action<Vector3> OnDragging;
    public event System.Action<Vector3> OnDraggEnd;
    public event System.Action OnSelected;
    public event System.Action OnDeselected;

    public bool freezeY;
    public bool freezeX;
    public bool freezeZ;
    [Tooltip("Is only useful when there are freeze objects")]
    public bool allowFingerOffset;
    public bool globalFinger;
    public bool useRigidbody = false;
    private bool touchedOnMe;

    private Vector3 positionBuffer;
    private Ray ray;
    private RaycastHit hit;
    private Rigidbody rigidBody;

    private void Awake()
    {
        gameObject.layer = Constants.LayerInputCollision;
        positionBuffer = transform.position;

        if (useRigidbody)
        {
            rigidBody = GetComponent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 80f, detectionLayer))
            {
                if (ReferenceEquals(hit.collider.gameObject, gameObject))
                {
                    positionBuffer = transform.position;
                    OnSelected?.Invoke();
                    touchedOnMe = true;
                    OnDraggBegin?.Invoke(positionBuffer);
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (globalFinger)
            {
                Move();
            }

            if (!Physics.Raycast(ray, out hit, 80f, detectionLayer) && !allowFingerOffset)
            {
                return;
            }

            if (hit.collider != null && ReferenceEquals(hit.collider.gameObject, gameObject) && touchedOnMe)
            {
                Move();
            }

            if (allowFingerOffset && touchedOnMe)
            {
                Move();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnDraggEnd?.Invoke(GetPositionInScreen());

            if (globalFinger) {return;}
            OnDeselected?.Invoke();
            touchedOnMe = false;
        }
    }

    private Vector3 GetPositionInScreen()
    {
        float z = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, z));
        return screenPos;
    }

    private void Move()
    {
        if (useRigidbody)
        {  MoveRigidbody(GetPositionInScreen());}
        else
        { MoveTransform(GetPositionInScreen()); }
       OnDragging?.Invoke(GetPositionInScreen());
    }


    private void MoveTransform(Vector3 _screenPos)
    {
        Vector3 movePosition = FreezePosition(_screenPos);
        transform.position = movePosition;
    }

    private void MoveRigidbody(Vector3 _screenPos)
    {
        Vector3 movePosition = FreezePosition(_screenPos);
        rigidBody.MovePosition(movePosition);
    }

    private Vector3 FreezePosition(Vector3 positionToMod)
    {
        Vector3 posFinal = positionToMod;
        if (freezeZ)
        {
            posFinal = new Vector3(posFinal.x, posFinal.y, positionBuffer.z);
        }
        if (freezeY)
        {
            posFinal = new Vector3(posFinal.x,positionBuffer.y, posFinal.z);
        }
        if (freezeX)
        {
            posFinal = new Vector3(positionBuffer.x, posFinal.y, posFinal.z);
        }
        return posFinal;
    }
}
