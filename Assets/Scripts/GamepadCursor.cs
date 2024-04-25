using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
public class GamepadCursor : MonoBehaviour
{

    Mouse virtualMouse;
    public RectTransform cursor;
    public float speed = 1000f;
    bool previousMouseState;
    public RectTransform canvas;
    public PlayerInput input;
    float timer;
    bool pressed;

    private void OnEnable()
    {
        if(virtualMouse == null)
        {
            virtualMouse = (Mouse) InputSystem.AddDevice("VirtualMouse");
        }
        else if (!virtualMouse.added)
        {
            InputSystem.AddDevice(virtualMouse);
        }

        InputUser.PerformPairingWithDevice(virtualMouse, input.user);

        InputSystem.onAfterUpdate += UpdateMotion;

        if(cursor != null)
        {
            Vector2 pos = cursor.anchoredPosition;
            InputState.Change(virtualMouse.position, pos);
        }

    }

    private void OnDisable()
    {
        InputSystem.RemoveDevice(virtualMouse);
        InputSystem.onAfterUpdate -= UpdateMotion;
    }

    void UpdateMotion()
    {
        if(virtualMouse == null || Gamepad.current == null)
        {
            return;
        }

        Vector2 stickValue = Gamepad.all[0].leftStick.ReadValue();
        stickValue *= speed * Time.deltaTime;

        Vector2 currentPos = virtualMouse.position.ReadValue();
        Vector2 newPos = currentPos + stickValue;

        newPos.x = Mathf.Clamp(newPos.x, 0, Screen.width);
        newPos.y = Mathf.Clamp(newPos.y, 0, Screen.height);

        InputState.Change(virtualMouse.position, newPos);
        InputState.Change(virtualMouse.delta, stickValue);


        //bool aButton = Gamepad.all[0].aButton.IsPressed();
        //if (previousMouseState != Gamepad.all[0].aButton.isPressed)
        //{
        //    virtualMouse.CopyState<MouseState>(out var mouseState);
        //    mouseState.WithButton(MouseButton.Left, Gamepad.all[0].aButton.IsPressed());
        //    InputState.Change(virtualMouse, mouseState);
        //    previousMouseState = aButton;
        //    Debug.Log(newPos);
        //}

        MoveCursor(newPos);

    }

    void MoveCursor(Vector2 pos)
    {
        Vector2 anchorPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, pos, null, out anchorPos);
        cursor.anchoredPosition = anchorPos;

    }
}
