using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class RendeToImageConverter : MonoBehaviour
{
    [SerializeField] RenderTexture renderTexture;
    [SerializeField] Camera renderCamera;
    [SerializeField] Vector3 PhotoshootPos;
    //[SerializeField] string ImageName = "GirlOutlines";
    int counter = 0;
    //public Texture2D texture;
    string filePath;
    string CurrentClipToCapture;
    private void OnEnable()
    {
        AnimationChanger.OnCaptureImage += OnCapture;
    }
    private void OnDisable()
    {
        AnimationChanger.OnCaptureImage -= OnCapture;
    }
    private void OnCapture(string ClipName)
    {
        CurrentClipToCapture = ClipName;
        counter++;
        SaveRenderTextureAsPNG("E:\\Shivam\\CapturedPngs/" + ClipName + ".png");
    }

    private void LateUpdate()
    {
        /*
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();
        byte[] bytes = texture.EncodeToPNG(); // or EncodeToJPG()
        File.WriteAllBytes("E:\\Shivam\\Puzzle Pose\\CapturedPNG" + "/outlines.png", bytes);
        */
    }
    public void SaveRenderTextureAsPNG(string filePath)
    {
        renderCamera.targetTexture = renderTexture;

        // Render the camera to the Render Texture
        renderCamera.Render();

        // Create a new texture from the Render Texture
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        Color32[] pixels = texture.GetPixels32();
        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i].a == 0)
            {
                pixels[i] = new Color32(0, 0, 0, 0);
            }
        }
        texture.SetPixels32(pixels);
        texture.Apply();
        RenderTexture.active = null;

        // Encode the texture as a PNG file and save it to disk
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);

        // Destroy the temporary texture to free up memory
        Destroy(texture);
    }
   
}
