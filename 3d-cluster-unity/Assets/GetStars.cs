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
            Debug.Log(fullFile);
            var lines = fullFile.Split('\n');
            Debug.Log(lines[0]);

            for (int i = 0; i < lines.Length; i++)
            {
                var singleLine = lines[i].Split(',');
                if (i != 0)
                {
                    Debug.Log(singleLine[0]);
                    // Beware! y is in the vertical in unity, not z
                    float xL = float.Parse(singleLine[0]);
                    float yL = float.Parse(singleLine[2]);
                    float zL = float.Parse(singleLine[1]);
                    InitializeStarPrefab(xL, yL, zL);
                }
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
