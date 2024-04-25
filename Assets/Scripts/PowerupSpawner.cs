using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject[] positions;

    float  timer=5;

    public GameObject[] powerups;
   
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer = 5;
            
            SpawnPowerUp();
        }
    }
    void SpawnPowerUp()
    {

        int index=Random.Range(0, powerups.Length);
        int pos = -1;
        for (int i = 0; i < positions.Length; i++)
        {
         if (positions[i].GetComponentInChildren<PowerUpController>()!=null)
            {
              
            }   else
            {
                pos = i;
                break;
            }
        }

        if (pos > -1)
        {
        Instantiate(powerups[index], positions[pos].gameObject.transform,false);
        
        }
    }
}
