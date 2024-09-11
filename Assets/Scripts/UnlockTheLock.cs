using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockTheLock : MonoBehaviour
{
    public GameObject selectedSlot;
    public GameObject deletedInventory;
    public GameObject bottle;
    public GameObject suitcaseTop;
    public GameObject cam;
    public GameObject paintStain;
    public SpinThePIn pin1;
    public SpinThePIn pin2;
    public SpinThePIn pin3;
    public GameObject pinNumbers1;
    public GameObject pinNumbers2;
    public GameObject pinNumbers3;
    public GameObject crowbar;
    public GameObject sock;
    public bool gameLoad = false;
    public bool gameLoadLock = false;
    public AudioClip stuckPinSound;
    public AudioClip cleanStainSound;
    public AudioClip openLockSound;
    
    public void OnMouseDown()
    {
        if (cam.activeSelf || gameLoad)
        {
            SaveGame saveGameRef = GameObject.Find("SaveGame").GetComponent<SaveGame>();
            PlaySound playSoundRef = GetComponent<PlaySound>();

            if (paintStain.activeSelf)
            {
                if (selectedSlot.transform.parent == bottle.transform.parent || gameLoad)
                {
                    paintStain.SetActive(false);

                    pinNumbers1.SetActive(true);
                    pinNumbers2.SetActive(true);
                    pinNumbers3.SetActive(true);

                    bottle.transform.SetParent(deletedInventory.transform);
                    saveGameRef.saveJSON(bottle.name, bottle.transform.parent.name);

                    bottle.transform.position = deletedInventory.transform.position;
                    bottle.gameObject.SetActive(false);

                    if (!gameLoad)
                    {
                        playSoundRef.soundClip = cleanStainSound;
                        playSoundRef.playbackSpeed = 1.0f;
                        playSoundRef.playSound();
                    }
                }
                else
                {
                    if (!gameLoad)
                    {
                        playSoundRef.soundClip = stuckPinSound;
                        playSoundRef.playbackSpeed = 1.2f;
                        playSoundRef.playSound();
                    }
                }
            }
            else
            {
                bool value1 = pin1.isRightRotation;
                bool value2 = pin2.isRightRotation;
                bool value3 = pin3.isRightRotation;

                if(value1 && value2 && value3 || gameLoadLock)
                {
                    transform.Translate(Vector3.forward * (-0.05f), Space.Self);
                    transform.eulerAngles = new Vector3(0f, 200f, 90f);

                    suitcaseTop.transform.Rotate(new Vector3(0f, 45f, 0f), Space.Self);

                    Collider collider = GetComponent<Collider>();
                    collider.enabled = false;

                    collider = crowbar.GetComponent<Collider>();
                    collider.enabled = true;

                    collider = sock.GetComponent<Collider>();
                    collider.enabled = true;

                    saveGameRef.saveJSON(crowbar.name + "Collider", "True");

                    if (!gameLoad)
                    {
                        playSoundRef.soundClip = openLockSound;
                        playSoundRef.playbackSpeed = 1.5f;
                        playSoundRef.playSound();
                    }
                }
            }

            gameLoad = false;
            gameLoadLock = false;
        }
    }
}
