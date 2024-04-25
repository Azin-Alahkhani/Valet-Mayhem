 using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreUIController : MonoBehaviour
{
    public static ScoreUIController Instance;

    public TMP_Text[] scoresTxt = new TMP_Text[8];
    public event Action OnEndGame;

    public string endGameMsg;
    public int[] scores;

    public int winLimit;

    public TMP_Text timerTxt;

    public GameObject[] playerImages;
    public GameObject[] endGameImages;
    [HideInInspector] public int playersJoined;

    public float TimeLeft;
    public bool TimerOn = false;

    public GameObject[] crownIconPositions;

    public bool end;

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        end = false;

        Time.timeScale = 1;

        TimerOn = true;

        for (int i = endGameImages.Length - 1; i >= 0; i--)
        {
            endGameImages[i].gameObject.SetActive(false);
        }

        for (int i = playersJoined - 1; i >= 0; i--)
        {
            endGameImages[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < playerImages.Length; i++)
        {
            playerImages[i].gameObject.SetActive(false);
            
        }

        for (int i = 0; i < playersJoined; i++) 
        {
            playerImages[i].gameObject.SetActive(true);
        }
    }
    public int winnerIndex = 0;
    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                UpdateTimer(TimeLeft);
            }
            else
            {
                endGameMsg = "Time Is Up!";
                TimeLeft = 0;
                TimerOn = false;
                //Time.timeScale = 0;
                OnEndGame?.Invoke();
                end = true;
            }
        }
        for (int i = 0; i < 8; i++)
        {
           
            TMP_Text score = scoresTxt[i];
            scores[i] = int.Parse(score.text);
            if (scores[i] > 20000)
            {
                endGameMsg = "player #" + (i + 1) + " has won with " + scores[i] + " points!!";
                Debug.Log(endGameMsg);
                OnEndGame?.Invoke();
                end = true;
            }
        }
        UpdateWinner();

    }
    void UpdateWinner()
    {
        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] > scores[winnerIndex] && i!=winnerIndex)
            {
                winnerIndex = i;
            }

        }
        for (int i = 0; i < crownIconPositions.Length; i++)
        {   if(i== winnerIndex) {
                crownIconPositions[i].SetActive(true);
            } else
            {
                crownIconPositions[i].SetActive(false);
            }
           
        }
    }
    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        
        
        if (minutes==0 && seconds == 5)
        {
            AudioManager.Instance.PlayCountDownMusic();
            timerTxt.color = Color.red;
        }
        timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
