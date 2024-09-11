using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddToInventory : MonoBehaviour
{
    public CreateInventorySlots createInventorySlots;
    private GameObject[] slotArrayReference;
    private bool clicked = false;
    public float size = 0.5f;
    public float rotationX = 0f; 
    public float rotationY = 0f;
    public float rotationZ = 0f; 
    public float translateX = 0f;
    public float translateY = 0f;
    public float translateZ = 0f;
    public TextMeshProUGUI textToHide;
    private GameObject Canvas;
    private Renderer objectRenderer; 
    private Collider objectCollider;
    public GameObject activeCam;
    public GameObject coin;
    private GameObject inventoryObject = null;
    public GameObject selfObj = null;
    public bool isGameLoad = false;
    
    public void OnMouseDown()
    {
        Collider objCollider;
        
        if (!clicked && activeCam.activeSelf || !clicked && isGameLoad)
        {
            if (!isGameLoad)
            {
                selfObj = gameObject;
            }
            SaveGame saveGameRef = GameObject.Find("SaveGame").GetComponent<SaveGame>();
            int i = 0;
            slotArrayReference = createInventorySlots.GetArray();

            for (i = 0; i < slotArrayReference.Length; i++)
            {
                if (HasCoinChild(slotArrayReference[i]) && selfObj.name.Contains("coin"))
                {
                    objectRenderer = selfObj.GetComponent<Renderer>();
                    objectCollider = selfObj.GetComponent<Collider>();
                    objectRenderer.enabled = false;
                    objectCollider.enabled = false;

                    saveGameRef.saveJSON(selfObj.name, "False");

                    HideChildObjects(selfObj.transform);
                    break;
                }
                if (slotArrayReference[i].transform.childCount == 1 && slotArrayReference[i].transform.GetChild(0).name == "inventorySlotSelection" || slotArrayReference[i].transform.childCount == 0)
                {
                    if (selfObj.name.Contains("coin"))
                    {
                        inventoryObject = coin;

                        objectRenderer = selfObj.GetComponent<Renderer>();
                        objectCollider = selfObj.GetComponent<Collider>();
                        objectRenderer.enabled = false;
                        objectCollider.enabled = false;

                        saveGameRef.saveJSON(selfObj.name, "False");

                        HideChildObjects(selfObj.transform);
                    }
                    else
                    {
                        inventoryObject = selfObj;

                    }

                    inventoryObject.transform.SetParent(slotArrayReference[i].transform);

                    saveGameRef.saveJSON(inventoryObject.name, inventoryObject.transform.parent.name);

                    inventoryObject.transform.position = slotArrayReference[i].transform.position;
                    inventoryObject.transform.rotation = slotArrayReference[i].transform.rotation;

                    Vector3 currentScale = inventoryObject.transform.localScale;
                    inventoryObject.transform.localScale = new Vector3(currentScale.x * size, currentScale.y * size, currentScale.z * size);
                    inventoryObject.transform.Rotate(new Vector3(rotationX, rotationY, rotationZ), Space.Self);

                    inventoryObject.transform.Translate(new Vector3(translateX, translateY, translateZ), Space.Self);

                    float emissiveIntensity = 1.5f;
                    Color emissiveColor = Color.white;
                    Material material = inventoryObject.GetComponent<Renderer>().material;
                    material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);
                    ApplyEmissionIntensityToChildren(inventoryObject.transform, emissiveIntensity, emissiveColor);

                    inventoryObject.gameObject.layer = LayerMask.NameToLayer("UI");
                    objCollider = inventoryObject.GetComponent<Collider>();
                    objCollider.enabled = false;
                    ApplyUIChangeToChildren(inventoryObject.transform);


                    clicked = true;
                    break;
                }
            }

            if (selfObj.name.Contains("coin"))
            {
                int currentNumber;
                if (int.TryParse(textToHide.text, out currentNumber))
                {
                    int newNumber = currentNumber + 5;
                    textToHide.text = newNumber.ToString();
                }

                if (!textToHide.enabled)
                {
                    textToHide.enabled = true;

                    Canvas = GameObject.Find("Canvas");
                    Canvas.transform.SetParent(slotArrayReference[i].transform);
                    Vector3 currentPosition = Canvas.transform.position;
                    Canvas.transform.Translate(Vector3.down * 0.1f * i, Space.Self);
                }
            }
            selfObj.layer = LayerMask.NameToLayer("UI");

            objCollider = selfObj.GetComponent<Collider>();
            objCollider.enabled = false;
        }
    }

    bool HasCoinChild(GameObject parent)
    {
        int childCount = parent.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            GameObject child = parent.transform.GetChild(i).gameObject;

            if (child.name.Contains("coin"))
            {
                return true;
            }
        }

        return false;
    }

    void HideChildObjects(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Renderer childRenderer = child.GetComponent<Renderer>();
            if (childRenderer != null)
            {
                childRenderer.enabled = false;
            }
            HideChildObjects(child);
        }
    }

    void ApplyEmissionIntensityToChildren(Transform parent, float intensity, Color color)
    {
        foreach (Transform child in parent)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer != null)
            {
                Material material = renderer.material;
                material.SetColor("_EmissionColor", color * intensity);
            }
            ApplyEmissionIntensityToChildren(child, intensity, color);
        }
    }

    void ApplyUIChangeToChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.layer = LayerMask.NameToLayer("UI");
        }
    }
}
