using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{

    [SerializeField] float anglePerSec = 10.0f;
    [SerializeField] float cameraMoveSpeed = 0.5f;
    PlayerActions input;

    // Store the Vector2 result from CameraControl.Movement to move camera
    Vector2 currentMovement;

    // Store the up/down result from CameraControl.VerticalMovement to move camera
    float currentVerticalMovement;

    // Store the Vector2 result from CameraControl.Rotation to rotate camera
    Vector2 currentRotation;

    //Awake is called when the script is first built
    void Awake()
    {
        input = new PlayerActions();

        // Get input, += callback context. => lamdba expression to do something.
        input.CameraControl.Movement.performed += ctx => currentMovement = ctx.ReadValue<Vector2>();
        input.CameraControl.VerticalMovement.performed += ctx => currentVerticalMovement = ctx.ReadValue<float>();
        input.CameraControl.Rotation.performed += ctx => currentRotation = ctx.ReadValue<Vector2>();
    } 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float xValue = currentMovement.x * Time.deltaTime * cameraMoveSpeed;
        float yValue = currentVerticalMovement * Time.deltaTime * cameraMoveSpeed;
        float zValue = currentMovement.y * Time.deltaTime * cameraMoveSpeed;
        transform.Translate(xValue,yValue,zValue);

        float xRotate = currentRotation.x * Time.deltaTime * anglePerSec;
        float yRotate = currentRotation.y * Time.deltaTime * anglePerSec;

        transform.Rotate(xRotate, yRotate, 0);
    }

    void OnEnable()
    {
        input.CameraControl.Enable();
    }

    void OnDisable() 
    {
        input.CameraControl.Disable();
    }

}
