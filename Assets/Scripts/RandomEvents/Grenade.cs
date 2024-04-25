using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    public bool startCountdown;
    public float explodeTime;
    public float radius;
    float grenadeTimer;
    public int damage;
    LineRenderer lr;
    [HideInInspector]
    public GrenadeTimer grenadeTimerObj;
    public GameObject explosion;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
    }

    private void Update()
    {
        if (startCountdown)
        {
            grenadeTimerObj.gameObject.SetActive(true);
            
            DrawCircle(50);
            grenadeTimer += Time.deltaTime;
            grenadeTimerObj.text.text = (Mathf.RoundToInt(explodeTime - grenadeTimer)+1).ToString();
            if (grenadeTimer >= explodeTime)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(grenadeTimerObj.gameObject);
                Destroy(gameObject);
                Collider[] cols = Physics.OverlapSphere(transform.position, radius);
                foreach (var col in cols)
                {
                    if (col.GetComponent<CarMovements>())
                    {
                        col.GetComponent<CarMovements>().ReduceLifeOnDamage(damage);
                        col.GetComponent<CarMovements>().ShowFloatingLostLife(10);
                    }
                }
                
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider)
        {
            lr.enabled = true;
            startCountdown = true;
        }
    }

    void DrawCircle(int steps)
    {
        lr.positionCount = steps;

        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circProgress = (float)currentStep / steps;
            float currentRad = circProgress * 2 * Mathf.PI;
            float xScaled = Mathf.Cos(currentRad);
            float yScaled = Mathf.Sin(currentRad);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 pos = transform.position + new Vector3(x, 0.5f, y);

            lr.SetPosition(currentStep, pos);

        }
    }
}
