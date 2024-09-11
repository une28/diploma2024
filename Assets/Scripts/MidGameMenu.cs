using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidGameMenu : MonoBehaviour
{
    public GameObject activeCam;
    public GameObject exitGame;
    public GameObject MidGameUI;
    private GameObject planeToTransform;
    public GameObject inventoryBackground;
    public GameObject inventorySlot;
    public GameObject inventorySlotSelection;
    public GameObject arrow;
    private GameObject[] objectsToHide;
    private Renderer objectRenderer;
    private Collider objectCollider;
    private Vector3 arrowScale;
    public GameObject menuButton;
    public GameObject continueButton;
    public GameObject soundButton;
    public ReadPaper[] paperArray;

    public void Start()
    {
        objectsToHide = new GameObject[] { inventoryBackground, inventorySlot, inventorySlotSelection };
        arrowScale = new Vector3(0.0178f, 0.0178f, 0.0178f);
    }

    void OnMouseDown()
    {
        if (gameObject == menuButton)
        {
            OnMenu(false);

            foreach (ReadPaper paper in paperArray) {
                paper.OnMouseDown();
            }
        } else
        {
            OnMenu(true);
        }
    }

    void OnMenu(bool check)
    {
        objectRenderer = menuButton.GetComponent<Renderer>();
        objectCollider = menuButton.GetComponent<Collider>();
        objectRenderer.enabled = check;
        objectCollider.enabled = check;
        MidGameUI.SetActive(!check);

        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(check);
        }

        soundButton.SetActive(!check);

        if (!check)
        {
            arrow.transform.localScale = new Vector3(0, 0, 0);
            ChangePlaneLocation(activeCam);
        } else
        {
            arrow.transform.localScale = arrowScale;
        }
    }

    private void ChangePlaneLocation(GameObject activeCamera)
    {
        ChangeLocation(activeCamera, MidGameUI, 0.4f, 0.3f, 2.4f);
        ChangeLocation(activeCamera, soundButton, 0.4f, 0.3f, 0.15f);
    }

    private void ChangeLocation(GameObject activeCamera, GameObject button, float distance, float screenOffsetX, float screenOffsetZ)
    {
        planeToTransform = button;
        Vector3 cameraPosition = activeCamera.transform.position;
        Quaternion cameraRotation = activeCamera.transform.rotation;
        Vector3 arrowPosition = cameraPosition + cameraRotation * Vector3.forward * distance;
        planeToTransform.transform.position = arrowPosition;
        planeToTransform.transform.rotation = cameraRotation;

        planeToTransform.transform.Rotate(Vector3.right, -90f);
        planeToTransform.transform.Translate(Vector3.right * screenOffsetX, Space.Self);
        planeToTransform.transform.Translate(Vector3.forward * screenOffsetZ, Space.Self);
    }
}
