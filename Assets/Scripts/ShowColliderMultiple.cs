using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowColliderMultiple : MonoBehaviour
{
    public GameObject hiddenCollider;
    public GameObject[] cam;

    void Start()
    {

    }

    void Update()
    {
        Collider objCollider = hiddenCollider.GetComponent<Collider>();

        int count = 0;

        foreach (GameObject obj in cam)
        {
            if (obj.activeSelf)
            {
                objCollider.enabled = false;
                break;
            }
            else
            {
                count++;
            }
        }

        if (count == cam.Length) {
            objCollider.enabled = true;
        }
    }
}
