using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventListItem
{
    public string eventName;
    public RandomEvent randomEvent;
    public bool active = true;
}


public class RandomEventController : MonoBehaviour
{

    public static RandomEventController Instance;


    public float timeToStart;


    public float minTimeBetweenEvents;
    public float maxTimeBetweenEvents;

    float betweenEventTime;
    float betweenEventTimer;



    float startTimer;

    public List<EventListItem> events;

    List<RandomEvent> activeEvents = new List<RandomEvent>();

    public List<GameObject> parkingSpots;

    public List<GameObject> drivableCars;

    public List<Transform> parkingSpotsParents;

    bool startEvents;

    [Header("Bounds")]
    public Transform topLeft;
    public Transform topRight;
    public Transform bottomLeft;
    public Transform bottomRight;


    private void Awake()
    {
        Instance = this;
        foreach (var spots in parkingSpotsParents)
        {
            for (var i = 0; i < spots.transform.childCount; i++)
            {
                if (spots.GetChild(i).gameObject.activeSelf)
                {
                    parkingSpots.Add(spots.GetChild(i).gameObject);
                }
                
            }
        }


    }

    // Start is called before the first frame update
    void Start()
    {

        int index = 0;
        //If scene is loaded through level select
        if (ActivatedEvents.Instance)
        {
            foreach (var e in ActivatedEvents.Instance.events)
            {
                if (e.active)
                {
                    activeEvents.Add(e.randomEvent);
                }
                index++;
            }

            
            foreach (var e in events)
            {
                if (!activeEvents.Contains(e.randomEvent))
                {
                    e.active = false;
                }
            }
        }
        //If scene is loaded through unity
        else
        {
            foreach (var e in events)
            {
                if (e.active)
                {
                    activeEvents.Add(e.randomEvent);
                }
            }
        }



    }

    // Update is called once per frame
    void Update()
    {
        if(activeEvents.Count > 0)
        {
            PreEventTime();
            if (startEvents)
            {
                StartEvents();
            }
        }


    }

    void PickEvent()
    {
        if(activeEvents.Count > 0)
        {
            //Debug.Log("spawned event");
            int num = Random.Range(0, activeEvents.Count);
            Instantiate(activeEvents[num]);
        }

    }

    void StartEvents()
    {
        betweenEventTimer += Time.deltaTime;
        if(betweenEventTimer >= betweenEventTime)
        {
            PickEvent();
            betweenEventTime = Random.Range(minTimeBetweenEvents, maxTimeBetweenEvents);
            //Debug.Log("Next Event In: " + betweenEventTime.ToString());
            betweenEventTimer = 0;
        }
    }

    void PreEventTime()
    {
        if(activeEvents.Count > 0)
        {
            if (startTimer < timeToStart)
            {

                startTimer += Time.deltaTime;
                if (startTimer >= timeToStart)
                {
                    betweenEventTime = Random.Range(minTimeBetweenEvents, maxTimeBetweenEvents);
                    startEvents = true;
                    //Debug.Log("Events Starting");
                    //Debug.Log("Next Event In: " + betweenEventTime.ToString());
                    //Debug.Log("events");
                }
            }
        }

    }
}
