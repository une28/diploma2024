using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkenScreen : MonoBehaviour
{
    public GameObject plane;
    public GameObject activeCam;
    private Collider objCollider;
    public bool endGame = false;

    void OnMouseDown()
    {
        if (activeCam.activeSelf)
        {
            objCollider = plane.GetComponent<Collider>();
            objCollider.enabled = true;
            StartCoroutine(ChangeAlpha());

            SaveGame saveGameRef = GameObject.Find("SaveGame").GetComponent<SaveGame>();
            saveGameRef.clearSave();

            endGame = true;
        }
    }

    void Update()
    {
        
    }

    IEnumerator returnAlpha()
    {
        float duration = 0.3f;
        ChangeObjectAlpha(0f, duration);
        yield return new WaitForSeconds(duration);

    }

    IEnumerator ChangeAlpha()
    {
        float duration = 2.0f;

        ChangeObjectAlpha(1f, duration);
        yield return new WaitForSeconds(duration);
        yield return new WaitForSeconds(duration);

    }

    void ChangeObjectAlpha(float targetAlpha, float duration)
    {
        Renderer renderer = plane.GetComponent<Renderer>();
        Color currentColor = renderer.material.color;
        Color targetColor = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
        StartCoroutine(ChangeAlphaOverTime(renderer, currentColor, targetColor, duration));
    }

    IEnumerator ChangeAlphaOverTime(Renderer renderer, Color startColor, Color targetColor, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            renderer.material.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        renderer.material.color = targetColor;
    }
}
