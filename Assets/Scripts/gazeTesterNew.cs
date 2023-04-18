using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gazeTesterNew : MonoBehaviour
{
    float timeStatic;
    public float timeCountdown;
    GameObject hitObject;
    public Material newM;
    public GameObject loadScreen;
    public Image circle;
    public Image circleFilling;
    public Text text;

    IEnumerator Loadlevel ( )
    {
        loadScreen.SetActive (true);
        AsyncOperation operation = SceneManager.LoadSceneAsync("TrainSCENE1");
        //operation.allowSceneActivation = false;
        //while (!operation.isDone)
        //{
        //    circleFilling.fillAmount = operation.progress;
        //    //text.text = operation.progress * 100 + "%";
        //    
        //}
        yield return null;
    }

    private void Start()
    {
        timeStatic = timeCountdown; //Set timeStatic (reset time) to same as the user defined countdown time in inspector at start
    }

    void FixedUpdate()
    {

        RaycastHit hit; //Raycaster
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)) //Raycast from transform.position (camera center point), forward direction from camera, reference to what is hit, distance Raycast should travel to detect objects hit
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow); //Raycast debug visible line for testing
            if (hit.transform.gameObject.tag == "Player") //Objects should have a tag and a RigidBody to be hit
            {
                Debug.Log("Hit Player"); //Debug line to make sure a tagged object is hit and reported

                timeCountdown = timeCountdown -= Time.deltaTime; //Start counting down

                if (timeCountdown < 0) //Once countdown has passed 0
                {
                    hitObject = hit.transform.gameObject; //Set the object hit as the hitObject variable
                    hitObject.GetComponent<Renderer>().material = newM; //Set the hitObject material

                    timeCountdown = timeStatic; //Reset the timeCountdown
                }
            }

            if (hit.transform.gameObject.tag == "LoadLevel") //Objects should have a tag and a RigidBody to be hit
            {
                Debug.Log("Trigger Load"); //Debug line to make sure a tagged object is hit and reported

                timeCountdown = timeCountdown -= Time.deltaTime; //Start counting down
                //Debug.Log("Loading Level " + hit.transform.gameObject.GetComponent<startgame>().levelNumber);

                circle.gameObject.SetActive(true);

                if (timeCountdown < 0) //Once countdown has passed 0
                {
                    hitObject = hit.transform.gameObject; //Set the object hit as the hitObject variable
                    StartCoroutine(Loadlevel());
                    //hitObject.GetComponent<startgame>().startGame(); //Set the hitObject material
                    timeCountdown = timeStatic; //Reset the timeCountdown
                }
            else if (hit.transform.gameObject == null)
            {
                timeCountdown = timeStatic;
                circle.gameObject.SetActive(false);

                Debug.Log("Did Hit"); //Debug line for all hit objects
                Debug.Log(hit.transform.gameObject.name);
            }
            }

            

            else
            {
                timeCountdown = timeStatic;
                circle.gameObject.SetActive(false);

                Debug.Log("Gazing at the air");
            }
        }
    }
}