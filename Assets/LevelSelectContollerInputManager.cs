using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectContollerInputManager : MonoBehaviour
{
    public GameObject levelSelectOutline;
    public GameObject eventsOutline;

    public GameObject[] selectionObjects;

    public LevelScroll levelScroll;

    public bool moved = false;
    float timer;
    int index;

    public bool canScroll = true;

    public bool startBackTimer;

    float backTimer;

    // Start is called before the first frame update
    void Start()
    {
        canScroll = true;
    }

    // Update is called once per frame
    void Update()
    {




        if (canScroll)
        {

            

            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (!moved)
                {
                    float horizontal = Gamepad.all[i].leftStick.x.value;
                    float vertical = Gamepad.all[i].leftStick.y.value;

                    if (horizontal > 0.5f || Gamepad.all[i].dpad.right.wasPressedThisFrame)
                    {
                        index++;
                        if (index > selectionObjects.Length - 1)
                        {
                            index = 0;
                        }


                        selectionObjects[index].GetComponent<LevelSelectSelection>().selected = true;
                        moved = true;
                    }
                    else if (horizontal < -0.5f || Gamepad.all[i].dpad.left.wasPressedThisFrame)
                    {
                        index--;
                        if (index < 0)
                        {
                            index = selectionObjects.Length - 1;
                        }


                        selectionObjects[index].GetComponent<LevelSelectSelection>().selected = true;
                        moved = true;
                    }


                }



                if (Gamepad.all[i].buttonSouth.wasPressedThisFrame)
                {
                    startBackTimer = true;
                    selectionObjects[index].GetComponent<LevelSelectSelection>().activated = true;
                    selectionObjects[index].GetComponent<LevelSelectSelection>().OnActivation();
                }


                if (Gamepad.all[i].buttonEast.wasPressedThisFrame && !startBackTimer)
                {
                    SceneManager.LoadScene(1);                    
                }

                if (Gamepad.all[i].buttonNorth.wasPressedThisFrame)
                {
                    levelScroll.LoadLevel();
                }

            }
        }




        Selection();
        Timer();
    }

    void Selection()
    {
        foreach (var obj in selectionObjects)
        {
            if (obj == selectionObjects[index])
            {
                obj.GetComponent<LevelSelectSelection>().outline.SetActive(true);
            }
            else
            {
                obj.GetComponent<LevelSelectSelection>().outline.SetActive(false);
            }
        }
    }

    void Timer()
    {
        if (moved)
        {
            timer += Time.deltaTime;
            if (timer > 0.25f)
            {
                moved = false;
                timer = 0;
            }
        }

        if (startBackTimer)
        {
            backTimer += Time.deltaTime;
            if(backTimer >= 0.25f)
            {
                startBackTimer = false;
                backTimer = 0;
            }
        }
    }
}
