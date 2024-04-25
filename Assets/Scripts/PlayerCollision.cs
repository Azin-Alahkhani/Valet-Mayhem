using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(gameObject);
        //Debug.Log(transform.parent);
        if (collision.gameObject.GetComponent<OnCollidedWith>())
        {
            if (!collision.gameObject.GetComponent<OnCollidedWith>().isProtected)
            {
                if (GetComponent<Rigidbody>().velocity.magnitude > 3)
                {
                    if (collision.gameObject.CompareTag("Player") && transform.CompareTag("pickedUpCar") && !transform.GetComponent<CarMovements>().parked && !collision.gameObject.GetComponent<OnCollidedWith>().collided)
                    {
                        collision.gameObject.GetComponent<OnCollidedWith>().Collided(2);
                        Vector3 dir = (transform.position - collision.transform.position).normalized;
                        collision.gameObject.GetComponent<Rigidbody>().AddForce(-dir * (30 * GetComponent<Rigidbody>().velocity.magnitude), ForceMode.Impulse);
                        
                    }
                }
            }
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }



    }
}
