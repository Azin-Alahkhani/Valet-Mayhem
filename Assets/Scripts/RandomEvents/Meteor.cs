using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float explosionRadius;
    public float explosionForce;
    public GameObject shadow;
    GameObject spawnedShadow;
    public LayerMask groundLayer;
    public float shadowSizeSpeed;
    public float maxShadowSize;
    bool spawned;
    public float timeBeforeFall;
    float timer;
    bool fall;
    bool timerActive = true;
    bool hasExploded;
    public ParticleSystem explosion;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        Destroy(gameObject, 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            timer += Time.deltaTime;
            if (timer >= timeBeforeFall)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                fall = true;
                GetComponent<Rigidbody>().AddForce(-transform.up * 200, ForceMode.Impulse);
                timerActive = false;

            }
        }


        if (!spawned) 
        {
            if (Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit, 1000, groundLayer))
            {
                spawnedShadow = Instantiate(shadow, hit.point + new Vector3(0, 5, 0), Quaternion.identity);
                spawned = true;
            }

            //Debug.DrawRay(transform.position, -Vector3.up * 1000, Color.red);
        }

        if(spawnedShadow != null)
        {
            spawnedShadow.transform.localScale = Vector3.Lerp(spawnedShadow.transform.localScale, new Vector3(maxShadowSize, spawnedShadow.transform.localScale.y, maxShadowSize), Time.deltaTime * shadowSizeSpeed);
        }

    }

    void Explode()
    {
        hasExploded = true;
        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider col in cols)
        {

            if (col.GetComponent<Rigidbody>())
            {
                if(col.tag == "Barrier")
                {
                    col.GetComponent<Rigidbody>().isKinematic = false;
                }
                Vector3 dir = col.transform.position - transform.position;

                col.GetComponent<Rigidbody>().AddForce(dir * explosionForce);
            }

        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!hasExploded)
        {
            //Debug.Log(other.collider);
            if (other.collider.GetComponentInParent<CarMovements>())
            {
                other.collider.GetComponentInParent<CarMovements>().ReduceLifeOnDamage(1000);
            }

            if (other.collider.CompareTag("Player"))
            {
                other.collider.GetComponent<OnCollidedWith>().Collided(5);
            }

            if (other.collider.gameObject.layer == 7)
            {
                GetComponent<Rigidbody>().isKinematic = true;
            }

            if (spawnedShadow != null)
            {
                Destroy(spawnedShadow);
            }

            if (!other.collider.CompareTag("Water"))
            {
                Explode();
            }

            
            
        }

        
    }
}
