using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkyboxOnCollision : MonoBehaviour
{
    public string objectTag; // The tag of the object that triggers the skybox change
    public Material newSkybox; // The new skybox material to use

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(objectTag))
        {
            Debug.Log("Collided with object with tag: " + objectTag);
            RenderSettings.skybox = newSkybox;
            Debug.Log("Skybox material set to: " + newSkybox.name);
        }
    }
}
