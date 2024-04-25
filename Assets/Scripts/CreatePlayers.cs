using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;



public class CreatePlayers : MonoBehaviour
{
    public static CreatePlayers Instance;
    public GameObject playerPrefab;
    public List<GameObject> players;
    public Material[] playerMats;
    public TMP_Text[] scoreTexts;
    public Transform playerSpawnParent;
    int index = 0;
    int controllerIndex = 0;
    public GameObject playerNumberText;

    public GameObject[] playerPrefabs;
    public GameObject[] playerIcons;

    //public InputActionAsset controlScheme;


    private void Awake()
    {
        Instance = this;
    }

    void SpawnPlayer(PlayerInput player, int controllerIndex, string controlScheme, PlayerWithController pwc)
    {
        if (pwc.isReady)
        {
            player.GetComponent<PlayerController>().pad = Gamepad.all[controllerIndex];
            player.GetComponent<PlayerController>().controlScheme = controlScheme;
            player.GetComponent<PlayerController>().OnSpawn();
            players.Add(player.gameObject);
            player.GetComponent<PlayerController>().scoreTextMesh = scoreTexts[index];
            player.transform.position = new Vector3(playerSpawnParent.GetChild(index).position.x, playerSpawnParent.GetChild(index).position.y + 1, playerSpawnParent.GetChild(index).position.z);
            player.GetComponent<MeshRenderer>().material = playerMats[pwc.colourIndex];
            if (player.transform.childCount > 1)
            {
                var skinnedRenderer = player.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>();

                for (int i = 0; i < skinnedRenderer.materials.Length; i++)
                {
                    if (skinnedRenderer.materials[i].name == player.transform.GetChild(0).GetChild(1).GetComponent<ChangeColour>().materialToChange.name + " (Instance)")
                    {
                        Material[] mats = skinnedRenderer.materials;
                        mats[i] = playerMats[pwc.colourIndex];
                        skinnedRenderer.materials = mats;
                    }
                }
            }


            if (ActivatedEvents.Instance.showPlayerNumber)
            {
                GameObject txt = Instantiate(playerNumberText);
                txt.GetComponent<TextMeshPro>().text = "P" + (pwc.playerNumber).ToString();
                txt.GetComponent<FollowPlayer>().target = player.transform;
                player.GetComponent<PlayerController>().playerNumberText = txt.GetComponent<FollowPlayer>();
            }

            if (ActivatedEvents.Instance.showPlayerIcon)
            {
                GameObject icon = Instantiate(playerIcons[pwc.playerNumber-1]);
                icon.GetComponent<FollowPlayer>().target = player.transform;
                icon.GetComponent<FollowPlayer>().yOffset = 0.25f;
                player.GetComponent<PlayerController>().icon = icon;
                icon.transform.localScale = new Vector3(1, 1, 1);
                
            }

            if (pwc.selectedMaterial != null)
            {
                player.GetComponent<MeshRenderer>().material = pwc.selectedMaterial;
            }


            if (Camera.main.GetComponent<MultipleTargetCamera>())
            {
                Camera.main.GetComponent<MultipleTargetCamera>().targets.Add(player.transform);
            }
            ScoreUIController.Instance.playersJoined++;
            index++;
        }

    }

    // Start is called before the first frame update
    void Start()
    {

       // Debug.Log(Gamepad.all.Count);


        foreach (var player in PlayerControllerManager.Instance.players)
        {
            
            if (player.joined)
            {
                if (player.controllerSide == PlayerWithController.ControllerSide.Left)
                {
                    var playerSpawn = PlayerInput.Instantiate(playerPrefabs[player.playerNumber-1], controlScheme: "GamePadLeft", pairWithDevice: Gamepad.all[player.controllerIndex]);
                    SpawnPlayer(playerSpawn, player.controllerIndex, "GamePadLeft", player);
                }
                else
                {
                    var playerSpawn = PlayerInput.Instantiate(playerPrefabs[player.playerNumber-1], controlScheme: "GamePadRight", pairWithDevice: Gamepad.all[player.controllerIndex]);
                    SpawnPlayer(playerSpawn, player.controllerIndex, "GamePadRight", player);
                }
            }


            
        }

        //for (int i = 0; i < Gamepad.all.Count; i++)
        //{

        //    var player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "GamePadLeft", pairWithDevice: Gamepad.all[controllerIndex]);
        //    player1.GetComponent<PlayerController>().pad = Gamepad.all[i];
        //    player1.GetComponent<PlayerController>().controlScheme = "GamePadLeft";
        //    player1.GetComponent<PlayerController>().OnSpawn();
        //    players.Add(player1.gameObject);
        //    player1.GetComponent<PlayerController>().scoreTextMesh = scoreTexts[index];
        //    player1.transform.position = playerSpawnParent.GetChild(index).position;
        //    player1.GetComponent<MeshRenderer>().material = playerMats[index];
        //    index++;

        //    var player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "GamePadRight", pairWithDevice: Gamepad.all[controllerIndex]);
        //    player2.GetComponent<PlayerController>().pad = Gamepad.all[i];
        //    player2.GetComponent<PlayerController>().controlScheme = "GamePadRight";
        //    player2.GetComponent<PlayerController>().OnSpawn();
        //    players.Add(player2.gameObject);
        //    player2.GetComponent<PlayerController>().scoreTextMesh = scoreTexts[index];
        //    player2.transform.position = playerSpawnParent.GetChild(index).position;
        //    player2.GetComponent<MeshRenderer>().material = playerMats[index];

        //    index++;
        //    controllerIndex++;

        //    if (Camera.main.GetComponent<MultipleTargetCamera>())
        //    {
        //        Camera.main.GetComponent<MultipleTargetCamera>().targets.Add(player1.transform);
        //        Camera.main.GetComponent<MultipleTargetCamera>().targets.Add(player2.transform);
        //    }

        //}

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
