using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class gazeontrain : MonoBehaviour
{
    [SerializeField] float timeToTrigger = 3f;
    [SerializeField] Material newMaterial;
    [SerializeField] Material invisibleMaterial;
    [SerializeField] GameObject loadScreen;
    [SerializeField] Image circle;
    [SerializeField] Image circleFilling;
    [SerializeField] Text text;
    [SerializeField] GameObject newspaperObject;
    [SerializeField] GameObject foxObject;

    private float timeLooking = 0f;
    private GameObject hitObject;

    private void Start()
    {
        circle.gameObject.SetActive(false);
        timeLooking = 0f;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);

            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Hit Player");

                circle.gameObject.SetActive(false);
                timeLooking = 0f;
            }
            else if (hit.transform.gameObject == newspaperObject || hit.transform.gameObject == foxObject)
            {
                Debug.Log("Gazing at interactable object");

                timeLooking += Time.deltaTime;

                circle.gameObject.SetActive(true);
                circleFilling.fillAmount = timeLooking / timeToTrigger;

                if (timeLooking >= timeToTrigger)
                {
                    if (hit.transform.gameObject == newspaperObject)
                    {
                        Debug.Log("Newspaper interaction triggered");

                        StartCoroutine(ShowLoadingCircle());

                        Renderer newspaperRenderer = newspaperObject.GetComponent<Renderer>();
                        newspaperRenderer.material = invisibleMaterial;
                    }
                    else if (hit.transform.gameObject == foxObject)
                    {
                        Debug.Log("Fox interaction triggered");

                        Renderer foxRenderer = foxObject.GetComponent<Renderer>();
                        foxRenderer.material = newMaterial;
                    }

                    circle.gameObject.SetActive(false);
                    timeLooking = 0f;
                }
            }
            else
            {
                circle.gameObject.SetActive(false);
                timeLooking = 0f;

                Debug.Log("Gazing at the air");
            }
        }
    }

    private IEnumerator ShowLoadingCircle()
    {
        circleFilling.fillAmount = 0f;

        yield return new WaitForSeconds(0.5f);

        circleFilling.fillAmount = 0.25f;

        yield return new WaitForSeconds(0.5f);

        circleFilling.fillAmount = 0.5f;

        yield return new WaitForSeconds(0.5f);

        circleFilling.fillAmount = 0.75f;

        yield return new WaitForSeconds(0.5f);

        circleFilling.fillAmount = 1f;

        yield return new WaitForSeconds(1f);

        loadScreen.SetActive(true);
    }
}
