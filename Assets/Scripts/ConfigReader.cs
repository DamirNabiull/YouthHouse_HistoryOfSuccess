using System.Collections.Generic;
using System.IO;
using Models;
using UnityEngine;

public class ConfigReader : MonoBehaviour
{
    public static DataConfig AppConfig { get; private set; }
    public static readonly Dictionary<int, Profile> Profiles = new Dictionary<int, Profile>();

    public static void ReadConfig()
    {
        if (AppConfig != null) return; 
        
        var configPath = Application.streamingAssetsPath + "/config.json";
        var fileContents = File.ReadAllText(configPath);
        AppConfig = JsonUtility.FromJson<DataConfig>(fileContents);
    }

    public static void ReadImages()
    {
        foreach (var page in AppConfig.pages)
        {
            ReadFolder(page.idRight);
            ReadFolder(page.idLeft);
        }

        Debug.Log(Profiles.Count);
    }

    private static void ReadFolder(int id)
    {
        var path = id.ToString();
        var imagesPath = Application.streamingAssetsPath + $"/{path}/";

        var previewTex = GetTextureOfImage(imagesPath + "preview.png");
        var profileTex = GetTextureOfImage(imagesPath + "profile.png");

        Profiles.Add(id, new Profile()
        {
            PreviewImage = previewTex,
            ProfileInfoImage = profileTex
        });
    }

    private static Texture2D GetTextureOfImage(string file)
    {
        var www = new WWW(file);
        var tex = new Texture2D(1920, 1080, TextureFormat.RGBA4444, false);
        www.LoadImageIntoTexture(tex);

        return tex;
    }
}