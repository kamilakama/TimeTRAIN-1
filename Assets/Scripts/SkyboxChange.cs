using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChange : MonoBehaviour
{
    public Material Skybox1;
    public Material otherSkyBox;
 
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Train") // Checking if it is the train
        {
            if (RenderSettings.skybox != otherSkyBox) // if the skybox is not the one we want
            {
                RenderSettings.skybox = otherSkyBox; // change it to the desired skybox
            }
        }
    }
}
