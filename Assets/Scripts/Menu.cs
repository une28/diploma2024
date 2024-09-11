using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject MenuUI;
    public GameObject loadGame;
    public GameObject exitGame;
    public GameObject soundButton;
    public GameObject soundCross;
    private GameObject planeToTransform;
    public GameObject activeCamera;
    public GameObject arrow;
    private MeshRenderer objectRenderer;
    private Collider objectCollider;
    public GameObject MenuUICollider;
    public GameObject authorButton;

    public void Start()
    {
        MenuUI.SetActive(true);
        arrow.transform.localScale = new Vector3(0, 0, 0);
        activeCamera = gameObject;
        ChangePlaneLocation(activeCamera);
    }

    private void ChangePlaneLocation(GameObject activeCamera)
    {
        ChangeLocation(activeCamera, loadGame, 0.4f, -0.25f, 0.05f);
        ChangeLocation(activeCamera, exitGame, 0.4f, -0.25f, -0.05f);
        ChangeLocation(activeCamera, soundButton, 0.4f, 0.24f, 0.15f);
        ChangeLocation(activeCamera, soundCross, 0.4f, 0.27f, 0.15f);
        ChangeLocation(activeCamera, MenuUICollider, 0.4f, 0.0f, 0.0f);
        ChangeLocation(activeCamera, authorButton, 0.4f, 0.32f, 0.15f);
    }

    private void ChangeLocation(GameObject activeCamera,GameObject button, float distance, float screenOffsetX, float screenOffsetZ)
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
