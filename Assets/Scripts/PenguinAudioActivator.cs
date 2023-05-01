using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinAudioActivator : MonoBehaviour
{
    public float timeToActivate = 2.0f; // Set the amount of time in seconds required to activate the audio
    float timeCountdown;
    GameObject hitObject;
    public AudioSource audioSource;

    private void Start()
    {
        timeCountdown = timeToActivate; // Set the countdown timer to the initial time
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.CompareTag("Penguin"))
            {
                timeCountdown -= Time.deltaTime;
                if (timeCountdown < 0)
                {
                    hitObject = hit.transform.gameObject;
                    audioSource.Play();
                    timeCountdown = timeToActivate;
                }
            }
            else
            {
                timeCountdown = timeToActivate;
            }
        }
    }
}
