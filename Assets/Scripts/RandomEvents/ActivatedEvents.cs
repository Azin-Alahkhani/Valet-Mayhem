using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedEvents : MonoBehaviour
{

    public static ActivatedEvents Instance;
    public List<EventListItem> events;

    public bool showPlayerIcon;
    public bool showPlayerNumber;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeIconValue()
    {
        showPlayerIcon = !showPlayerIcon;
    }

    public void ChangeNumberValue()
    {
        showPlayerNumber = !showPlayerNumber;
    }

}
