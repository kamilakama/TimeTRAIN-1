using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Kama : MonoBehaviour
{
    private Color currentColor = new Color(0, 0, 0, 0); // Default starting color: black and fully transparent
    private Color targetColor = new Color(0, 0, 0, 0);  // Target color: black and fully transparent
    private Color deltaColor = new Color(0, 0, 0, 0);   // Speed of color change per second
    private bool fadeOverlay = false;

    private static Material fadeMaterial = null;
    private static int fadeMaterialColorID = -1;

    void OnEnable()
    {
        // Initialize the fade material if not already done
        if (fadeMaterial == null)
        {
            fadeMaterial = new Material(Shader.Find("Unlit/Color")); // Use a basic shader for fading
            fadeMaterialColorID = Shader.PropertyToID("_Color");
        }

        // Reset the fade state
        currentColor = new Color(0, 0, 0, 0);
        targetColor = new Color(0, 0, 0, 0);
        deltaColor = new Color(0, 0, 0, 0);
    }

    void OnDisable()
    {
        // Clean up resources if needed (optional in this case)
    }

    public static void StartFade(Color newColor, float duration, bool fadeOverlay = false)
    {
        SteamVR_Fade.Start(newColor, duration, fadeOverlay); // Use SteamVR_Fade's built-in functionality
    }

    public static void ViewFade(Color newColor, float duration)
    {
        SteamVR_Fade.View(newColor, duration); // Modern API call to handle fading
    }

    public void OnStartFade(Color newColor, float duration, bool fadeOverlay)
    {
        this.fadeOverlay = fadeOverlay;

        if (duration > 0.0f)
        {
            targetColor = newColor;
            deltaColor = (targetColor - currentColor) / duration;
        }
        else
        {
            currentColor = newColor;
        }
    }

    void OnPostRender()
    {
        // Update the fade effect
        if (currentColor != targetColor)
        {
            // Check if the fade is nearly complete
            if (Mathf.Abs(currentColor.a - targetColor.a) < Mathf.Abs(deltaColor.a) * Time.deltaTime)
            {
                currentColor = targetColor;
                deltaColor = new Color(0, 0, 0, 0);
            }
            else
            {
                currentColor += deltaColor * Time.deltaTime;
            }

            // Handle overlay fading
            if (fadeOverlay)
            {
                var overlay = SteamVR_Overlay.instance;
                if (overlay != null)
                {
                    overlay.alpha = 1.0f - currentColor.a;
                }
            }
        }

        // Render the fade effect using the material
        if (currentColor.a > 0 && fadeMaterial)
        {
            fadeMaterial.SetColor(fadeMaterialColorID, currentColor);
            fadeMaterial.SetPass(0);
            GL.PushMatrix();
            GL.LoadOrtho();
            GL.Begin(GL.QUADS);

            GL.Vertex3(0, 0, 0);
            GL.Vertex3(1, 0, 0);
            GL.Vertex3(1, 1, 0);
            GL.Vertex3(0, 1, 0);

            GL.End();
            GL.PopMatrix();
        }
    }

#if UNITY_EDITOR
    // Test fading functionality in the editor using the spacebar
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartFade(Color.black, 1.0f, false); // Fade to black in 1 second
            StartCoroutine(ResetFade());
        }
    }

    private IEnumerator ResetFade()
    {
        yield return new WaitForSeconds(1.0f);
        StartFade(Color.clear, 1.0f, false); // Fade back to clear in 1 second
    }
#endif
}
