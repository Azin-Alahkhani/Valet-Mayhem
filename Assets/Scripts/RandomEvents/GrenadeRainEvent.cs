using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeRainEvent : TimedEvent
{

    public GameObject grenade;
    public float grenadeSpawnInterval;
    float grenadeSpawnTimer;
    public float minForce;
    public float maxForce;
    public GrenadeTimer grenadteTimerObj;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        grenadeSpawnTimer += Time.deltaTime;
        if(grenadeSpawnTimer >= grenadeSpawnInterval)
        {
            int randomIndex = Random.Range(0, transform.childCount);
            var randomAngle = Quaternion.Euler(transform.GetChild(randomIndex).localEulerAngles.x,
                Random.Range(transform.GetChild(randomIndex).localEulerAngles.y - 30, transform.GetChild(randomIndex).localEulerAngles.y + 30), transform.GetChild(randomIndex).localEulerAngles.z);
            GameObject nade = Instantiate(grenade, transform.GetChild(randomIndex).position, randomAngle);
            GrenadeTimer gTimer = Instantiate(grenadteTimerObj, nade.transform.position, Quaternion.Euler(90,0,0));
            nade.GetComponent<Grenade>().grenadeTimerObj = gTimer;
            gTimer.grenadeToFollow = nade.transform;
            float force = Random.Range(minForce, maxForce);
            nade.GetComponent<Rigidbody>().AddForce(nade.transform.right * force, ForceMode.Impulse);
            grenadeSpawnTimer = 0;
        }

        Timer();
    }
}
