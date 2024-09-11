using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteSound : MonoBehaviour
{
    private bool isClicked = false;
    public GameObject soundCross;

    void Start()
    {
        SaveGame saveGameRef = GameObject.Find("SaveGame").GetComponent<SaveGame>();
        saveGameRef.readSound();
    }

    public void OnMouseDown()
    {
        SaveGame saveGameRef = GameObject.Find("SaveGame").GetComponent<SaveGame>();

        if (!isClicked)
        {
            isClicked = true;
            soundCross.SetActive(true);

            saveGameRef.saveJSON("MuteSound", "True");

            AudioListener.volume = 0;
        } else
        {
            isClicked = false;
            soundCross.SetActive(false);

            saveGameRef.saveJSON("MuteSound", "False");

            AudioListener.volume = 1;
        }
    }
}
