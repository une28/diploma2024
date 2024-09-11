using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateInventorySlots : MonoBehaviour
{
    public GameObject quadrantPrefab;
    private GameObject clone;
    public GameObject InventorySlotSelected;
    public GameObject[] slotArray = new GameObject[4];
    public GameObject[] childObjects = new GameObject[5];

    void Start()
    {
        if (gameObject.name == "inventorySlot")
        {
            for (int i = 0; i < 4; i++)
            {
                clone = Instantiate(quadrantPrefab, new Vector3(quadrantPrefab.transform.position.x, quadrantPrefab.transform.position.y, quadrantPrefab.transform.position.z - (i * 0.1f)), quadrantPrefab.transform.rotation);
                slotArray[i] = clone;
            }

            for (int i = 0; i < 4; i++)
            {
                slotArray[i].transform.parent = quadrantPrefab.transform;
            }

            MeshRenderer meshRenderer = quadrantPrefab.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }

            MeshCollider meshCollider = quadrantPrefab.GetComponent<MeshCollider>();
            meshCollider.enabled = false;
        }
    }

    public GameObject[] GetArray()
    {
        return slotArray;
    }

    void OnMouseDown()
    {
        InventorySlotSelected.transform.parent = transform;
        InventorySlotSelected.transform.position = transform.position;
        InventorySlotSelected.transform.rotation = transform.rotation;
    }
}
