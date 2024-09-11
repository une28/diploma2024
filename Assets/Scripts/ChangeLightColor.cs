using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLightColor : MonoBehaviour
{
    public GameObject Light;
    public GameObject vendingMachine;
    public GameObject table;
    private SaveGame saveGameRef;
    public GameObject[] coins;
    public AudioClip coinSound;
    public AudioClip beepSound;
    public bool isGameLoad = false;

    private int check = 1;
    void Start()
    {
        float emissiveIntensity = 1.5f;
        Color emissiveColor = Color.yellow;
        Material material = Light.GetComponent<Renderer>().material;
        material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);

    }

    public void OnMouseDown()
    {
        check *= (-1);
        StartCoroutine(DelayedExecution(check));
    }

    IEnumerator DelayedExecution(int num)
    {
        yield return new WaitForSeconds(0.6f);
        saveGameRef = GameObject.Find("SaveGame").GetComponent<SaveGame>();
        PlaySound playSoundRef = GetComponent<PlaySound>();

        if (num == -1)
        {
            float emissiveIntensity = 1.5f;
            Color emissiveColor = Color.red;
            Material material = Light.GetComponent<Renderer>().material;
            material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);
            vendingMachine.transform.Translate(Vector3.up * (-1.5f), Space.Self);
            table.transform.Translate(Vector3.up * (1.5f), Space.Self);

            foreach (GameObject coin in coins)
            {
                coin.SetActive(false);

            }

            if (!isGameLoad)
            {
                playSoundRef.soundClip = beepSound;
                playSoundRef.playSound();
            }

            saveGameRef.saveJSON(Light.name, "red");

        }
        else
        {
            float emissiveIntensity = 1.5f;
            Color emissiveColor = Color.yellow;
            Material material = Light.GetComponent<Renderer>().material;
            material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);

            vendingMachine.transform.Translate(Vector3.up * (1.5f), Space.Self);
            table.transform.Translate(Vector3.up * (-1.5f), Space.Self);

            foreach (GameObject coin in coins)
            {
                coin.SetActive(true);

            }

            if (!isGameLoad)
            {
                playSoundRef.soundClip = coinSound;
                playSoundRef.playSound();
            }
                
            saveGameRef.saveJSON(Light.name, "yellow");

        }
        isGameLoad = false;
    }
}
