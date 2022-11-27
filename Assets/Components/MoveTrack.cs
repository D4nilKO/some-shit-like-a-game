using UnityEngine;

public class MoveTrack : MonoBehaviour
{
    [HideInInspector] public Vector2 moveInput;

    public Joystick joystick;

    public ControlType controlType;

    public enum ControlType
    {
        PC,
        Android
    };
    
    public Vector2 MovementLogic()
    {
        switch (controlType)
        {
            case ControlType.PC:
                moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                joystick.gameObject.SetActive(false);
                break;
            case ControlType.Android:
                moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
                joystick.gameObject.SetActive(true);
                break;
            default: break;
        }

        return new Vector2(moveInput.x, moveInput.y);
    }
}