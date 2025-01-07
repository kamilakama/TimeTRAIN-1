using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;

public class gazeTesterNew : MonoBehaviour
{
    float timeStatic;
    float timeLooking;
    public float timeCountdown;
    GameObject hitObject;
    public Material newM;
    public GameObject loadScreen;
    public Image circle;
    public Image circleFilling;
    public Text text;
    public GameObject penguinObject;
    public float timeUntilAudio;
    public AudioClip penguinAudio;

    bool hasPlayedAudio = false;

    IEnumerator Loadlevel()
    {
        loadScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync("TrainSCENE1");
        yield return null;
    }

    private void Start()
    {
        timeStatic = timeCountdown;
        timeLooking = 0f; // Initialize time looking at 0
        hasPlayedAudio = false; // Set hasPlayedAudio to false initially
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.transform.gameObject.tag == "Player")
            {
                Debug.Log("Hit Player");

                timeCountdown = timeCountdown -= Time.deltaTime;

                if (timeCountdown < 0)
                {
                    hitObject = hit.transform.gameObject;
                    hitObject.GetComponent<Renderer>().material = newM;

                    timeCountdown = timeStatic;
                }
            }

            else if (hit.transform.gameObject.tag == "Penguin")
            {
                Debug.Log("Gazing at penguin");

                timeLooking += Time.deltaTime;

                if (timeLooking >= timeUntilAudio && !hasPlayedAudio)
                {
                    Debug.Log("Played Penguin Audio");
                    AudioSource penguinAudioSource = penguinObject.GetComponent<AudioSource>();
                    penguinAudioSource.PlayOneShot(penguinAudio);
                    hasPlayedAudio = true;
                    timeLooking = 0f;
                }
                else if (hasPlayedAudio)
                {
                    timeLooking = 0f;
                }
            }

            else if (hit.transform.gameObject.tag == "LoadLevel")
            {
                Debug.Log("Trigger Load");

                timeCountdown = timeCountdown -= Time.deltaTime;

                circle.gameObject.SetActive(true);

                if (timeCountdown < 0)
                {
                    hitObject = hit.transform.gameObject;
                    StartCoroutine(Loadlevel());
                    timeCountdown = timeStatic;
                }
            }
            else
            {
                timeCountdown = timeStatic;
                circle.gameObject.SetActive(false);
                timeLooking = 0f;

                Debug.Log("Gazing at the air");
            }
        }
    }
}
