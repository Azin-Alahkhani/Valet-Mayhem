using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMenuButton : MenuButton
{
    public override void Activate()
    {
        if(PlayerControllerManager.Instance != null)
        {
            Destroy(PlayerControllerManager.Instance.gameObject);
        }
        if(ActivatedEvents.Instance != null)
        {
            Destroy(ActivatedEvents.Instance.gameObject);
        }
        PauseMenu.Instance.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
