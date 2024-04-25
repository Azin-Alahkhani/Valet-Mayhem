using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManagerController : MonoBehaviour
{


    public ScoreUIController scoreUIController;
    public GameObject mainScene;
    public GameObject endScene;

    public AudioSource gameAudio;
    public AudioSource endAudio;

    public TMP_Text EndMessage;

    public PlayerControls playerControls;

    public TMP_Text[] scoresTxt = new TMP_Text[8];

    public Toggle audioToggle;
      
    public static float[] endScores = new float[8];


    public string gameMode = "start";

    public static int playerNum;

    public GameObject[] crownIcons;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
        playerControls.Restart.action.performed += ctx => RestartGame();
        playerControls.Start.action.performed += ctx => StartGame();
        playerControls.Exit.action.performed += ctx => QuitGame();

       
    }
    private void OnDisable()
    {
        playerControls.Enable();
        playerControls.Restart.action.performed -= ctx => RestartGame();
        playerControls.Start.action.performed -= ctx => StartGame();
        playerControls.Exit.action.performed -= ctx => QuitGame();
    }
    private void QuitGame()
    {
        Debug.Log("quit on e");
        Application.Quit();
    }
    private void StartGame()
    {
        if (gameMode == "start")
        {
           
            if (mainScene != null)
                mainScene.SetActive(true);
            gameMode = "play";
        }

    }

    private void Update()
    {
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            if (Gamepad.all[i].startButton.wasPressedThisFrame && !endScene.activeSelf)
            {
                if (!PauseMenu.Instance.gameObject.activeSelf)
                {
                    PauseMenu.Instance.gameObject.SetActive(true);
                    PauseMenu.Instance.Freeze();
                }
                else
                {
                    PauseMenu.Instance.gameObject.SetActive(false);
                    PauseMenu.Instance.Unfreeze();
                }
                
            }
        }
    }
    void Start()
    {

        scoreUIController.OnEndGame += LoadEndGameScene;
        endScene.SetActive(false);
        mainScene.SetActive(true);

        
       
        //if (audioToggle != null)
        //    audioToggle.onValueChanged.AddListener(OnToggleValueChanged);

    }
    //void OnToggleValueChanged(bool isOn)
    //{
    //    if (isOn)
    //    {
    //        gameAudio.Stop();
    //    }
    //    if (!isOn)
    //    {
    //        gameAudio.Play();
    //    }
    //}

    private void LoadEndGameScene()
    {
        
        Debug.Log("game over scene load");
        PauseMenu.Instance.gameObject.SetActive(false);
        EndMessage.text = scoreUIController.endGameMsg;

       
        playerNum = scoreUIController.playersJoined;

        for (int i = 0; i < playerNum; i++)
        {
            scoresTxt[i].text = ScoreUIController.Instance.scoresTxt[i].text;
            endScores[i] = int.Parse(scoreUIController.scoresTxt[i].text);
            scoresTxt[i].transform.parent.GetComponent<EndScore>().score = int.Parse(ScoreUIController.Instance.scoresTxt[i].text);
        }

        endScene.SetActive(true);
        mainScene.SetActive(false);
        //gameAudio.Stop();
        endAudio.Play();
        gameMode = "End";
        SortScores();
        for (int i = 0; i < 8; i++)
        {
            if (i   == ScoreUIController.Instance.winnerIndex)
                crownIcons[i].SetActive(true);
            else crownIcons[i].SetActive(false);
        }


        // SceneManager.LoadScene(4);

    }
    void SortScores()
    {
        
    }
    


    void RestartGame()
    {
        if (gameMode == "End")
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
          
            mainScene.SetActive(true);
            endScene.SetActive(false);
            gameMode = "start";
        }
    }

}
