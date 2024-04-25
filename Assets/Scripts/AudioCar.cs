using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCar : MonoBehaviour
{




    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;



    public Collider soundTrigger;

    public bool soundFinish = false;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!soundFinish)
        {
            if (other.gameObject.CompareTag("Player"))
            {



                audioSource.PlayOneShot(audioSource.clip, volume);

                soundFinish = true;
            }
        }

       
    }









}
