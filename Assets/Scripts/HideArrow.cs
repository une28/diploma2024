using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideArrow : MonoBehaviour
{
    public GameObject arrow;
    public GameObject[] camArray;
    private MeshRenderer arrowMeshRenderer;
    private BoxCollider arrowBoxCollider;
    private MeshCollider arrowMeshCollider;

    void Start()
    {
        arrowMeshRenderer = arrow.GetComponent<MeshRenderer>();
        arrowBoxCollider = arrow.GetComponent<BoxCollider>();
        arrowMeshCollider = arrow.GetComponent<MeshCollider>();
    }

    void Update()
    {
        bool anyObjectActive = false;

        foreach (GameObject obj in camArray)
        {
            if (obj.activeSelf)
            {
                anyObjectActive = true;
                break;
            }
        }

        if (anyObjectActive)
        {
            arrowMeshRenderer.enabled = false;
            arrowBoxCollider.enabled = false; 
            arrowMeshCollider.enabled = false;
        }
        else
        {
            arrowMeshRenderer.enabled = true;
            arrowBoxCollider.enabled = true;
            arrowMeshCollider.enabled = true;
        }
    }
}
