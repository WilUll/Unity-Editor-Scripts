using System.Collections;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TextureImporterEditor : AssetPostprocessor
{
    public void OnPostprocessTexture(Texture2D texture)
    {
        string lowerCaseAssetPath = assetPath.ToLower();
        if (lowerCaseAssetPath.Contains("_normal"))
        {
            TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            textureImporter.textureType = TextureImporterType.NormalMap;
        }

        string nameOfFile = assetPath.Substring(assetPath.LastIndexOf('/') + 1);
        nameOfFile = nameOfFile.Substring(nameOfFile.LastIndexOf('_') + 1);

        string folderName = assetPath.Remove(assetPath.LastIndexOf('/'));
        Debug.Log(folderName);

        folderName = folderName.Substring(folderName.LastIndexOf('/') + 1);

        nameOfFile = "T_" + folderName + "_" + nameOfFile;


        string fileLocation = assetPath.Remove(assetPath.LastIndexOf('/')) + "/" + nameOfFile;

        File.Move(assetPath, fileLocation);
    }
}
