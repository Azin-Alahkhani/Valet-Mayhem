using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectMenuButton : MenuButton
{
    public override void Activate()
    {
        if (PlayerControllerManager.Instance != null)
        {
            Destroy(PlayerControllerManager.Instance.gameObject);
        }
        if (ActivatedEvents.Instance != null)
        {
            Destroy(ActivatedEvents.Instance.gameObject);
        }
        SceneManager.LoadScene(1);

    }
}
