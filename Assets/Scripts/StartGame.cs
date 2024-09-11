using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject arrow;
    public GameObject menuUI;
    public GameObject menuCam;
    public GameObject soundButton;
    private Renderer objectRenderer;
    private Collider objectCollider;
    public bool loadGame = false;

    void OnMouseDown()
    {
        arrow.transform.localScale = new Vector3(0.0178f, 0.0178f, 0.0178f);
        loadGame = true;

        mainCamera.SetActive(true);
        menuCam.SetActive(false);

        menuUI.SetActive(false);

        soundButton.SetActive(false);
    }
}
