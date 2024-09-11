using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinThePIn : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    public float swipeThreshold = 100f;
    public bool isRightRotation = false;
    public int rightNum;
    public GameObject paintStain;
    public AudioClip rotatePinSound;
    public AudioClip stuckPinSound;

    void OnMouseDown()
    {
        startTouchPosition = Input.mousePosition;
    }

    void OnMouseUp() {

        if (!paintStain.activeSelf)
        {
            PlaySound playSoundRef = GetComponent<PlaySound>();
            if (Input.GetMouseButtonUp(0))
            {
                endTouchPosition = Input.mousePosition;
                float swipeDistance = endTouchPosition.x - startTouchPosition.x;
                if (Mathf.Abs(swipeDistance) > swipeThreshold)
                {
                    if (swipeDistance < 0)
                    {
                        transform.Rotate(Vector3.forward, 36f);

                        if (transform.localRotation.eulerAngles.z == 360f)
                        {
                            Quaternion newRotation = Quaternion.Euler(0f, 0f, 0f); 
                            transform.rotation = newRotation; 
                        }

                        if (Mathf.Approximately(transform.localRotation.eulerAngles.z, 36f * (rightNum - 1)))
                        {
                            isRightRotation = true;
                        }
                        else
                        {
                            isRightRotation = false;
                        }
                        playSoundRef.soundClip = rotatePinSound;
                        playSoundRef.playbackSpeed = 1.0f;
                        playSoundRef.playSound();

                    }
                    else
                    {
                        playSoundRef.soundClip = stuckPinSound;
                        playSoundRef.playbackSpeed = 1.2f;
                        playSoundRef.playSound();
                    }
                }
            }
        }
    }
}
