using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Selected : LevelSelectSelection
{

    LevelScroll levelScroll;
    bool moved;
    float timer;

    private void Start()
    {

        levelScroll = transform.parent.parent.GetComponent<LevelScroll>();

    }

    public override void OnActivation()
    {
        buttonPrompt.text = "Press B/Circle to Deselect";
        for (int i = 0; i < outline.transform.childCount-1; i++)
        {
            if (outline.transform.GetChild(i).GetComponent<Image>())
            {
                outline.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
            
        }
    }

    void OnDeactivation()
    {
        buttonPrompt.text = "Press A/X to Select";
        for (int i = 0; i < outline.transform.childCount-1; i++)
        {
            if (outline.transform.GetChild(i).GetComponent<Image>())
            {
                outline.transform.GetChild(i).GetComponent<Image>().color = Color.grey;
            }
        }
    }

    private void Update()
    {
        if (activated)
        {
            inputManager.canScroll = false;
            

            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (!moved)
                {
                    float horizontal = -Gamepad.all[i].leftStick.x.value;

                    if (horizontal > 0.5f || Gamepad.all[i].dpad.left.wasPressedThisFrame)
                    {
                        levelScroll.ScrollLeft();
                        moved = true;
                    }
                    else if (horizontal < -0.5f || Gamepad.all[i].dpad.right.wasPressedThisFrame)
                    {
                        levelScroll.ScrollRight();
                        moved = true;
                    }
                }


                if (Gamepad.all[i].buttonEast.wasPressedThisFrame)
                {
                    OnDeactivation();
                    inputManager.startBackTimer = true;
                    activated = false;
                    inputManager.canScroll = true;
                }
            }

            if (moved)
            {
                timer += Time.deltaTime;
                if(timer >= 0.25f)
                {
                    moved = false;
                    timer = 0;
                }
            }
        }
    }

}
