using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.Serializable]
public class ColourMaterialPair
{
    public Color color;
    public Material material;
}

public class SelectScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject collum;
    public List<GameObject> list = new List<GameObject>();
    private int selectedSkin = 0;
    private GameObject tempGO;
    public GameObject startText;
    public List<int> joinedIndex;
    public ColourMaterialPair[] availableColours;
    public GameObject timerPanel;
    public TextMeshProUGUI timerText;
    float readyTimer;
    bool startTimer;
    public float maxTimeAfterReady;
    public int index = 0;


    private void Start()
    {

        Time.timeScale = 1;

        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            PlayerControllerManager.Instance.controllers.Add(new Controller());
        }

        for (int i = 0; i < Gamepad.all.Count * 2; i++) 
        {
            PlayerControllerManager.Instance.players.Add(new PlayerWithController());
            //if( i % 2 != 0)
            //{
            //    PlayerControllerManager.Instance.players[i].controllerSide = PlayerWithController.ControllerSide.Right;
            //}
        }

        for(int i = 0; i < transform.childCount-1; i++)
        {
            availableColours[i].color = transform.GetChild(i).GetComponent<Image>().color;
            //PlayerControllerManager.Instance.players[i].selectedMaterial = availableColours[i].material;
        }

        
        tempGO = collum;
    }

    void PlayerSetup(int i, PlayerWithController player, Controller controller, PlayerWithController.ControllerSide side)
    {

    }
    
    void Join(int i, PlayerWithController player, Controller controller)
    {
        

        if (Gamepad.all[i].startButton.wasPressedThisFrame && !player.joined && !controller.right)
        {

            //Debug.Log(i);
            PlayerSetup(i, player, controller, PlayerWithController.ControllerSide.Right);
            player.joined = true;
            
            player.card = GetNextCard();
            player.card.GetComponent<PlayerCard>().cardJoined = true;
            player.card.GetComponent<PlayerCard>().padIndex = i;
            player.card.GetComponent<PlayerCard>().side = PlayerWithController.ControllerSide.Right;
            player.card.transform.GetChild(2).gameObject.SetActive(false);
            //player.card.SetActive(true);
            player.controllerSide = PlayerWithController.ControllerSide.Right;
            controller.right = true;
            controller.playerCard = player.card;
            player.selectedColour = player.card.GetComponent<Image>().color;
            player.controllerIndex = i;
            player.playerNumber = index;
            player.card.transform.GetChild(0).gameObject.SetActive(true);
            player.card.transform.GetChild(3).gameObject.SetActive(true);
            player.card.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Press X/Square button to change colour";
            player.card.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "P" + (player.playerNumber).ToString();
            player.card.transform.GetChild(4).gameObject.SetActive(true);
            player.pad = Gamepad.all[i];
            player.colourIndex = index-1;
            joinedIndex.Add(index);
            //Debug.Log(player.pad);
            //NextOption();
            //if (index < PlayerControllerManager.Instance.players.Count)
            //{
            //    index++;
            //}
            
        }

        else if (Gamepad.all[i].selectButton.wasPressedThisFrame && !player.joined && !controller.left)
        {
            player.joined = true;
            player.card = GetNextCard();
            player.card.transform.GetChild(2).gameObject.SetActive(false);
            player.card.GetComponent<PlayerCard>().cardJoined = true;
            player.card.GetComponent<PlayerCard>().padIndex = i;
            player.card.GetComponent<PlayerCard>().side = PlayerWithController.ControllerSide.Left;
            //player.card.SetActive(true);
            player.controllerSide = PlayerWithController.ControllerSide.Left;
            controller.left = true;
            controller.playerCard = player.card;
            player.selectedColour = player.card.GetComponent<Image>().color;
            player.controllerIndex = i;
            player.playerNumber = index;
            player.card.transform.GetChild(0).gameObject.SetActive(true);
            player.card.transform.GetChild(3).gameObject.SetActive(true);
            player.card.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Press Left Dpad button to change colour";
            player.card.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "P" + (player.playerNumber).ToString();
            player.card.transform.GetChild(4).gameObject.SetActive(true);
            player.pad = Gamepad.all[i];
            player.colourIndex = index - 1;
            joinedIndex.Add(player.card.GetComponent<PlayerCard>().cardIndex);
            //Debug.Log(player.pad);
            //NextOption();
            //if (index < PlayerControllerManager.Instance.players.Count)
            //{
            //    index++;
            //}
        }
    }

    public void Leave()
    {

    }

    GameObject GetNextCard()
    {
        index = 0;
        foreach (var card in list)
        {
            index++;
            if (!card.GetComponent<PlayerCard>().cardJoined)
            {
                
                return card;
            }
        }
        return null;
    }

    void Leave(int i , Controller controller, PlayerWithController pwc)
    {
        //if (Gamepad.all[i].selectButton.wasPressedThisFrame && pwc.joined)
        //{

            
            
            
        //}


        //else if (Gamepad.all[i].startButton.wasPressedThisFrame)
        //{
        //    foreach (var player in PlayerControllerManager.Instance.players)
        //    {
        //        if (controller.right && (player.controllerIndex == i && player.controllerSide == PlayerWithController.ControllerSide.Right))
        //        {
        //            player.joined = false;
        //            player.card.transform.GetChild(0).gameObject.SetActive(false);
        //            player.card.transform.GetChild(3).gameObject.SetActive(false);
        //            player.card.transform.GetChild(2).gameObject.SetActive(true);
        //            controller.right = false;
        //            index--;
        //        }
        //    }
            
        //    //BackOption();
            
        //}
        
    }

    private void Update()
    {

        

        if(index < 0)
        {
            index = 0;
        }
        else if(index > 7)
        {
            index = 7;
        }

        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            if (index > 0)
            {
                //Leave(i, PlayerControllerManager.Instance.controllers[i], PlayerControllerManager.Instance.players[index]);
            }

            if (index < PlayerControllerManager.Instance.players.Count)
            {
                Join(i, PlayerControllerManager.Instance.players[index], PlayerControllerManager.Instance.controllers[i]);
            }
            

           
        }

        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            if (Gamepad.all[i].buttonNorth.wasPressedThisFrame && startText.activeSelf)
            {
                //RemoveUnreadyPlayers();
                SceneManager.LoadScene(2);
            }
            else if (Gamepad.all[i].buttonEast.wasPressedThisFrame)
            {
                SceneManager.LoadScene(0);
            }
        }


        ColourChange();
        ReadyTimer();

        //for (int i = 0; i < Gamepad.all.Count; i++)
        //{
        //    if (Gamepad.all[i].buttonSouth.isPressed)
        //    {
        //        NextOption();
        //    }
        //    else if (Gamepad.all[i].buttonEast.isPressed)
        //    {
        //        BackOption();
        //    }
        //}


        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    NextOption();
        //}
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    BackOption();
        //}
    }
    //public void NextOption()
    //{


    //    tempGO = list[selectedSkin];
    //    selectedSkin = selectedSkin + 1;
    //    tempGO.SetActive(true);
    //}
    //public void BackOption()
    //{
    //    //tempGO.SetActive(false);
    //    selectedSkin = selectedSkin - 1;
    //    tempGO = list[selectedSkin];

    //}
    void ColourChange()
    {
        foreach (var player in PlayerControllerManager.Instance.players)
        {
            if (player.joined)
            {
                if (player.colourIndex > availableColours.Length)
                {
                    player.colourIndex = 0;
                }

                if (player.controllerSide == PlayerWithController.ControllerSide.Right)
                {
                    if (player.pad.buttonWest.wasPressedThisFrame)
                    {
                        player.colourIndex++;
                        if (player.colourIndex > availableColours.Length - 1)
                        {
                            player.colourIndex = 0;
                        }
                        player.card.GetComponent<Image>().color = availableColours[player.colourIndex].color;
                        player.selectedColour = availableColours[player.colourIndex].color;
                        player.selectedMaterial = availableColours[player.colourIndex].material;

                    }
                }
                else if (player.controllerSide == PlayerWithController.ControllerSide.Left)
                {
                    if (player.pad.dpad.left.wasPressedThisFrame)
                    {
                        player.colourIndex++;
                        if (player.colourIndex > availableColours.Length - 1)
                        {
                            player.colourIndex = 0;
                        }
                        player.card.GetComponent<Image>().color = availableColours[player.colourIndex].color;
                        player.selectedColour = availableColours[player.colourIndex].color;
                        player.selectedMaterial = availableColours[player.colourIndex].material;
                    }
                }
            }
        }
    }

    void ReadyTimer()
    {
        if (startTimer)
        {
            readyTimer -= Time.deltaTime;

            timerText.text =  "Starting In:\n" + Mathf.Round(readyTimer).ToString();

            if(readyTimer <= 0)
            {
                SceneManager.LoadScene(2);
                //RemoveUnreadyPlayers();
            }
        }
    }

    void RemoveUnreadyPlayers()
    {
        foreach (var player in PlayerControllerManager.Instance.players)
        {
            if (!player.card.GetComponent<PlayerCard>().ready)
            {
                player.joined = false;
            }
        }
    }

    public void ReadyCheck()
    {
        float ready = 0;
        float notReady = 0;
        foreach (var card in list)
        {
            if (card.GetComponent<PlayerCard>().cardJoined)
            {
                if (card.GetComponent<PlayerCard>().ready)
                {
                    ready++;
                }
                else
                {
                    notReady++;
                }
            }

        }

        foreach (var player in PlayerControllerManager.Instance.players)
        {
            if(player.card != null)
            {
                if (player.card.GetComponent<PlayerCard>().ready)
                {
                    player.isReady = true;
                }
                else
                {
                    player.isReady = false;
                }
            }

        }

        float total = ready + notReady;

        

        float percent = (ready / total) * 100;

        if(percent >= 50)
        {
            startTimer = true;
            timerPanel.SetActive(true);
            readyTimer = maxTimeAfterReady;
        }
        else
        {
            startTimer = false;
            timerPanel.SetActive(false);
            readyTimer = maxTimeAfterReady;
        }


        if (percent >= 100)
        {
            startText.SetActive(true);
        }
        else
        {
            startText.SetActive(false);
        }

    }

}


