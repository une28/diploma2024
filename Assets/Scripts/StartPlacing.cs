using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartPlacing : MonoBehaviour
{
    private bool check = false;
    GameObject plane;
    GameObject planeToTransform;
    GameObject activeCamera;
    private GameObject InventorySlotSelected;
    public TextMeshProUGUI textToHide;
    public GameObject quadrantPrefab;
    private GameObject clone;
    private GameObject Canvas;
    private GameObject inventorySlot;
    private GameObject[] slotArray = new GameObject[4];
    private SaveGame saveGameRef;

    void Start()
    {
        activeCamera = gameObject;

        if (!check) {
            planeToTransform = GameObject.Find("inventory");
            Vector3 scale = planeToTransform.transform.localScale;
            scale.x = -scale.x;
            planeToTransform.transform.localScale = scale;

            ChangePlaneLocation(activeCamera);

            Canvas = GameObject.Find("Canvas");
            inventorySlot = GameObject.Find("inventorySlot");
            Canvas.transform.SetParent(inventorySlot.transform);

            planeToTransform = GameObject.Find("darkScreenPlane");
            inventorySlot.transform.SetParent(planeToTransform.transform);

            saveGameRef = GameObject.Find("SaveGame").GetComponent<SaveGame>();
            saveGameRef.startLoading();

            check = true;
        }
    }

    private void CreateClones()
    {
        slotArray[0] = quadrantPrefab;
        Destroy(this);
        for (int i = 0; i < 3; i++)
        {
            clone = Instantiate(quadrantPrefab, new Vector3(i * 0.01f, 0, 0), Quaternion.identity);
            slotArray[i] = clone;
        }

        for (int i = 0; i < 3; i++)
        {
            slotArray[i].transform.parent = quadrantPrefab.transform;
        }
    }

    private void ChangePlaneLocation(GameObject activeCamera)
    {
        ChangeLocation(activeCamera, "darkScreenPlane", 0.4f, 0.0f, 0.0f);
        ChangeLocation(activeCamera, "inventory", 0.5f, -0.44f, 0.1f);
        ChangeLocation(activeCamera, "inventorySlot", 0.42f, -0.365f, 0.15f);
        ChangeLocation(activeCamera, "inventorySlotSelection", 0.42f, -0.365f, 0.15f);
        ChangeLocation(activeCamera, "Canvas", 0.4f, -0.243f, 0.073f);
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

        if (objectName == "Canvas" || objectName == "CanvasTextBackground")
        {
            planeToTransform.transform.Translate(Vector3.right * screenOffsetX, Space.Self);
            planeToTransform.transform.Translate(Vector3.up * screenOffsetZ, Space.Self);

        } 
        else
        {
            planeToTransform.transform.Rotate(Vector3.right, -90f);
            planeToTransform.transform.Translate(Vector3.right * screenOffsetX, Space.Self);
            planeToTransform.transform.Translate(Vector3.forward * screenOffsetZ, Space.Self);
        }
    }


    void Update()
    {
        
    }
}
