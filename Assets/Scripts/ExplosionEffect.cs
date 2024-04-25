using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    
    private AudioSource crashAudio;

    void Start()
    {
        crashAudio = GetComponent<AudioSource>();
        crashAudio.mute = false;
        //Debug.Log("explosion effect instantiated");
        gameObject.name = "explosion";
        
        crashAudio.Play();
        Destroy(gameObject, 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreUIController.Instance.end)
        {
            crashAudio.mute = true;
        }
    }
}
