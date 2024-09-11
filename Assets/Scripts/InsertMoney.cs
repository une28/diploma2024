using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InsertMoney : MonoBehaviour
{
    public TextMeshProUGUI textToHide;
    public GameObject coins;
    public GameObject canvas;
    public GameObject deletedInventory;
    public bool insertedMoney = false;
    public bool isGameLoad = false;
    public AudioClip coinsFall;
    public AudioClip notEnoughMoney;
    public GameObject selectedSlot;

    public void OnMouseDown()
    {
        PlaySound playSoundRef = GetComponent<PlaySound>();

        if (!insertedMoney)
        {
            if (textToHide.text == "15" && selectedSlot.transform.parent == coins.transform.parent || isGameLoad)
            {
                if (!isGameLoad)
                {
                    playSoundRef.soundClip = coinsFall;
                    playSoundRef.playSound();
                }

                coins.transform.SetParent(deletedInventory.transform);

                SaveGame saveGameRef = GameObject.Find("SaveGame").GetComponent<SaveGame>();
                saveGameRef.saveJSON(coins.name, coins.transform.parent.name);

                coins.transform.position = deletedInventory.transform.position;
                canvas.transform.SetParent(deletedInventory.transform);
                canvas.transform.position = deletedInventory.transform.position;

                coins.SetActive(false);
                canvas.SetActive(false);
                insertedMoney = true;
            }
            else
            {
                playSoundRef.soundClip = notEnoughMoney;
                playSoundRef.playSound();
            }
            isGameLoad = false;
        }
    }
}
