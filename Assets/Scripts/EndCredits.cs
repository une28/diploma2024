using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCredits : MonoBehaviour
{
    public DarkenScreen darkenScreenRef;
    public GameObject plane;
    public GameObject menuCam;
    public Menu menuRef;
    private Collider objCollider;
    public GameObject inventoryBackground;
    public GameObject inventorySlot;
    private Vector3 arrowScale;
    public GameObject arrow;
    private GameObject[] objectsToHide;
    public SwitchCamera switchCameraRef;
    public GameObject activeCam;
    public GameObject camToActivate;

    void OnMouseDown()
    {
        if (darkenScreenRef.endGame)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    IEnumerator returnAlpha()
    {
        float duration = 0.3f;
        ChangeObjectAlpha(0f, duration);
        yield return new WaitForSeconds(duration);

    }

    IEnumerator ChangeAlpha()
    {
        float duration = 0.3f;

        ChangeObjectAlpha(0.5f, duration);
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
