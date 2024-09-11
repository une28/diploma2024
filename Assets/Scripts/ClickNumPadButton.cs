using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickNumPadButton : MonoBehaviour
{
    public string buttonName;
    public GameObject bottle;
    public TextMeshProUGUI display; 
    private bool insertedMoney = false;
    private List<string> numList = new List<string>() { "201", "202", "203", "204", "205" };
    public InsertMoney scriptRef;
    public AudioClip beepSound;
    public AudioClip bottleFallSound;
    public AudioClip notEnoughMoneySound;

    void OnMouseDown()
    {
        PlaySound playSoundRef = GetComponent<PlaySound>();

        if (buttonName == "OK")
        {
            insertedMoney = scriptRef.insertedMoney;
            if (insertedMoney == true && numList.Contains(display.text)) 
            {
                playSoundRef.soundClip = bottleFallSound;
                playSoundRef.playSound();

                display.text = "";
                bottle.SetActive(true);

                SaveGame saveGameRef = GameObject.Find("SaveGame").GetComponent<SaveGame>();
                saveGameRef.saveJSON("bottleActiveSelf", "True");
            } else
            {
                playSoundRef.soundClip = notEnoughMoneySound;
                playSoundRef.playSound();
            }
        } 
        else if (buttonName == "DEL")
        {
            display.text = display.text.Substring(0, display.text.Length - 1);
        }
        else
        {
            if (display.text.Length < 3)
            {
                display.text += buttonName;

            }
        }
    }
}
