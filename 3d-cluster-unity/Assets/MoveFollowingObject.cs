using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFollowingObject : MonoBehaviour
{
    float moveSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        moveCameraObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void moveCameraObject()
    {
        float xValue = moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        float yValue = 0;
        float zValue = moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.Translate(xValue,yValue,zValue);
    }
}
