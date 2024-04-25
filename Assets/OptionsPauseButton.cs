using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPauseButton : MenuButton
{
    public override void Activate()
    {
        Debug.Log("options");
        PauseMenu.Instance.gameObject.SetActive(false);
    }
}
