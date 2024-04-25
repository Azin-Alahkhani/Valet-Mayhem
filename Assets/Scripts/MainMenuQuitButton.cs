using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuQuitButton : MenuButton
{
    public override void Activate()
    {
        Debug.Log("Quit");
        Application.Quit();

    }
}
