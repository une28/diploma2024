using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public GameObject activeCam = null;
    public bool activeCamNeed = true;
    public AudioClip soundClip;
    public float playbackSpeed = 1f;
    public float volumeLevel = 1f;
    public bool onClick = true;
    public bool onEvent = false;
    public bool isOneTimeEvent = false;
    public bool shouldPlayOnAwake = true;

    void Start()
    {
        if (gameObject.name == "arrow")
        {
            AudioSource audioSource = GetComponent<AudioSource>();

            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            audioSource.clip = soundClip;
            audioSource.pitch = playbackSpeed;
            audioSource.volume = volumeLevel;
        }
    }

    void OnMouseDown()
    {
        if (!activeCamNeed && onClick || activeCam.activeSelf && onClick )
        {
            playSound();

            if (isOneTimeEvent)
            {
                onClick = false;
            }
        }
    }

    public void playSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = soundClip;
        audioSource.pitch = playbackSpeed;
        audioSource.volume = volumeLevel;

        audioSource.Play();

        audioSource.playOnAwake = shouldPlayOnAwake;
    }

    void Update()
    {
        if (activeCam.activeSelf && onEvent)
        {
            onEvent = false;
            playSound();
        }
    }
}
