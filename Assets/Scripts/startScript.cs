using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startScript : MonoBehaviour
{
    public Button[] buttons;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    private void OnEnable()
    {
        buttons[0].onClick.AddListener(Play);
        //buttons[1].onClick.AddListener(ShowOptionsMenu);
        buttons[2].onClick.AddListener(() => { Application.Quit();});
    }

    private void OnDisable()
    {
        buttons[0].onClick.RemoveAllListeners();
        buttons[2].onClick.RemoveAllListeners();
    }
    void Play() {
        Debug.Log("play pressesd");
        SceneManager.LoadScene(1);
    }
}
