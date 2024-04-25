using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndSceneController : MonoBehaviour
{
    public TMP_Text[] scoresTxt ;

    public GameObject[] playerScorePanels;

    int playerNumber;

    void Start()
    {
        playerNumber = GameManagerController.playerNum;

        for (int i = 0; i < 8; i++)
        {
            scoresTxt[7-i].text = GameManagerController.endScores[i].ToString();
        }
        for (int i = 0; i < 8 ; i++)
        {
            if(i< playerNumber)
            {
                playerScorePanels[i].SetActive(true);

            }
            else
            {
                playerScorePanels[i].SetActive(false);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
