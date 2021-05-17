using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets; 

public class GetStars : MonoBehaviour
{
    // Reference to the Prefab
    public GameObject Star;
    [SerializeField] string assetFileLocation = "Assets/DataFiles/snapshot.txt";
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

    void getFromFile()
    {
        
        Addressables.LoadAssetAsync<TextAsset>(assetFileLocation).Completed += handle =>
        {
            var fullFile = handle.Result.text;
            var lines = fullFile.Split('\n');
            
            // 1 less than lines.Length as the length of the array is 1 longer than the lines in the file
            // Start at 1 as 0 is the header line
            for (int i = 1; i < lines.Length - 1; i++)
            {
                var singleLine = lines[i].Split(',');
                // Beware! y is in the vertical in unity, not z
                float xL = float.Parse(singleLine[0]);
                float yL = float.Parse(singleLine[2]);
                float zL = float.Parse(singleLine[1]);
                InitializeStarPrefab(xL, yL, zL);
            }

            Addressables.Release(handle);
        };
    }

    void getStarsFromFile()
    {

        try
        {
            getFromFile();
        }
        catch
        {
            InitializeStarPrefab(0,0,0);
        }
        

        
    }

}
