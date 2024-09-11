using System.Collections;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public float rotationAngle = 90f; 
    public float rotationAngleX = 0f; 
    private bool isOpen = false;
    public GameObject mainCam;
    public float yOffset = 0.0f;
    public AudioClip soundClipOpen;
    public float playbackSpeedOpen = 1f;
    public float volumeLevelOpen = 1f;
    public AudioClip soundClipClose;
    public float playbackSpeedClose = 1f;
    public float volumeLevelClose = 1f;

    void OnMouseDown()
    {
        if (mainCam.activeSelf)
        {
            if (!isOpen)
            {
                transform.Rotate(rotationAngleX, 0, rotationAngle);
                transform.Translate(Vector3.forward * yOffset, Space.World);
                isOpen = true;

                playSound(soundClipOpen, playbackSpeedOpen, volumeLevelOpen);
            }
            else
            {
                transform.Rotate(-rotationAngleX, 0, -rotationAngle);
                transform.Translate(Vector3.back * yOffset, Space.World);
                isOpen = false;

                playSound(soundClipClose, playbackSpeedClose, volumeLevelClose);
            }
        }
    }

    public void playSound(AudioClip soundClip, float playbackSpeed, float volumeLevel)
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
    }
}
