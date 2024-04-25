using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseResumeButton : MenuButton
{
    public override void Activate()
    {
        transform.parent.parent.gameObject.SetActive(false);
        PauseMenu.Instance.gameObject.SetActive(false);
        PauseMenu.Instance.Unfreeze();
    }
}
