using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CallForText : MonoBehaviour
{
    public GameObject activeCam;
    public TextMeshProUGUI textToShow;
    public GameObject textBackground;
    public TextAsset jsonFile;
    public int dialogId = 1;
    public bool onClick = true;
    public bool onEvent = false;

    void Start()
    {
        if (gameObject.name == "CameraMainRoomWindowUI")
        {
            SaveGame saveGameRef = GameObject.Find("SaveGame").GetComponent<SaveGame>();
            string jsonValue = saveGameRef.readJson(gameObject.name + "Text");
            bool checkState;
            if (bool.TryParse(jsonValue, out checkState))
            {
                if (checkState == false )
                    onEvent = false;

            }
        }

        if (activeCam.activeSelf && onEvent)
        {
            onEvent = false;
            readJSON();

            SaveGame saveGameRef = GameObject.Find("SaveGame").GetComponent<SaveGame>();
            saveGameRef.saveJSON(gameObject.name + "Text", "False");

        }

    }
    void OnMouseDown()
    { 
        if (activeCam.activeSelf && onClick)
        {
            readJSON();
            
        }

    }

    void readJSON()
    {
        GameTexts gameTextsInJson = JsonUtility.FromJson<GameTexts>(jsonFile.text);

        foreach (GameText text in gameTextsInJson.gameTexts)
        {
            if (text.id == dialogId.ToString())
            {
                textToShow.text = text.gameText;
                textBackground.SetActive(true);
                break;
            }
        }
    }

    [System.Serializable]
    public class GameText
    {
        public string id;
        public string gameText;
    }

    [System.Serializable]
    public class GameTexts
    {
        public GameText[] gameTexts;
    }
}
