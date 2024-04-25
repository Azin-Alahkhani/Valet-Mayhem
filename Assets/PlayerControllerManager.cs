using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class Controller
{
    public GameObject playerCard;
    public bool left;
    public bool right;
}

[System.Serializable]
public class PlayerWithController
{
    public int controllerIndex;
    public enum ControllerSide {Left, Right, Unnassigned };
    public ControllerSide controllerSide = ControllerSide.Unnassigned;
    public bool joined;
    public GameObject card;
    public int playerNumber;
    public Gamepad pad;
    public int colourIndex;
    public Color selectedColour;
    public Material selectedMaterial;
    public bool isReady;
}


public class PlayerControllerManager : MonoBehaviour
{
    public List<PlayerWithController> players;
    public List<Controller> controllers;
    public static PlayerControllerManager Instance;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
