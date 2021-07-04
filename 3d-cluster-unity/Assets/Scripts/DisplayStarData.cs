using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStarData : MonoBehaviour
{
    GameObject distanceTextElement; 
    GameObject getStars;
    float pcConversion;


    // Start is called before the first frame update
    void Start()
    {
        // Get the Text elemeny
        distanceTextElement = GameObject.Find("LocalCameraData");

        // Get the unit distance that this started with 
        GameObject initializeStars = GameObject.Find("InitializeStars");
        GetStars getStars = initializeStars.GetComponent<GetStars>();
        
        // Determine how much one unit is in parsecs
        pcConversion = getStars.pcUnits;


    }

    // Update is called once per frame
    void Update()
    {
        UpdateDistanceText();   
    }


    void UpdateDistanceText()
    {

        // Find the distance from the centre of the star cluster
        float xSquared = transform.position.x * transform.position.x * pcConversion * pcConversion;
        float ySquared = transform.position.y * transform.position.y * pcConversion * pcConversion;
        float zSquared = transform.position.z * transform.position.z * pcConversion * pcConversion;
        double distancePc = System.Math.Round(Mathf.Sqrt( xSquared + ySquared + zSquared), 3);
    
        // Define the string for the Distance: line
        string distancePcText = "Distance: " + distancePc.ToString() + " (pc)";

        // Get the radius needed for the sphere to be 1 parsec
        float pcSphereRadius = 1 / pcConversion;
        float pcVolume = (4 * Mathf.PI) /  3;

        // Get the list of all colliders (spheres) in the volume
        Collider[] starsInSphere = Physics.OverlapSphere(transform.position, pcSphereRadius); 
        
        // Determine the total mass in the sphere
        float solarMass = 0;
        foreach (var starInSphere in starsInSphere)
        {
            solarMass += starInSphere.GetComponentInParent<Rigidbody>().mass;
        }

        // Calculate the mass density
        double massDensity = System.Math.Round((solarMass / pcVolume), 3);

        // Define the string for mass Density
        string massDensityText = "Mass Density: " + massDensity.ToString();

        // Update the text element to display the distance from centre
        // as well as the mass density
        distanceTextElement.GetComponent<Text>().text = distancePcText + "\n" + massDensityText;
    }
}
