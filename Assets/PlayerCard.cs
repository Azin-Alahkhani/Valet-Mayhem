using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{
    public int cardIndex;
    public bool cardJoined;
    public int padIndex;
    public PlayerWithController.ControllerSide side;
    SelectScreenManager manager;
    public bool ready;
    float timer;
    bool startTimer;
    bool canLeave;
    Image readyImage;
    public TextMeshProUGUI readyText;


    // Start is called before the first frame update
    void Start()
    {
        manager = transform.parent.gameObject.GetComponent<SelectScreenManager>();
        readyImage = transform.GetChild(4).GetChild(1).GetComponent<Image>();


    }

    // Update is called once per frame
    void Update()
    {
        if (side == PlayerWithController.ControllerSide.Left)
        {
            if (Gamepad.all[padIndex].leftShoulder.wasPressedThisFrame && cardJoined && !ready)
            {
                startTimer = true;
                ready = true;
                readyImage.color = Color.green;
                manager.ReadyCheck();
            }
        }
        else if (side == PlayerWithController.ControllerSide.Right)
        {
            if (Gamepad.all[padIndex].rightShoulder.wasPressedThisFrame && cardJoined && !ready)
            {
                startTimer = true;
                ready = true;
                readyImage.color = Color.green;
                manager.ReadyCheck();
            }
        }


        if (side == PlayerWithController.ControllerSide.Left)
        {
            readyText.text = "Press Left Bumper to Ready Up";
        }
        else if (side == PlayerWithController.ControllerSide.Right)
        {
            readyText.text = "Press Right Bumber to Ready Up";
        }

        if (startTimer)
        {
            timer += Time.deltaTime;
            if(timer >= 0.1f)
            {
                canLeave = true;
                startTimer = false;
                timer = 0;
            }
        }

        if (canLeave)
        {
            if (side == PlayerWithController.ControllerSide.Right)
            {
                if (Gamepad.all[padIndex].rightShoulder.wasPressedThisFrame && cardJoined && ready)
                {
                    ready = false;
                    canLeave = false;
                    readyImage.color = Color.red;
                    manager.ReadyCheck();
                }
            }
            else if (side == PlayerWithController.ControllerSide.Left)
            {
                if (Gamepad.all[padIndex].leftShoulder.wasPressedThisFrame && cardJoined && ready)
                {
                    ready = false;
                    canLeave = false;
                    readyImage.color = Color.red;
                    manager.ReadyCheck();
                }
            }
        }


    }


    void Leave()
    {
        manager.joinedIndex.Remove(cardIndex);
        cardJoined = false;
        foreach (var player in PlayerControllerManager.Instance.players)
        {
            if(player.card == gameObject)
            {
                player.joined = false;
                player.card = null;
            }
        }

        manager.index = 0;
        
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
        transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "";
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";

        if (side == PlayerWithController.ControllerSide.Left)
        {
            PlayerControllerManager.Instance.controllers[padIndex].left = false;
        }
        else if (side == PlayerWithController.ControllerSide.Right)
        {
            PlayerControllerManager.Instance.controllers[padIndex].right = false;
        }
    }
}
