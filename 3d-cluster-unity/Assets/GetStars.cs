using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStars : MonoBehaviour
{
    // Reference to the Prefab
    public GameObject Star;
    // Start is called before the first frame update
    void Start()
    {
        InitializeStarPrefab(0, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeStarPrefab(float x ,float y, float z)
    {
        // Instantiate at position (x, y, z) and zero rotation
        Instantiate(Star, new Vector3(x, y, z), Quaternion.identity);
    }

}
