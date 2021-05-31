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

    // Set the conversion from pc to km
    public static float kmToPc = 3.24078E-14f;

    [SerializeField] string assetFileLocation = "Assets/DataFiles/snapshot.txt";
    
    // timeScale will multiply how many seconds a step should be for velocity
    [SerializeField] float timeScale = 3.1536E11f; // seconds in 10000 years
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
    void InitializeStar(Vector3 starPosition, Vector3 starVelocity, int i)
    {
        GameObject newStar;
        Rigidbody starVel;
        newStar = (GameObject)Instantiate(Star, starPosition, Quaternion.identity);
        starVel = newStar.GetComponent<Rigidbody>();
        starVel.velocity = starVelocity;
        newStar.name = "Star_" + i;

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
            // only get 5% of stars:
            //for (int i = 1; i < ines.Length - 1; i+=20)
            //for (int i = 1; i < 200; i++)
            //for (int i = 1; i < lines.Length - 1; i++)
            {
                
                var singleLine = lines[i].Split(',');

                // Beware! y is in the vertical in unity, not z
                Vector3 starPosition;
                starPosition.x = float.Parse(singleLine[0]); // x in file               
                starPosition.y = float.Parse(singleLine[2]); // z in file   
                starPosition.z = float.Parse(singleLine[1]); // y in file  

                // Beware! y is in the vertical in unity, not z
                //Vector3 starVelocity;
                //starVelocity.x = float.Parse(singleLine[3]) * kmToPc *timeScale; //vx in file
                //starVelocity.y = float.Parse(singleLine[5]) * kmToPc *timeScale; //vz in file
                //starVelocity.z = float.Parse(singleLine[4]) * kmToPc *timeScale; //vy in file

                //InitializeStar(xL, yL, zL, xV, yV, zV);
                InitializeStar(starPosition, starVelocity, i);
                // Check for max raidus
                currentRadius = Mathf.Sqrt( (starPosition.x*starPosition.x) + (starPosition.y*starPosition.y) + (starPosition.z*starPosition.z));

                if (currentRadius > clusterMaxRadius)
                {
                    clusterMaxRadius = currentRadius;
                }

                // Update the camera location to be at the max radius
                
                //mainCamera = GameObject.Find("Main Camera");
                //mainCamera.transform.position = new Vector3(0, 0, -clusterMaxRadius);
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
            Vector3 starPosition = new Vector3(0,0,0);
            Vector3 starVelocity = new Vector3(0,0,0);
            InitializeStar(starPosition, starVelocity, 0);
        }

    }


}
