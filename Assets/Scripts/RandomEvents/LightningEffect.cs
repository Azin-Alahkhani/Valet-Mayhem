using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEffect : MonoBehaviour
{
    [HideInInspector] public float destroyTime;
    Transform sparkPoints;
    float timer;

    // Start is called before the first frame update
    void Start()
    {

        sparkPoints = GetComponent<NewCarMovement>().sparkPoints;

        if (GetComponent<NewCarMovement>())
        {
            GetComponent<NewCarMovement>().maxSpeed *= 5;
        }

        Destroy(this, destroyTime);
    }

    

    private void OnDestroy()
    {
        if (GetComponent<NewCarMovement>())
        {
            GetComponent<NewCarMovement>().maxSpeed /= 5;
        }
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0.5f)
        {
            int randomNum = Random.Range(0, sparkPoints.childCount);
            ParticleSystem spark = sparkPoints.GetChild(randomNum).GetComponent<ParticleSystem>();
            spark.Play();
            timer = 0;
        }

    }
}
