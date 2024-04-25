using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;


[CreateAssetMenu(menuName = "PowerUp/HealthBuff")]

public class HealthBuff : PowerUp
{
    public int amount;

    public GameObject powerUpFloatingText;
    public override void Apply(GameObject gameObject)
    {
        gameObject.GetComponent<CarMovements>().thisCar.life += amount;
        if(gameObject.GetComponent<CarMovements>().thisCar.life > 100)
        {
            gameObject.GetComponent<CarMovements>().thisCar.life = 100; 
        }

        if (powerUpFloatingText != null)
        {
            var go = Instantiate(powerUpFloatingText, new Vector3(gameObject.transform.position.x, 2, gameObject.transform.position.z), Quaternion.Euler(90, 0, 0), gameObject.transform);
            go.GetComponent<TextMeshPro>().color = Color.green;
            go.GetComponent<TextMeshPro>().text = "" + gameObject.GetComponent<CarMovements>().thisCar.life;
        }

    }

    public override void Cancel(GameObject gameObject)
    {
       
    }
}
