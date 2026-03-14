using UnityEngine;

public interface ICountFinger
{
    int fingerNumbers { get; set; }

}

public interface IDraggable : ICountFinger
{
    Vector3 currentPosition { get; set; }
    event System.Action<Vector3> OnDraggBegin;
    event System.Action<Vector3> OnDragging;
    event System.Action<Vector3> OnDraggEnd;
}


public interface ITouchable : ICountFinger
{
    float timeTouching { get; set; }
    event System.Action OnTouchBegin;
    event System.Action OnTouching;
    event System.Action OnTouchEnd;
}

public interface ISwipeable : ICountFinger
{
    float magnitudSwipe { get; set; }
    event System.Action<Vector3> OnSwipedMagnitud;
    event System.Action<SwipeDirection> OnSwiped;
}

public interface ISourceSelectable
{
    event System.Action OnSelected;
    event System.Action OnDeselected;
}

public enum SwipeDirection
{
    None,
    Up,
    Right,
    Left,
    Down
}

