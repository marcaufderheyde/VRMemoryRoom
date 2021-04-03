using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanoramaCapture : MonoBehaviour
{
    public Camera targetCamera;
    public RenderTexture cubeMapLeft;
    public RenderTexture equirectRT;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Capture();
        }
    }

    public void Capture()
    {
        targetCamera.RenderToCubemap(cubeMapLeft);
        cubeMapLeft.ConvertToEquirect(equirectRT);
        Save(equirectRT);
    }

    public void Save(RenderTexture rt)
    {
        Texture2D tex = new Texture2D(rt.width, rt.height);

        RenderTexture.active = rt;
        Debug.Log("Should be reading pixels now...");
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        RenderTexture.active = null;
        Debug.Log("Should be encoding to JPG now...");
        byte[] bytes = tex.EncodeToJPG();

        string path = Application.dataPath + "/Panorama2" + ".jpg";
        Debug.Log("Should be writing to file now...");
        System.IO.File.WriteAllBytes(path, bytes);
    }
}
