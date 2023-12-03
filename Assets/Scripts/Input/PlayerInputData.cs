using UnityEngine;

public class PlayerInputData
{
    public Vector2? CurrentTouchPosition;
    public Vector2? PreviousTouchPosition;

    public bool IsPressed;

    public bool IsJustClick;

    public bool WasSwap;
    public float PressedTime;
}