using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorEvent : StrikeEvent
{

    public GameObject meteor;




    // Start is called before the first frame update
    void Start()
    {
        Instantiate(meteor, new Vector3(Random.Range(RandomEventController.Instance.topLeft.position.x, RandomEventController.Instance.topRight.position.x), 200, 
            Random.Range(RandomEventController.Instance.topLeft.position.z, RandomEventController.Instance.bottomLeft.position.z)), Quaternion.identity);
        Destroy(gameObject, 15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
