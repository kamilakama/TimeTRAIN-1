using UnityEngine;

public class FadeIn : MonoBehaviour
{
    // The duration of the fade-in effect in seconds
    public float fadeTime = 2f;

    // The maximum radius of the sphere
    public float maxRadius = 10f;

    // The material that the sphere is using
    private Material material;

    // The time that the script started running
    private float startTime;

    // Called when the script starts running
    void Start()
    {
        // Get the material component of the object that this script is attached to
        material = GetComponent<Renderer>().material;

        // Set the color of the material to black
        material.color = Color.black;

        // Set the alpha value of the material to 1
        material.SetFloat("_Alpha", 1f);

        // Record the start time of the script
        startTime = Time.time;
    }

    // Called every frame
    void Update()
    {
        // Calculate the amount of time that has elapsed since the script started running
        float timeElapsed = Time.time - startTime;

        // Calculate the alpha value based on the elapsed time and the fade time
        float alpha = Mathf.Clamp01(1f - timeElapsed / fadeTime);

        // Calculate the radius of the sphere based on the alpha value and the maximum radius
        float radius = Mathf.Lerp(0f, maxRadius, alpha);

        // Set the "_Radius" property of the material to the calculated radius
        material.SetFloat("_Radius", radius);

        // If the alpha value is 0 or less, deactivate the game object
        if (alpha <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
