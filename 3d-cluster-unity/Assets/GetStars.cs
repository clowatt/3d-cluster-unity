using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStars : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject star = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        star.transform.position = new Vector3(0, 1.5f, 0);    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
