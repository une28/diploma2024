using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCollider : MonoBehaviour
{
    public GameObject hiddenCollider;
    public GameObject cam;

    void Start()
    {
        
    }

    void Update()
    {
        Collider objCollider = hiddenCollider.GetComponent<Collider>();
        if (cam.activeSelf)
        {
            objCollider.enabled = false;
        }
        else
        {
            objCollider.enabled = true;
        }
    }
}
