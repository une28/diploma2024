using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;
using static SaveGame;

public class SaveGame : MonoBehaviour
{
    public TextAsset jsonFile;
    private bool checkState;
    private string checkStateString;
    public GameObject[] colliderArray;
    public CallForText[] textRefs;
    public GameObject[] inventoryObject;
    public AddToInventory[] addToInventoryRef;
    public GameObject bottle;
    public UnlockTheLock unlockTheLockRef;
    public UnlockFrontDoor unlockFrontDoorRef;
    public GameObject[] camArray;
    public GameObject activeCam;
    public TextMeshProUGUI textCoinsCount;
    public SwitchCamera switchCameraRef;
    public ChangeView changeViewRef;
    public GameObject Light;
    public ChangeLightColor changeLightColorRef;
    public InsertMoney insertMoneyRef;
    public GameObject[] plankArray;
    public GameObject[] standingBoardArray;
    public GameObject[] nailArray;
    public MuteSound muteSoundRef;

    void Start()
    {
        camArray = new GameObject[changeViewRef.camArray.Length];
        for (int i = 0; i < changeViewRef.camArray.Length; i++)
        {
            camArray[i] = changeViewRef.camArray[i];
        }

        saveJSON("SaveFile", "True");
        saveJSON("MuteSound", "False");
    }

    public void changeCamera(GameObject camToActivate, GameObject camToDeactivate)
    {
        saveJSON(camToDeactivate.name, "False");
        saveJSON(camToActivate.name, "True");
        
    }

    public void loadGame()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        string filePath = Path.Combine(Application.persistentDataPath, "Save/Save.json");
        #else
                string filePath = Path.Combine(Application.dataPath, "Save/Save.json");
#endif

        

        if (!File.Exists(filePath))
        {
            Debug.LogWarning("File does not exist, will be created");
        } else
        {
            string jsonValue;
            if (colliderArray != null)
            {
                foreach (GameObject obj in colliderArray)
                {

                    jsonValue = readJson(obj.name);
                    if (bool.TryParse(jsonValue, out checkState))
                    {
                        obj.GetComponent<Collider>().enabled = checkState;

                        if (checkState && obj == colliderArray[3])
                        {
                            for (int i = 0; i < plankArray.Length; i++)
                            {
                                plankArray[i].SetActive(false);
                                standingBoardArray[i].SetActive(true);
                                nailArray[i].SetActive(false);
                            }
                            for (int i = 0; i < nailArray.Length; i++)
                            {
                                nailArray[i].SetActive(false);
                            }

                        }

                    }

                }
            }

            if (!colliderArray[0].GetComponent<Collider>().enabled)
            {
                unlockFrontDoorRef.gameLoad = true;
                unlockFrontDoorRef.OnMouseDown();
            }



            foreach (CallForText obj in textRefs)
            {
                jsonValue = readJson(obj.name + "Text");

                if (bool.TryParse(jsonValue, out checkState))
                {
                    if (checkState != obj.onEvent)
                    {
                        obj.onEvent = checkState;
                    }
                        
                }
            }

            if (inventoryObject != null && addToInventoryRef != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    checkStateString = readJson(inventoryObject[i].name);
                    if (checkStateString != inventoryObject[i].transform.parent.name)
                    {
                        if (i == 0)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                jsonValue = readJson(inventoryObject[j + 4].name);
                                bool checkRef;
                                if (bool.TryParse(jsonValue, out checkRef))
                                {
                                    if (!checkRef)
                                    {
                                        addToInventoryRef[j].isGameLoad = true;
                                        addToInventoryRef[j].selfObj = inventoryObject[j + 4];
                                        addToInventoryRef[j].OnMouseDown();
                                    }
                                }
                            }
                        }
                        else
                        if (checkStateString == "inventorySlot(Clone)")
                        {

                            addToInventoryRef[i + 2].isGameLoad = true;
                            addToInventoryRef[i + 2].selfObj = inventoryObject[i];
                            addToInventoryRef[i + 2].OnMouseDown();
                        }

                        if (checkStateString == "DeletedInventory")
                        {
                            switch (i)
                            {
                                case 0:
                                    textCoinsCount.enabled = false;
                                    insertMoneyRef.isGameLoad = true;
                                    insertMoneyRef.OnMouseDown();
                                    break;
                                case 1:
                                    unlockTheLockRef.gameLoad = true;
                                    unlockTheLockRef.OnMouseDown();
                                    break;
                            }

                            inventoryObject[i].transform.SetParent(GameObject.Find(checkStateString).transform);
                            inventoryObject[i].SetActive(false);
                        }

                    }
                    if (i == 3)
                    {
                        string crowbarCollider = readJson(inventoryObject[i].name + "Collider");
                        bool crowbarColliderCheck;
                        if (bool.TryParse(crowbarCollider, out crowbarColliderCheck))
                        {
                            if (crowbarColliderCheck)
                            {
                                unlockTheLockRef.gameLoad = true;
                                unlockTheLockRef.gameLoadLock = true;
                                unlockTheLockRef.OnMouseDown();
                            }
                        } 
                        else

                        if (checkStateString == "DeletedInventory" || checkStateString == "inventorySlot(Clone)")
                        {
                            unlockTheLockRef.gameLoad = true;
                            unlockTheLockRef.gameLoadLock = true;
                            unlockTheLockRef.OnMouseDown();
                        }
                    }
                }
            }

            if (bottle != null)
            {
                string bottleActive = readJson("bottleActiveSelf");
                bool bottleActiveCheck;
                if (bool.TryParse(bottleActive, out bottleActiveCheck))
                {
                    bottle.SetActive(bottleActiveCheck);
                }
            }

            if (Light != null)
            {
                if (readJson(Light.name) == "red")
                {
                    changeLightColorRef.isGameLoad = true;
                    changeLightColorRef.OnMouseDown();
                }
            }

            GameObject camToActivate = activeCam;
            foreach (GameObject obj in camArray)
            {
                jsonValue = readJson(obj.name);

                if (bool.TryParse(jsonValue, out checkState))
                {
                    if (checkState)
                    {
                        camToActivate = obj;
                        break;
                    }
                }
            }
            if (camToActivate != activeCam)
            {
                switchCameraRef.camActive = activeCam;
                switchCameraRef.camToActivate = camToActivate;
                switchCameraRef.OnMouseDown();
            }
        }
    }

    public void readSound()
    {
        string jsonValue = readJson("MuteSound");
        bool checkState;
        if (bool.TryParse(jsonValue, out checkState))
        {
            if (checkState)
            {
                muteSoundRef.OnMouseDown();
            }
        }
    }

    public void clearSave()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
                string filePath = Path.Combine(Application.persistentDataPath, "Save/Save.json");
        #else
                string filePath = Path.Combine(Application.dataPath, "Save/Save.json");
        #endif

        File.WriteAllText(filePath, string.Empty);
    }

    public void saveJSON(string objName, string objState)
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        string filePath = Path.Combine(Application.persistentDataPath, "Save/Save.json");
        string dirPath = Path.Combine(Application.persistentDataPath, "Save");
        #else
        string filePath = Path.Combine(Application.dataPath, "Save/Save.json");
        string dirPath = Path.Combine(Application.dataPath, "Save");
        #endif

        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        if (!File.Exists(filePath))
        {
            GameObjectSaves gameObjectsData = new GameObjectSaves();
            gameObjectsData.gameObjectSaves = new GameObjectSave[0];
            string jsonData = JsonUtility.ToJson(gameObjectsData, true);
            File.WriteAllText(filePath, jsonData);
        }

        string jsonContent = File.ReadAllText(filePath);

        GameObjectSaves gameObjects;
        if (!string.IsNullOrEmpty(jsonContent))
        {
            gameObjects = JsonUtility.FromJson<GameObjectSaves>(jsonContent);

           
        } else
        {
            gameObjects = new GameObjectSaves();
            gameObjects.gameObjectSaves = new GameObjectSave[0];
            string jsonData = JsonUtility.ToJson(gameObjects, true);
            File.WriteAllText(filePath, jsonData);

        }

        GameObjectSave myObject = new GameObjectSave();
        myObject.name = objName;
        myObject.state = objState.ToString();

        string json = JsonUtility.ToJson(myObject);

        bool objectExists = false;
        if (gameObjects != null && gameObjects.gameObjectSaves != null)
        {
            foreach (GameObjectSave existingObj in gameObjects.gameObjectSaves)
            {
                if (existingObj.name == myObject.name)
                {
                    existingObj.state = myObject.state;
                    objectExists = true;
                    break;
                }
            }

        }

        if (!objectExists)
        {
            int length = gameObjects.gameObjectSaves.Length;
            Array.Resize(ref gameObjects.gameObjectSaves, length + 1);
            gameObjects.gameObjectSaves[length] = myObject;
        }

        string updatedJsonData = JsonUtility.ToJson(gameObjects, true);
        File.WriteAllText(filePath, updatedJsonData);

        Debug.Log("JSON data saved successfully.");
        Debug.Log(filePath);
    }

    public string readJson(string objName)
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
                string filePath = Path.Combine(Application.persistentDataPath, "Save/Save.json");
        #else
                string filePath = Path.Combine(Application.dataPath, "Save/Save.json");
        #endif

        if (!File.Exists(filePath))
        {
            Debug.LogWarning("File does not exist.");
            return "";
        }

        string jsonContent = File.ReadAllText(filePath);
        GameObjectSaves gameObjectSaves = JsonUtility.FromJson<GameObjectSaves>(jsonContent);

        foreach (GameObjectSave obj in gameObjectSaves.gameObjectSaves)
        {
            if (obj.name == objName)
            {
                return obj.state;
            }
        }

        return "";
    }

    [System.Serializable]
    public class GameObjectSave
    {
        public string name;
        public string state;
    }

    [System.Serializable]
    public class GameObjectSaves
    {
        public GameObjectSave[] gameObjectSaves;
    }

    public void startLoading()
    {
        loadGame();
    }
}
