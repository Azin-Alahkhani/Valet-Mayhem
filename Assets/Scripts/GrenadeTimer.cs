using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GrenadeTimer : MonoBehaviour
{
    [HideInInspector]
    public Transform grenadeToFollow;

    [HideInInspector]
    public TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(grenadeToFollow.position.x, grenadeToFollow.position.y + 0.5f, grenadeToFollow.position.z);
    }
}
