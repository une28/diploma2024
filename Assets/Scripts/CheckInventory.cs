using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInventory : MonoBehaviour
{
    public GameObject selectedSlot;
    public GameObject key;
    public GameObject deletedInventory;
    public EnterExit checkBlocking;
    public AudioClip lockedSoundClip;
    public AudioClip openSoundClip;

    void OnMouseDown()
    {
        Collider collider;
        PlaySound playSoundRef = GetComponent<PlaySound>();
        if (!checkBlocking.blocking)
        {
            if (selectedSlot.transform.parent == key.transform.parent)
            {
                collider = GetComponent<Collider>();
                collider.enabled = false;

                key.transform.SetParent(deletedInventory.transform);
                key.transform.position = deletedInventory.transform.position;
                key.gameObject.SetActive(false);

                SaveGame saveGameRef = GameObject.Find("SaveGame").GetComponent<SaveGame>();
                saveGameRef.saveJSON(gameObject.name, "False");

                saveGameRef.saveJSON(key.name, key.transform.parent.name);

                playSoundRef.soundClip = openSoundClip;
                playSoundRef.playSound();
            }
            else
            {
                playSoundRef.soundClip = lockedSoundClip;
                playSoundRef.playSound();
            }
        } else
        {
            playSoundRef.soundClip = lockedSoundClip;
            playSoundRef.playSound();
        }
    }
}
