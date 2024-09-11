using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveNails : MonoBehaviour
{
    public GameObject activeCam;
    public GameObject friendPlank;
    public GameObject friendNail;
    public GameObject standingPlank;
    public GameObject crowbar;
    public GameObject selectedSlot;
    private MeshRenderer objRenderer;

    void Start()
    {
        
    }

    void OnMouseDown()
    {
        if (selectedSlot.transform.parent == crowbar.transform.parent && activeCam.activeSelf)
        {
            PlaySound playSoundRef = GetComponent<PlaySound>();
            playSoundRef.volumeLevel = 0.5f;
            playSoundRef.playSound();

            objRenderer = gameObject.GetComponent<MeshRenderer>();
            objRenderer.enabled = false;

            objRenderer = friendNail.GetComponent<MeshRenderer>();

            if (!objRenderer.enabled)
            {
                friendPlank.SetActive(false);
                standingPlank.SetActive(true);

                PlaySound playSoundPlankRef = standingPlank.GetComponent<PlaySound>();
                playSoundPlankRef.playSound();
            }
        }   
    }

    void Update()
    {
        
    }
}
