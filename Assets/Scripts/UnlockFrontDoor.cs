using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockFrontDoor : MonoBehaviour
{
    public bool isOpen = false;
    public float rotateX = 0f;
    public GameObject activeCam;
    public GameObject doorLatch;
    public GameObject mainRoomDoor;
    public GameObject objSelf;
    public bool gameLoad = false;

    void Start()
    {
        objSelf = gameObject;
    }

    public void OnMouseDown()
    {
        if (activeCam.activeSelf && !isOpen || !isOpen && gameLoad)
        {
            isOpen = true;
            transform.Rotate(new Vector3(rotateX, 0f, 0f), Space.Self);
            doorLatch.SetActive(false);
            Collider collider = mainRoomDoor.GetComponent<Collider>();
            collider.enabled = false;

            collider = GetComponent<Collider>();
            collider.enabled = false;

            SaveGame saveGameRef = GameObject.Find("SaveGame").GetComponent<SaveGame>();
            saveGameRef.saveJSON(mainRoomDoor.name, "False");
        }
    }
}
