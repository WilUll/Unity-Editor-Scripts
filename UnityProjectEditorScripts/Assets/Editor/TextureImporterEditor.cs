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

        /*
            !!! THIS WILL ONLY WORK ON FILES WITH THE NAMING CONVENTION OF _FILETYPE.png AT THE END EX: "EXAMPLE_File_Normal.png" !!!
            !!! THE FILE WILL BE RENAMED TO "T_FOLDERNAME_FILETYPE.png"
        */

        //Gets the last word of the file which in our case was the type of file it was ex. Normal, Albedo
        string nameOfFile = assetPath.Substring(assetPath.LastIndexOf('/') + 1);
        nameOfFile = nameOfFile.Substring(nameOfFile.LastIndexOf('_') + 1);

        //Gets the folder path of where the file was just placed
        string folderName = assetPath.Remove(assetPath.LastIndexOf('/'));

        //Gets the current folder where the file was just placed
        folderName = folderName.Substring(folderName.LastIndexOf('/') + 1);
        Debug.Log(folderName);

        //Creates the new name for the file
        nameOfFile = "T_" + folderName + "_" + nameOfFile;

        //Gets the file location for the file
        string fileLocation = assetPath.Remove(assetPath.LastIndexOf('/')) + "/" + nameOfFile;

        //Saves the file with its new name at the same location
        File.Move(assetPath, fileLocation);
    }
}
