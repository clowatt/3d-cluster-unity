using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class StarTypes : MonoBehaviour
{

    public static float solarMass = 1.989E30f; //kg
    public static float solarRadius = 6.955E5f; //km times kmToPc
    public static float solarLuminosity = 4.4f; // Mv in MK table

    // Start is called before the first frame update
    void Start()
    {
        
        // Since the units are very large, we'll be using the same size for all objects.
        gameObject.transform.localScale = new Vector3(0.05f ,0.05f, 0.05f);

        // if the tag is '1' or '2' then use Main Sequence
        // if the tag is 3 then use White Dwarf
        // if the tag is 4 then use Neutron Star
        // if the tag is 5 then use Black Hole
        if (gameObject.CompareTag("MS") || gameObject.CompareTag("ES"))
        {
            MainSequenceStar();
        } 
        else if (gameObject.CompareTag("WD"))
        {
            WhiteDwarf();
        } 
        else if (gameObject.CompareTag("NS"))
        {
            NeutronStar();
        }
        else if (gameObject.CompareTag("BH"))
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
        Debug.Log("Main Sequence");

        // Adressable Asset: G_Type_Star_MS
        string starMaterialAddress = "G_Type_Star_MS";
        Addressables.LoadAssetAsync<Material>(starMaterialAddress).Completed += OnLoadDone;
        
    }

    void WhiteDwarf()
    {
        Debug.Log("WhiteDwarf");

        // Adressable Asset: WhiteDwarf
        string starMaterialAddress = "WhiteDwarf";
        Addressables.LoadAssetAsync<Material>(starMaterialAddress).Completed += OnLoadDone;
    }

    void NeutronStar()
    {
        Debug.Log("NeutronStar");
        // Radius will be the smallest possible; so 0.01
        //gameObject.transform.localScale = new Vector3(0.01f ,0.01f, 0.01f);

        // Adressable Asset: Neutron Star
        string starMaterialAddress = "Neutron Star";
        Addressables.LoadAssetAsync<Material>(starMaterialAddress).Completed += OnLoadDone;
    }

    void BlackHole()
    {
        Debug.Log("BlackHole");

        // Adressable Asset: Black_Hole
        string starMaterialAddress = "Black_Hole";
        Addressables.LoadAssetAsync<Material>(starMaterialAddress).Completed += OnLoadDone;

    }

    private void OnLoadDone(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Material> obj)
    {
        // Get the mesh render to modify the material
        MeshRenderer my_renderer = GetComponent<MeshRenderer>();
        if ( my_renderer != null )
        {
            my_renderer.material = obj.Result;
        } 
    }

}
