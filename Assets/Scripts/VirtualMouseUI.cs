using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;

public class VirtualMouseUI : MonoBehaviour
{

    public RectTransform canvas;
    VirtualMouseInput virtualMouseInput;

    private void Awake()
    {
        virtualMouseInput = GetComponent<VirtualMouseInput>();
    }

    private void Update()
    {
        transform.localScale = Vector3.one *  1f / canvas.localScale.x;
        transform.SetAsLastSibling();
    }

    private void LateUpdate()
    {
        Vector2 pos = virtualMouseInput.virtualMouse.position.value;
        pos.x = Mathf.Clamp(pos.x, 0, Screen.width);
        pos.y = Mathf.Clamp(pos.y, 0, Screen.height);

        InputState.Change(virtualMouseInput.virtualMouse.position, pos);
    }
}
