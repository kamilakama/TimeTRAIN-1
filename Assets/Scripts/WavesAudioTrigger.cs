using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesAudioTrigger : MonoBehaviour
{
    public AudioSource audioSource;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Train"))
        {
            audioSource.Play();
        }
    }
}