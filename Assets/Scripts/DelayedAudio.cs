using UnityEngine;

public class DelayedAudio : MonoBehaviour
{
    public float delayTime = 90f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("PlayDelayedAudio", delayTime);
    }

    void PlayDelayedAudio()
    {
        audioSource.Play();
    }
}