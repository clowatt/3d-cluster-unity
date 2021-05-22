using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets; 

public class GetStars : MonoBehaviour
{
    // allow the ability to update the main camera
    GameObject mainCamera;

    // Store the maximum radius of the star cluster
    float clusterMaxRadius;
    
    // Reference to the Prefab
    public GameObject Star;


    [SerializeField] string assetFileLocation = "Assets/DataFiles/snapshot.txt";
    
    // Start is called before the first frame update
    void Start()
    {

        // Initialize the Cluster
        // This will also set the camera start location
        InitializeClusterConditions();

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    // Initialize the stars based on provided location in pc
    void InitializeStarPrefab(float x ,float y, float z)
    {

        Instantiate(Star, new Vector3(x, y, z), Quaternion.identity);
    }


    void GetFromFile()
    {
        // Load the text file from addressable asset location and store in handler
        Addressables.LoadAssetAsync<TextAsset>(assetFileLocation).Completed += handle =>
        {
            var fullFile = handle.Result.text;
            var lines = fullFile.Split('\n');
            float currentRadius;
            clusterMaxRadius = 0f;
                        
            // Start at i= 1 because i = 0 is the header line
            // 1 less than lines.Length as the length of the array 
            //     is 1 longer than the lines in the file
            for (int i = 1; i < lines.Length - 1; i++)
            {
                
                var singleLine = lines[i].Split(',');
                
                // Beware! y is in the vertical in unity, not z
                float xL = float.Parse(singleLine[0]); // x in file
                float yL = float.Parse(singleLine[2]); // z in file
                float zL = float.Parse(singleLine[1]); // y in file

                InitializeStarPrefab(xL, yL, zL);

                // Check for max raidus
                currentRadius = Mathf.Sqrt( (xL*xL) + (yL*yL) + (zL+zL));

                if (currentRadius > clusterMaxRadius)
                {
                    clusterMaxRadius = currentRadius;
                }

                // Update the camera location to be at the max radius
                
                mainCamera = GameObject.Find("Main Camera");
                mainCamera.transform.position = new Vector3(0, 0, -clusterMaxRadius);
            }

            // Release the asset handler
            Addressables.Release(handle);
        };

    }

    // GetStarsFromFile is called from Start() and will
    // either generate stars from a file, or fall back
    // to generating one star at the origin
    void InitializeClusterConditions()
    {

        try
        {
            GetFromFile();
        }
        catch
        {
            InitializeStarPrefab(0,0,0);
        }

    }


}
