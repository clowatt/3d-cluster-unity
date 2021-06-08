using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets; 

public class GetStars : MonoBehaviour
{

    // Note: The variables for star data are in StarTypes.cs
    // 1 unit is 0.001 parsec
    [SerializeField] float pcUnits = 0.001f;
    // Number of parsecs in a km: 3.24078e-14
    float pcInKm = 3.24078e-14f;


    // We need to be able to know how "far away" the furthest star is from
    // the centre of mass as a way to initialize the camera.
    float clusterMaxRadius;
    // So we will need to be able to grab the Main Camera.
    GameObject mainCamera;

    // But we also want to make it available to start at the center
    [SerializeField] bool cameraStartCenter = true;
    

    // We will be instantiating the Star prefab and allowing StarTypes.cs
    // to modify the different settings of the star
    public GameObject Star;

    
    // The asset file locaiton is the addressable location of the data file
    // This defaults to the toy snapshot included in the package
    [SerializeField] string assetFileLocation = "ToySnapshot";
    

    // To set up the velocity, we need to convert the velocity from km/s to km/yr
    // The number of seconds in a year
    //float secInYear = 3.154e+7f; 
    // We gather the number of years
    //[SerializeField] float timeScale = 1000f; // 1000 years start
    [SerializeField] float timeScale = 3.154e+11f; // 1000 years start
    // In the velocity reading we multiple secInYear by timeScale


    // What % of stars do you want sampled?
    [SerializeField] int stepSize = 1; //the step size determines how many are sampled


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
    void InitializeStar(Vector3 starPosition, Vector3 starVelocity, float starMass, string starType, int i)
    {
        GameObject newStar;
        Rigidbody starVel;
        GameObject starSphere; 
        newStar = (GameObject)Instantiate(Star, starPosition, Quaternion.identity);
        starVel = newStar.GetComponent<Rigidbody>();
        starVel.velocity = starVelocity;
        starVel.mass = starMass;
        newStar.name = "Star_" + i + "_" + starType;
        starSphere = newStar.transform.Find("Star Sphere").gameObject;
        starSphere.tag = starType;


    }


    // This gathers the information from a file and assigns all correct data
    // to the star prefab
    void GetFromFile()
    {
        float convertToKm = pcInKm / pcUnits; //1 km per solar radius
        float convertToPc = 1f / pcUnits; // for 1 parsec we need 1000 units

        // Load the text file from addressable asset location and store in handler
        Addressables.LoadAssetAsync<TextAsset>(assetFileLocation).Completed += handle =>
        {
            var fullFile = handle.Result.text;
            var lines = fullFile.Split('\n');
            float currentRadius;
            clusterMaxRadius = 0f;
            // Initialize in the event that no star lines are valid
            Vector3 starPosition = new Vector3(0, 0, 0);
            Vector3 starVelocity = new Vector3(0, 0, 0);
            float starMass = 1f; 
            string starType = "MS";
                        
            // Start at i= 1 because i = 0 is the header line
            // 1 less than lines.Length as the length of the array 
            //     is 1 longer than the lines in the file
            // the stepSize allows for quickly switching between percents in 
            // the unity editor
            for (int i = 1; i < lines.Length - 1; i+= stepSize)
            {
                
                var singleLine = lines[i].Split(',');
                
                // If the length is 3 or more, then we assume it has position
                if (singleLine.Length > 2)
                {
                    // Beware! y is in the vertical in unity, not z
                    starPosition.x = float.Parse(singleLine[0]) * convertToPc; // x in file               
                    starPosition.y = float.Parse(singleLine[2]) * convertToPc; // z in file   
                    starPosition.z = float.Parse(singleLine[1]) * convertToPc; // y in file  
                }

                // If the length is 6 or more, then we assume it has velocity
                if (singleLine.Length > 5)
                {
                    //timeScale = timeScale * secInYear;
                    // Beware! y is in the vertical in unity, not z
                    starVelocity.x = float.Parse(singleLine[3]) * convertToKm *timeScale; //vx in file
                    starVelocity.y = float.Parse(singleLine[5]) * convertToKm *timeScale; //vz in file
                    starVelocity.z = float.Parse(singleLine[4]) * convertToKm *timeScale; //vy in file
                }

                // If the length is 7 or more, then we assume it has mass and a type
                if (singleLine.Length > 6)
                {
                    starMass = float.Parse(singleLine[6]);
                    if (singleLine[7] == "1.0")
                    {
                        starType = "MS";
                    }
                    else if (singleLine[7] == "2.0")
                    {
                        starType = "ES";
                    }
                    else if (singleLine[7] == "3.0")
                    {
                        starType = "WD";
                    }
                    else if (singleLine[7] == "4.0")
                    {
                        starType = "NS";
                    }
                    else if (singleLine[7] == "5.0")
                    {
                        starType = "BH";
                    }
                    else
                    {
                        // default to main sequence
                        starType = "MS";
                    }

                }

                //InitializeStar(xL, yL, zL, xV, yV, zV);
                InitializeStar(starPosition, starVelocity, starMass, starType, i);
                
                // Check for max raidus
                currentRadius = Mathf.Sqrt( (starPosition.x*starPosition.x) + (starPosition.y*starPosition.y) + (starPosition.z*starPosition.z));

                if (currentRadius > clusterMaxRadius)
                {
                    clusterMaxRadius = currentRadius;
                }

                // Update the camera location to be at the max radius
                
                if (cameraStartCenter == false)
                {
                    mainCamera = GameObject.Find("Main Camera");
                    mainCamera.transform.position = new Vector3(0, 0, -clusterMaxRadius);
                }
                
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
            InitializeStar(starPosition, starVelocity, 1f, "MS", 0);
        }

    }


}
