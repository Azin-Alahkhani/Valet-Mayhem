using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EventsSelection : LevelSelectSelection
{

    public List<GameObject> selectionObjects;
    bool moved;
    int index;
    float timer;
    bool startStartTimer;
    float startTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnActivation()
    {
        buttonPrompt.text = "Press B/Circle to Deselect";
        startStartTimer = true;
        index = 0;
        for (int i = 0; i < outline.transform.childCount; i++) 
        {
            if (outline.transform.GetChild(i).GetComponent<Image>())
            {
                outline.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
        }
    }

    void OnDeactivation()
    {
        foreach (var obj in selectionObjects)
        {
            obj.GetComponent<EventsButtons>().selectionOutline.SetActive(false);
            Debug.Log(obj.GetComponent<EventsButtons>().selectionOutline);
        }

        buttonPrompt.text = "Press A/X to Select";
        for (int i = 0; i < outline.transform.childCount; i++)
        {
            if (outline.transform.GetChild(i).GetComponent<Image>())
            {
                outline.transform.GetChild(i).GetComponent<Image>().color = Color.grey;
            }
        }

        selectionObjects[index].GetComponent<EventsButtons>().selectionOutline.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (startStartTimer)
        {
            startTimer += Time.deltaTime;
            if(startTimer > 0.25f)
            {
                startStartTimer = false;
                startTimer = 0;
            }
        }

        if (activated)
        {

            Selection();

            inputManager.canScroll = false;

            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (!moved)
                {
                    float vertical = -Gamepad.all[i].leftStick.y.value;

                    if (vertical >= 1f || Gamepad.all[i].dpad.down.wasPressedThisFrame)
                    {
                        index++;
                        if (index > selectionObjects.Count - 1)
                        {
                            index = 0;
                        }


                        selectionObjects[index].GetComponent<EventsButtons>().selected = true;
                        moved = true;
                    }
                    else if (vertical <= -1f || Gamepad.all[i].dpad.up.wasPressedThisFrame)
                    {
                        index--;
                        if (index < 0)
                        {
                            index = selectionObjects.Count - 1;
                        }


                        selectionObjects[index].GetComponent<EventsButtons>().selected = true;
                        moved = true;
                    }
                }

                if (Gamepad.all[i].buttonSouth.wasPressedThisFrame && !startStartTimer)
                {
                    if (selectionObjects[index].GetComponent<AssignIndex>())
                    {
                        selectionObjects[index].transform.GetChild(1).GetComponent<Toggle>().isOn = !selectionObjects[index].transform.GetChild(1).GetComponent<Toggle>().isOn;
                    }
                    else if (selectionObjects[index].GetComponent<Button>())
                    {
                        selectionObjects[index].GetComponent<Button>().onClick.Invoke();
                    }
                }

                if (Gamepad.all[i].buttonEast.wasPressedThisFrame)
                {
                    activated = false;
                    OnDeactivation();
                    inputManager.startBackTimer = true;
                    
                    inputManager.canScroll = true;
                }
            }



            
            Timer();
        }
    }

    void Timer()
    {
        timer += Time.deltaTime;
        if(timer >= 0.25f)
        {
            moved = false;
            timer = 0;
        }
    }

    void Selection()
    {
        foreach (var obj in selectionObjects)
        {
            if (obj == selectionObjects[index])
            {
                obj.GetComponent<EventsButtons>().selectionOutline.SetActive(true);
            }
            else
            {
                obj.GetComponent<EventsButtons>().selectionOutline.SetActive(false);
            }
        }
    }
}
