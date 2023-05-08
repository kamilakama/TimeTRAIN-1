using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperTrigger : MonoBehaviour
{
    public Animator newspaperAnimator;
    public Transform playerHead;
    public float gazeDistance = 10f;

    private void Update()
    {
        // Cast a ray from the player's head to detect the newspaper object
        Ray gazeRay = new Ray(playerHead.position, playerHead.forward);
        RaycastHit hit;
        if (Physics.Raycast(gazeRay, out hit, gazeDistance))
        {
            // If the newspaper object is hit by the ray, trigger the animation
            if (hit.collider.gameObject == gameObject)
            {
                newspaperAnimator.SetTrigger("PlayAnimation");
            }
        }
    }
}
