using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowAuthorInfo : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI authorText;
    public GameObject loadGame;
    public GameObject exitGame;
    public GameObject soundButton;
    private bool isEnabled = false;

    void Start()
    {
        
    }

    public void OnMouseDown()
    {
        isEnabled = authorText.GetComponent<TextMeshProUGUI>().enabled;

        if (isEnabled)
        {
            buttonText.text = "?";
        } else
        {
            buttonText.text = "X";
        }
        
        loadGame.SetActive(isEnabled);
        exitGame.SetActive(isEnabled);
        soundButton.SetActive(isEnabled);

        authorText.GetComponent<TextMeshProUGUI>().enabled = !isEnabled;
    }

    void Update()
    {
        
    }
}
