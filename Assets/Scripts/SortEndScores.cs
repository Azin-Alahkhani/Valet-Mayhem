using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SortEndScores : MonoBehaviour
{
    public List<GameObject> scorePictures;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            scorePictures.Add(transform.GetChild(i).gameObject);
        }


        scorePictures = scorePictures.OrderBy(x => x.GetComponent<EndScore>().score).ToList();
        
        for (int i = 0;i < scorePictures.Count; i++)
        {
            scorePictures[i].transform.SetSiblingIndex(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
