using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject lampLight;
    public GameObject lightBulb;

    void OnMouseDown()
    { 
        if (!lampLight.activeSelf)
        {
            lampLight.SetActive(true);
            float emissiveIntensity = 1.5f;
            Color emissiveColor = Color.white;
            Material material = lightBulb.GetComponent<Renderer>().material;
            material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);
        } else
        {
            lampLight.SetActive(false);
            float emissiveIntensity = 0f;
            Color emissiveColor = Color.white;
            Material material = lightBulb.GetComponent<Renderer>().material;
            material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);
        }
    }
}
