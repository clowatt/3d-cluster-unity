using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{

    PlayerActions input;

    // Store the Vector2 result from CameraControl.Movement
    Vector2 currentMovement;

    // Store the Vector2 result from CameraControl.Movement
    Vector2 currentRotation;

    //Awake is called when the script is first built
    void Awake()
    {
        input = new PlayerActions();

        // Get input, += callback context. => lamdba expression to do something.
        input.CameraControl.Movement.performed += ctx => currentMovement = ctx.ReadValue<Vector2>();
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
        float xValue = currentMovement.x * Time.deltaTime;;
        float yValue = 0;
        float zValue = currentMovement.y * Time.deltaTime;;
        transform.Translate(xValue,yValue,zValue);

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
