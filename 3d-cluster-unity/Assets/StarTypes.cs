using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTypes : MonoBehaviour
{

    public static float solarMass = 1.989E30f; //kg
    public static float solarRadius = 6.955E5f * GetStars.kmToPc; //km times kmToPc
    public static float solarLuminosity = 4.4f; // Mv in MK table

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(gameObject.tag[0]);
        // if the tag is '1' or '2' then use Main Sequence
        // if the tag is 3 then use White Dwarf
        // if the tag is 4 then use Neutron Star
        // if the tag is 5 then use Black Hole
        if (gameObject.tag[0] == '1' || gameObject.tag[0] == '2')
        {
            MainSequenceStar();
        } 
        else if (gameObject.tag[0] == '3')
        {
            WhiteDwarf();
        } 
        else if (gameObject.tag[0] == '4')
        {
            NeutronStar();
        }
        else if (gameObject.tag[0] == '5')
        {
            BlackHole();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Main Sequence Stars
    // This is the tag '1' and for now it's also the tag '2'
    // the type of main sequence star is based on the mass
    // and using the MK spectral types to determine mass, radius
    // colour and luminosity
    void MainSequenceStar()
    {
        Debug.Log("Here");
    }

    void WhiteDwarf()
    {
        Debug.Log("Here");
    }

    void NeutronStar()
    {
        Debug.Log("Here");
    }

    void BlackHole()
    {
        Debug.Log("Here");
    }

}
