using UnityEngine;
using System.Collections;

public class NewspaperAnimation : MonoBehaviour {

    public float lookTimeRequired = 3.0f; // how long player must look at the newspaper to trigger animation
    private float lookTimeElapsed = 0.0f; // time player has currently spent looking at the newspaper
    private bool animationTriggered = false; // flag indicating if the animation has been triggered
    private Animation newspaperAnimation; // reference to the newspaper animation component
    private Animator animator; // reference to the animator component

    void Awake() {
        // get the animation and animator components
        newspaperAnimation = GetComponent<Animation>();
        animator = GetComponent<Animator>();

        if (newspaperAnimation != null) {
            // disable animation on awake
            newspaperAnimation.Stop();
            newspaperAnimation.enabled = false;
        } else {
            Debug.LogWarning("NewspaperAnimation: Animation component not found!");
        }

        if (animator != null) {
            animator.enabled = false;
        } else {
            Debug.LogWarning("NewspaperAnimation: Animator component not found!");
        }
    }

    void Update() {
        if (animator == null || newspaperAnimation == null) {
            // animator or animation component not found
            return;
        }

        // check if player is looking at the newspaper
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f)); // ray from center of screen
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject && hit.collider.CompareTag("Newspaper")) {

            // increment look time
            lookTimeElapsed += Time.deltaTime;

            // check if player has looked long enough to trigger animation
            if (!animationTriggered && lookTimeElapsed >= lookTimeRequired) {
                // trigger animation
                animator.enabled = false;
                newspaperAnimation.enabled = true;
                newspaperAnimation.Play();
                animationTriggered = true;
            }
        }
        else {
            // reset look time if player is not looking at the newspaper
            lookTimeElapsed = 0.0f;
        }
    }
}
