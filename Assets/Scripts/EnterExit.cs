using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterExit : MonoBehaviour
{
    public GameObject[] plankArray;
    public GameObject activeCam;
    public GameObject blockingCollider;
    public GameObject crowbar;
    public GameObject deletedInventory;
    public GameObject finalExitBlock;
    public bool blocking = false;

    void Update()
    {
        if (activeCam.activeSelf && !blocking)
        {
            bool allActive = true;

            foreach (GameObject obj in plankArray)
            {
                if (!obj.activeSelf)
                {
                    allActive = false;
                    break;
                }
            }

            if (allActive)
            {
                Collider collider = gameObject.GetComponent<Collider>();
                collider.enabled = true;

                collider = blockingCollider.GetComponent<Collider>();
                collider.enabled = true;
                blocking = true;

                crowbar.transform.SetParent(deletedInventory.transform);
                crowbar.SetActive(false);

                SaveGame saveGameRef = GameObject.Find("SaveGame").GetComponent<SaveGame>();
                saveGameRef.saveJSON(crowbar.name, crowbar.transform.parent.name);

                collider = finalExitBlock.GetComponent<Collider>();
                collider.enabled = false;

                saveGameRef.saveJSON(gameObject.name, "True");
                saveGameRef.saveJSON(blockingCollider.name, "True");
                saveGameRef.saveJSON(finalExitBlock.name, "False");
            }
        }
    }
}
