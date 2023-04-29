using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguSpeakTest : MonoBehaviour

{
    public float lookTime = 2.0f; // The amount of time the player needs to look at the object to trigger the audio clip
    private bool isLooking = false; // Flag to track whether the player is looking at the object
    public float lookTimer = 0.0f; // Timer to track how long the player has been looking at the object
    private AudioSource audioSource;
    public AudioClip audioClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isLooking)
        {
            lookTimer += Time.deltaTime;

            if (lookTimer >= lookTime)
            {
                PlayAudioClip();
            }
        }
        else
        {
            lookTimer = 0.0f;
        }
    }

    private void PlayAudioClip()
    {
        audioSource.Play();
        lookTimer = 0.0f;
    }

    private void OnBecameVisible()
    {
        isLooking = true;
    }

    private void OnBecameInvisible()
    {
        isLooking = false;
        lookTimer = 0.0f;
    }
}
