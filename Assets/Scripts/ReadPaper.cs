using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadPaper : MonoBehaviour
{
    public GameObject cam;
    public GameObject plane;
    private bool isClicked = false;
    public float translateX = 0f;
    public float translateY = 0f;
    public float translateZ = 0f;
    public float rotateX = 0f;
    public float rotateY = 0f;
    public float rotateZ = 0f;
    private Collider objCollider;
    public GameObject canvas;
    private Vector3 paperScale;
    private Quaternion paperRotation;
    private Vector3 paperLocation;
    public float scaleNumber = 1.0f;

    void Start()
    {
        paperScale = transform.localScale;
        paperRotation = transform.rotation;
        paperLocation = transform.position;
        canvas.SetActive(false);
    }

    public void OnMouseDown()
    {
        if (cam.activeSelf)
        {
            
            if (!isClicked)
            {
                isClicked = true;

                transform.localScale = paperScale * scaleNumber;

                transform.Translate(Vector3.forward * translateX, Space.Self);
                transform.Translate(Vector3.right * translateY, Space.Self);
                transform.Translate(Vector3.up * translateZ, Space.Self);

                transform.Rotate(new Vector3(rotateX, rotateY, rotateZ), Space.Self);

                gameObject.layer = LayerMask.NameToLayer("UI");

                objCollider = plane.GetComponent<Collider>();
                objCollider.enabled = true;

                canvas.SetActive(true);

                StartCoroutine(ChangeAlpha());
            } else
            {
                isClicked = false;

                transform.localScale = paperScale;
                transform.rotation = paperRotation;
                transform.position = paperLocation;

                gameObject.layer = LayerMask.NameToLayer("Default");

                objCollider = plane.GetComponent<Collider>();
                objCollider.enabled = false;

                canvas.SetActive(false);

                StartCoroutine(returnAlpha());
            }
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

        ChangeObjectAlpha(0.8f, duration);
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
