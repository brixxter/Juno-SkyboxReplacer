using ModApi.Mods;
using ModApi.Settings.Core;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering;

public class SkyboxScript : MonoBehaviour
{
    private string[] texNames, formats;
    public string fullPath;
    private Texture2D[] textures;

    // Start is called before the first frame update
    void Start()
    {
        InitializeSkybox();
    }

    void InitializeSkybox()
    {

        texNames = new string[6] { "PositiveZ", "NegativeZ", "PositiveX", "NegativeX", "PositiveY", "NegativeY" };
        formats = new string[3] { ".png", ".jpg", ".dds" };
        textures = new Texture2D[6];


        string gamePath, modPath;

        modPath = "/Mods/SkyboxReplacer/";
        gamePath = Application.persistentDataPath;
        fullPath = gamePath + modPath;

        for (int f = 0; f < formats.Length && !ValidityCheck(); f++)
        {
            for (int i = 0; i <= 5; i++) GetTexture(i, f);
        }
        FinalizeTextures();
    }


    void GetTexture(int i, int f)
    {
        string path = fullPath + texNames[i] + formats[f];
        if (System.IO.File.Exists(path) && textures[i] == null)
        {
            textures[i] = new Texture2D(2, 2);
            textures[i].wrapMode = TextureWrapMode.Clamp;

            var rawData = System.IO.File.ReadAllBytes(path);
            if (rawData != null) textures[i].LoadImage(rawData);
        }
    }


    void FinalizeTextures()
    {
        if (ValidityCheck())
        {
            RenderSettings.skybox.SetTexture("_FrontTex", textures[0]);
            RenderSettings.skybox.SetTexture("_BackTex", textures[1]);
            RenderSettings.skybox.SetTexture("_LeftTex", textures[2]);
            RenderSettings.skybox.SetTexture("_RightTex", textures[3]);
            RenderSettings.skybox.SetTexture("_UpTex", textures[4]);
            RenderSettings.skybox.SetTexture("_DownTex", textures[5]);
        }
    }


    private bool ValidityCheck() //returns true if all of the textures are valid
    {
        for (int i = 0; i <= 5; i++)
        {
            if (textures[i] == null)
            {
                return false;
            }
        }
        return true;
    }
}

