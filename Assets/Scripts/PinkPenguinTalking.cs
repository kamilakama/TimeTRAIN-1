using UnityEngine;

public class PinkPenguinTalking : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;
    private bool hasCollided = false; // Declare the variable at the class level

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    void Update()
    {
        if (hasCollided && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // OnTriggerStay is called every frame while the collider is colliding with another collider
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasCollided = true; // Set the variable to true when the collider is colliding with the player
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasCollided = false; // Set the variable to false when the collider is no longer colliding with the player
        }
    }
}
