using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveCamera : MonoBehaviour
{
    [SerializeField] float autoCameraSpeed = 0.1f;
    bool forward = true;   
    float startZValue;
    
    // Start is called before the first frame update
    void Start()
    {
        // Store the starting position of the camera
        startZValue = transform.position.z; 
    }

    // Update is called once per frame
    void Update()
    {
            MoveCameraEnabled();
    }

    void MoveCameraEnabled()
    {
        // if the position is not yet at the centre 
        if (transform.position.z >= 0f)
        {
            forward = false;
            transform.Translate(0f, 0f, -autoCameraSpeed);
        }
        else if (forward)
        {
            transform.Translate(0f, 0f, autoCameraSpeed);
        } 
        else if (transform.position.z < startZValue)
        {
            // Do nothing
            transform.Translate(0f, 0f, 0f);
        }
        else 
        {
            transform.Translate(0f, 0f, -autoCameraSpeed);
        }
    }
}
