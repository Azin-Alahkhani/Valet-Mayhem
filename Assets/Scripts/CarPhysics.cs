using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour
{
    // Start is called before the first frame update

    public float forceMagnitude;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {







        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        Vector3 forceDirection = hit.gameObject.transform.position - transform.position;

        forceDirection.y = 0;
        forceDirection.Normalize();

        rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);



    }









}
