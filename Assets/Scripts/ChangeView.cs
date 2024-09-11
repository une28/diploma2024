using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ChangeView : MonoBehaviour
{
    private Dictionary<GameObject, int> objectValues = new Dictionary<GameObject, int>();
    private bool isCoroutineRunning = false;
    private bool isCoroutineRunning2 = false;
    public GameObject[] camArray;
    private int[] positions;
    public GameObject activeCam;
    private GameObject arrow;
    private GameObject canvas;
    private Collider objCollider;
    private GameObject planeToTransform;
    public GameObject parent;
    private GameObject InventorySlotSelected;
    private SaveGame saveGameRef;
    private Collider objectCollider;

    void Start() {
        positions = new int[] {0,1,1,2,2,3,3,1,6,4,5,30,21,6,41,51,40,50,20,31,31,32,33,34,400,401,402,403,35,36,404};

        for (int i = 0; i<camArray.Length; i++ )
            objectValues.Add(camArray[i], positions[i]);
    }

    void OnMouseDown()
    {
        if (isCoroutineRunning || isCoroutineRunning2)
        {
            return;
        }

        PlaySound playSoundRef = GetComponent<PlaySound>();
        playSoundRef.playSound();

        StartCoroutine(DelayedExecution());
        StartCoroutine(ChangeAlpha());
    }

    IEnumerator DelayedExecution()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(0.6f);

        foreach (GameObject obj in camArray)
        {
            if (obj.activeSelf)
            {
                activeCam = obj;
                break; 
            }
        }
        int targetNumber = objectValues[activeCam] - 1;

        GameObject camToActivate = GetObjectByValue(targetNumber);
        GameObject camToSend;

        if (activeCam == camArray[0])
        {
            camArray[7].SetActive(true);

            activeCam.SetActive(false);

            ChangePlaneLocation(camArray[7]);

            camToSend = camArray[7];

        } 
        else
        if (activeCam == camArray[3] || activeCam == camArray[4])
        {
            camArray[7].SetActive(true);

            activeCam.SetActive(false);

            ChangePlaneLocation(camArray[7]);

            camToSend = camArray[7];
        }
        else
        if (activeCam == camArray[5] || activeCam == camArray[6])
        {
            camArray[4].SetActive(true);

            activeCam.SetActive(false);

            ChangePlaneLocation(camArray[4]);

            camToSend = camArray[4];
        }
        else
        if (activeCam == camArray[21])
        {
            camArray[20].SetActive(true);

            activeCam.SetActive(false);

            ChangePlaneLocation(camArray[20]);

            camToSend = camArray[20];
        }
        else
        if (activeCam == camArray[23])
        {
            camArray[11].SetActive(true);

            activeCam.SetActive(false);

            ChangePlaneLocation(camArray[11]);

            camToSend = camArray[11];
        }
        else
        if(activeCam == camArray[24])
        {
            camArray[16].SetActive(true);

            activeCam.SetActive(false);

            ChangePlaneLocation(camArray[16]);

            camToSend = camArray[16];   
        }
        else
        if (activeCam == camArray[28])
        {
            camArray[20].SetActive(true);

            activeCam.SetActive(false);

            ChangePlaneLocation(camArray[20]);

            camToSend = camArray[20];
        }
        else
        if (activeCam == camArray[30])
        {
            camArray[24].SetActive(true);

            activeCam.SetActive(false);

            ChangePlaneLocation(camArray[24]);

            camToSend = camArray[24];
        }
        else
        if (activeCam == camArray[9])
        {
            camArray[7].SetActive(true);

            activeCam.SetActive(false);

            ChangePlaneLocation(camArray[7]);

            camToSend = camArray[7];
        }
        else
        {
            camToActivate.SetActive(true);

            activeCam.SetActive(false);

            ChangePlaneLocation(camToActivate);

            camToSend = camToActivate;
        }

        saveGameRef = GameObject.Find("SaveGame").GetComponent<SaveGame>();
        saveGameRef.changeCamera(camToSend, activeCam); 

        MidGameMenu midGameMenuRef = GameObject.Find("menuButton").GetComponent<MidGameMenu>();
        midGameMenuRef.activeCam = camToSend;

        isCoroutineRunning = false; 
    }


    private void ChangePlaneLocation(GameObject activeCamera)
    {
        ChangeLocation(activeCamera, "darkScreenPlane", 0.4f, 0.0f, 0.0f);
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

    private GameObject GetObjectByValue(int value)
    {
        foreach (KeyValuePair<GameObject, int> pair in objectValues)
        {
            if (pair.Value == value)
            {
                return pair.Key;
            }
        }
        return null; 
    }

    private GameObject plane;

    IEnumerator ChangeAlpha()
    {
        isCoroutineRunning2 = true;
        plane = GameObject.Find("darkScreenPlane");
        float duration = 0.3f;
        ChangeObjectAlpha(1f, duration);
        yield return new WaitForSeconds(duration);
        yield return new WaitForSeconds(duration);
        ChangeObjectAlpha(0f, duration);
        yield return new WaitForSeconds(duration);
        isCoroutineRunning2 = false;
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
