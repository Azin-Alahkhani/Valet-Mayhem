using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{

    public bool paused;
    public static PauseMenu Instance;
    bool moved;
    int buttonIndex;
    public GameObject[] buttons;
    float timer;


    private void Awake()
    {
        Instance = this;
    }

    public void Freeze()
    {
        paused = true;
        Time.timeScale = 0.000000001f;
    }

    public void Unfreeze()
    {
        paused = false;
        Time.timeScale = 1;
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
            timer += Time.deltaTime * 1000000000;
            if (timer >= 0.25f)
            {
                moved = false;
                timer = 0;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        Input();
        Selection();
        Timer();

        for (int i = 0; i < Gamepad.all.Count; i++)
        {


            if (Gamepad.all[i].buttonSouth.wasPressedThisFrame)
            {
                buttons[buttonIndex].GetComponent<MenuButton>().Activate();
            }
        }
    }
}
