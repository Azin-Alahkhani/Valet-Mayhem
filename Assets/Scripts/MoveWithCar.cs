using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithCar : MonoBehaviour
{
    public bool move = false;

    public GameObject car;

    float timer = 7;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
        if (move && car)
        {
            gameObject.transform.position = car.transform.position;
        }
        if(car.GetComponent<CarMovements>().parked ) {
            Destroy(gameObject);
        }
    }
}
