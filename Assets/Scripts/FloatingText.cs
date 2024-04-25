using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    private float destroyTime = 1f;
    private Vector3 changeScale = new Vector3(-0.1f, 0, 0);
    TextMeshPro textMesh;

    public AudioSource audioSource;
        void Start()
    {
        if(audioSource != null)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        }

        Destroy(gameObject, destroyTime);
        textMesh = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.fontSize -= Time.deltaTime * 80;
    }
}
