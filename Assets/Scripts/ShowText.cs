using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ShowText : MonoBehaviour
{
    public TextMeshProUGUI textToShow;
    public TextMeshProUGUI generalText;
    private bool stopSlowTyping = false;
    private bool doneTyping = false;
    private bool isNewline = false;
    public GameObject inventoryBackground;
    public GameObject inventorySlot;
    public GameObject arrow;
    private GameObject[] objectsToHide;
    private MeshRenderer objRenderer;
    private Collider objCollider;
    private Vector3 arrowScale;
    public float letterSpeed = 0.1f;

    public void OnEnable()
    {
        StartCoroutine(showText(generalText.text));
        objectsToHide = new GameObject[] { inventoryBackground, inventorySlot };
        arrowScale = arrow.transform.localScale;
    }

    IEnumerator showText(string text)
    {
        yield return new WaitForSeconds(0.1f);

        textToShow.GetComponent<TextMeshProUGUI>().enabled = true;

        textToShow.text = "";
        doneTyping = false;

        for (int i = 0; i < text.Length; i++)
        {
            isNewline = false;

            if (stopSlowTyping)
            {
                stopSlowTyping = false;
                

                int j = i;
                while (j < text.Length && text[j] != '\n')
                {
                    textToShow.text += text[j];
                    j++;
                }

                if (j >= text.Length)
                {
                    doneTyping = true;
                    yield break;
                } else
                {
                    i = j;
                }
            }
            textToShow.text += text[i];
            yield return new WaitForSeconds(letterSpeed);

            if (text[i] == '\n')
            {
                isNewline = true;
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                textToShow.text = "";
            }
        }
        doneTyping = true;
    }

    private void OnMouseDown()
    {
        if(!isNewline)
        {
            if (!doneTyping)
            {
                stopSlowTyping = true;
            }
            else
            {
                foreach (GameObject obj in objectsToHide)
                {
                    obj.SetActive(true);
                }

                textToShow.GetComponent<TextMeshProUGUI>().enabled = false;

                gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        
    }
}
