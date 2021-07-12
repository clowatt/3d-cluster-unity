using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets; 
using UnityEngine.VFX;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GetVFXParticles : MonoBehaviour
{
    // Location of addressable asset
    [SerializeField] string assetFileLocation = "ToySnapshot";
    

    // Start is called before the first frame update
    void Start()
    {
        Addressables.LoadAssetAsync<TextAsset>(assetFileLocation).Completed += onLoadDone;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onLoadDone(AsyncOperationHandle<TextAsset> obj){
        string fullFile = obj.Result.text;
        string[] particleData = fullFile.Split('\n');
        int particleCount = particleData.Length -2;
    
        Texture2D texture = new Texture2D(particleCount, 1, TextureFormat.RFloat, false);
        Color[] positions = new Color[particleCount];
        
        // update the positions based on each particle data, the first line is the header
        for (int i = 1; i < particleCount +1; i++)
        {
            var singleLine = particleData[i].Split(',');
            var starPositionX = float.Parse(singleLine[0]); // x in file               
            var starPositionY = float.Parse(singleLine[2]); // z in file   
            var starPositionZ = float.Parse(singleLine[1]); // y in file 
            positions[i] = new Color(starPositionX, starPositionY, starPositionZ, 0);
        }

        texture.SetPixels(positions);
        texture.Apply();
        
        var vfx = GetComponent<VisualEffect>();
        vfx.SetTexture("ExposedTextureProperty", texture);
        
    }
}
