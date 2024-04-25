using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenuButton : MenuButton
{

    public int sceneToLoad = 3;
    public override void Activate()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
