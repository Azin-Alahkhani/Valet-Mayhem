using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PowerUp/Shield")]
public class SheildPowerup : PowerUp
{

    public GameObject shieldPrefab;

    GameObject shieldObj;
    public override void Apply(GameObject gameObject)
    {
        if (gameObject.GetComponent<CarMovements>()) {

            gameObject.GetComponent<CarMovements>().isShielded = true;
            Debug.Log("shield activated");

            shieldObj = Instantiate(shieldPrefab, gameObject.transform);
            shieldObj.GetComponent<MoveWithCar>().move = true;
            shieldObj.GetComponent<MoveWithCar>().car = gameObject;
        }
    }

    public override void Cancel(GameObject gameObject)
    {
        if (gameObject.GetComponent<CarMovements>())
        {
            gameObject.GetComponent<CarMovements>().isShielded = false;
            Debug.Log("shield deactivated");
            Destroy(shieldObj);

        }
    }

}
