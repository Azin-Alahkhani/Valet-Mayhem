
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePointParkingSpot : TimedEvent
{
    public Transform parkingSpot;
    public GameObject doublePointsIndicator;
    public Material goldMat;
    public ParticleSystem goldParticle;
    Material normalMat;
    private ParticleSystem particle;

    private void Awake()
    {
        ended.AddListener(TimerEnded);
    }

    // Start is called before the first frame update
    void Start()
    {
        

        int num = Random.Range(0, RandomEventController.Instance.parkingSpots.Count);
        parkingSpot = RandomEventController.Instance.parkingSpots[num].transform;

        normalMat = parkingSpot.GetChild(1).GetComponent<MeshRenderer>().material;
        particle = Instantiate(goldParticle, parkingSpot.transform);
       

        parkingSpot.GetChild(1).GetComponent<MeshRenderer>().material = goldMat;

        parkingSpot.GetComponentInChildren<ParkingSpot>().doublePoints = true;
    }

    void TimerEnded()
    {
        parkingSpot.GetComponentInChildren<ParkingSpot>().doublePoints = false;
        parkingSpot.GetChild(1).GetComponent<MeshRenderer>().material = normalMat;
        Destroy(particle.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        base.Timer();
    }
}
