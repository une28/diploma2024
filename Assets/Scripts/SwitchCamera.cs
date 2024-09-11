using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchCamera : MonoBehaviour
{
    public GameObject camActive; 
    public GameObject camToActivate;
    public GameObject hiddenCollider;
    private bool isCoroutineRunning = false;
    private bool isCoroutineRunning2 = false;
    private GameObject arrow;
    private GameObject canvas;
    private MeshCollider objCollider;
    private GameObject plane;
    private GameObject planeToTransform;
    private GameObject InventorySlotSelected;
    private SaveGame saveGameRef;

    public void OnMouseDown()
    {
        if (camActive.activeSelf)
        {
            if (isCoroutineRunning || isCoroutineRunning2)
            {
                return;
            }
            arrow = GameObject.Find("arrow");
            AudioSource audioSource = arrow.GetComponent<AudioSource>();
            
            audioSource.Play();

            StartCoroutine(DelayedExecution());
            StartCoroutine(ChangeAlpha());
        }
    }
    
    IEnumerator DelayedExecution()
    {
        isCoroutineRunning = true;

        yield return new WaitForSeconds(0.6f);

        if (camActive.activeSelf)
        {
            camToActivate.SetActive(true);

            camActive.SetActive(false);

            ChangePlaneLocation(camToActivate);

            saveGameRef = GameObject.Find("SaveGame").GetComponent<SaveGame>();
            saveGameRef.changeCamera(camToActivate, camActive);

            MidGameMenu midGameMenuRef = GameObject.Find("menuButton").GetComponent<MidGameMenu>();
            midGameMenuRef.activeCam = camToActivate;
        }
        isCoroutineRunning = false;
    }


    private void ChangePlaneLocation(GameObject activeCamera)
    {
        ChangeLocation(activeCamera, "darkScreenPlane", 0.4f,0.0f,0.0f);
        ChangeLocation(activeCamera, "arrow", 1f, 0.8f, 0.0f);
    }

    private void ChangeLocation(GameObject activeCamera, string objectName, float distance, float screenOffsetX, float screenOffsetZ)
    {
        planeToTransform = GameObject.Find(objectName);
        Vector3 cameraPosition = activeCamera.transform.position;
        Quaternion cameraRotation = activeCamera.transform.rotation;
        Vector3 arrowPosition = cameraPosition + cameraRotation * Vector3.forward * distance;
        planeToTransform.transform.position = arrowPosition;
        planeToTransform.transform.rotation = cameraRotation;

        planeToTransform.transform.Rotate(Vector3.right, -90f);
        planeToTransform.transform.Translate(Vector3.right * screenOffsetX, Space.Self);
        planeToTransform.transform.Translate(Vector3.forward * screenOffsetZ, Space.Self);
    }



    IEnumerator ChangeAlpha()
    {
        plane = GameObject.Find("darkScreenPlane");
        float duration = 0.3f;
        ChangeObjectAlpha(1f, duration);
        yield return new WaitForSeconds(duration);
        yield return new WaitForSeconds(duration);
        ChangeObjectAlpha(0f, duration);
        yield return new WaitForSeconds(duration);
    }

    void ChangeObjectAlpha(float targetAlpha, float duration)
    {
        Renderer renderer = plane.GetComponent<Renderer>();
        Color currentColor = renderer.material.color;
        Color targetColor = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
        StartCoroutine(ChangeAlphaOverTime(renderer, currentColor, targetColor, duration));
    }

    IEnumerator ChangeAlphaOverTime(Renderer renderer, Color startColor, Color targetColor, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            renderer.material.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        renderer.material.color = targetColor;
    }
}
