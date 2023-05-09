using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOUTcamera : MonoBehaviour
{
    [Range(0, 1)] public float alpha;
    public Image image;

    void Start()
    {
        StartCoroutine(FadeOutAfterTime(15f)); // Start coroutine to fade out after 3 minutes and 15 seconds (195 seconds)
    }

    IEnumerator FadeOutAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Fade out the scene
        while (alpha < 1)
        {
            alpha += Time.deltaTime / 2; // Adjust the fade speed by changing the divisor (e.g. divide by 4 for a slower fade)
            var tempColor = image.color;
            tempColor.a = alpha;
            image.color = tempColor;
            yield return null;
        }

        // Load a new scene or quit the game
        // In this example, we'll just quit the game
        Application.Quit();
    }
}
