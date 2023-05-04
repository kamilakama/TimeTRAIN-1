using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeCamera : MonoBehaviour
{
    [Range (0,1)] public float alpha;
    public Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        var tempColor = image.color;
        tempColor.a = alpha;
        image.color = tempColor;
    }
}
