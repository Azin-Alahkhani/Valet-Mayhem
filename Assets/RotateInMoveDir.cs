using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInMoveDir : MonoBehaviour
{
    PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = transform.parent.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(new Vector3(controller.movement.x, 0, controller.movement.z) != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(controller.movement.x, 0, controller.movement.z));
        }
        
    }
}
