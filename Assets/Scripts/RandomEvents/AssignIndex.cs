using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignIndex : MonoBehaviour
{
    [HideInInspector] public int index;

    public void SetIndex(int i)
    {
        index = i;
    }

    public void OnCheckBoxChange()
    {
        Debug.Log("check");
        ActivatedEvents.Instance.events[index].active = !ActivatedEvents.Instance.events[index].active;
    }
}
