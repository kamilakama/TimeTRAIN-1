using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn1 : MonoBehaviour
{
    public float fadeTime = 2f;
    public float maxRadius = 10f;
    public float alpha = 1f;

    private Material material;
    private float startTime;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.color = new Color(0f, 0f, 0f, alpha);
        startTime = Time.time;
    }

    void Update()
    {
        float timeElapsed = Time.time - startTime;
        float t = Mathf.Clamp01(timeElapsed / fadeTime);
        float radius = Mathf.Lerp(0f, maxRadius, t);

        material.SetFloat("_Radius", radius);

        alpha = 1f - t;
        material.color = new Color(0f, 0f, 0f, alpha);

        if (alpha <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}