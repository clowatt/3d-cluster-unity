using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets; 

public class GetStars : MonoBehaviour
{
    // Reference to the Prefab
    public GameObject Star;
    // Start is called before the first frame update
    void Start()
    {
        getStarsFromFile();
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

    void getStarsFromFile()
    {
        string assetFileLocation = "Assets/DataFiles/snapshot.txt";
        //var assetHadlner = Addressables.LoadAssetAsync<TextAsset>(assetFileLocation);
        //Debug.Log(assetHadlner);
        Addressables.LoadAssetAsync<TextAsset>(assetFileLocation).Completed += handle =>
        {
            Debug.Log(handle.Result.text);
            Addressables.Release(handle);
        };
    }

}
