using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEvent : StrikeEvent
{

    public float lightningEffectDuration;
    LineRenderer lr;
    public ParticleSystem lightning;


    // Start is called before the first frame update
    void Start()
    {

        //lr = GetComponent<LineRenderer>();

        int num = Random.Range(0, RandomEventController.Instance.drivableCars.Count);
        RandomEventController.Instance.drivableCars[num].AddComponent<LightningEffect>();
        RandomEventController.Instance.drivableCars[num].GetComponent<LightningEffect>().destroyTime = lightningEffectDuration;

        //lr.SetPosition(1, RandomEventController.Instance.drivableCars[num].transform.position);

        
        ParticleSystem strike = Instantiate(lightning, RandomEventController.Instance.drivableCars[num].transform.position, Quaternion.identity);
        Destroy(strike, 1f);
        Destroy(gameObject, 1.3f);
    }


}
