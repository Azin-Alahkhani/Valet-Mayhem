using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEditor;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.InputSystem.Controls;

public class ChangeScene : MonoBehaviour
{

    Gamepad pad;
    public GameObject[] buttons;
    int buttonIndex = 0;
    bool moved;
    float timer;


    private void Start()
    {
    }

    void Input()
    {

        for (int i = 0; i < Gamepad.all.Count; i++) 
        {
            float vertical = Gamepad.all[i].leftStick.y.value;

            

            if ((vertical < -0.5f && !moved) || (Gamepad.all[i].dpad.down.wasPressedThisFrame && !moved))
            {
                buttonIndex++;
                if (buttonIndex > buttons.Length - 1)
                {
                    buttonIndex = 0;
                }
                moved = true;
            }
            else if ((vertical > 0.5f && !moved) || (Gamepad.all[i].dpad.up.wasPressedThisFrame && !moved))
            {
                buttonIndex--;
                if (buttonIndex < 0)
                {
                    buttonIndex = buttons.Length - 1;
                }
                moved = true;
            }
        }

    }

    void Selection()
    {
        foreach (GameObject button in buttons)
        {
            if (button == buttons[buttonIndex])
            {
                button.GetComponent<MenuButton>().selected = true;
            }
            else
            {
                button.GetComponent<MenuButton>().selected = false;
            }
        }
    }

    void Timer()
    {
        if (moved)
        {
            timer += Time.deltaTime;
            if (timer >= 0.25f)
            {
                moved = false;
                timer = 0;
            }
        }
    }


    private void Update()
    {

        for (int i = 0; i < Gamepad.all.Count; i++)
        {

            Input();
            Selection();
            Timer();


            if (Gamepad.all[i].buttonSouth.wasPressedThisFrame)
            {
                buttons[buttonIndex].GetComponent<MenuButton>().Activate();
            }
        }
    }


    public void startScene()
    {
        SceneManager.LoadScene(1);
    }

    public void quit()
    {
        Application.Quit();
    }
}
