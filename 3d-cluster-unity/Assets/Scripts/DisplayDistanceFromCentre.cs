using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDistanceFromCentre : MonoBehaviour
{
    GameObject distanceTextElement; 
    GameObject getStars;
    float pcConversion;


    // Start is called before the first frame update
    void Start()
    {
        distanceTextElement = GameObject.Find("DistanceFromCentre");
        GameObject initializeStars = GameObject.Find("InitializeStars");
        GetStars getStars = initializeStars.GetComponent<GetStars>();
        pcConversion = getStars.pcUnits;


    }

    // Update is called once per frame
    void Update()
    {
        UpdateDistanceText();   
    }


    void UpdateDistanceText()
    {

        float xSquared = transform.position.x * transform.position.x * pcConversion * pcConversion;
        float ySquared = transform.position.y * transform.position.y * pcConversion * pcConversion;
        float zSquared = transform.position.z * transform.position.z * pcConversion * pcConversion;
        float distancePc = Mathf.Sqrt( xSquared + ySquared + zSquared);

        distanceTextElement.GetComponent<Text>().text = "Distance: " + distancePc.ToString() + " (pc)";
    }
}
