using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddRandomEvents : MonoBehaviour
{
    public GameObject eventListPrefab;
    public Transform contentTransform;
    int index;
    bool disable;
    public TextMeshProUGUI buttonText;


    // Start is called before the first frame update
    void Start()
    {

        foreach (var e in ActivatedEvents.Instance.events)
        {
            e.active = true;
            GameObject eList = Instantiate(eventListPrefab, contentTransform);
            GetComponent<EventsSelection>().selectionObjects.Add(eList);
            eList.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.eventName;
            eList.GetComponent<AssignIndex>().SetIndex(index);
            index++;
        }


    }

    public void ChangeDisable()
    {
        disable = !disable;
        ChangeAllActiveState();
    }

    void ChangeAllActiveState()
    {
        if (disable)
        {
            int index = 0;
            foreach (var e in ActivatedEvents.Instance.events)
            {
                if (e.active == disable)
                {
                    e.active = true;
                }
                
                contentTransform.GetChild(index).GetChild(1).GetComponent<Toggle>().isOn = false;
                index++;


            }
            buttonText.text = "Enable All";
        }
        else
        {
            int index = 0;
            foreach (var e in ActivatedEvents.Instance.events)
            {

                if (e.active == disable)
                {
                    e.active = false;
                }
                contentTransform.GetChild(index).GetChild(1).GetComponent<Toggle>().isOn = true;
                index++;


            }
            buttonText.text = "Disable All";
        }

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
