using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateFolderStructure
{
    private static readonly string projectName = "MechHiveDescent";
    private static readonly string root = "Assets/"+projectName;

    [MenuItem("Tools/Create Project Folder Structure")]
    public static void CreateFolders()
    {
        CreateRoot();

        Create("Art");
        Create("Art/Sprites");
        Create("Art/Textures");
        Create("Art/Materials");
        Create("Art/Shaders");
        Create("Art/VFX");

        Create("Audio");
        Create("Audio/SFX");
        Create("Audio/Music");

        Create("Prefabs");
        Create("Prefabs/Characters");
        Create("Prefabs/Enemies");
        Create("Prefabs/Projectiles");
        Create("Prefabs/Environment");
        
        Create("Scenes");
        Create("Scenes/Levels");
        Create("Scenes/UI");
        Create("Scenes/Menus");

        Create("Scripts");
        Create("Scripts/Core");
        Create("Scripts/Player");
        Create("Scripts/Enemies");
        Create("Scripts/Weapons");
        Create("Scripts/UI");
        Create("Scripts/Utils");

        Create("Data");
        
        Create("Resources"); 
        Create("Docs");
        Create("Editor");
        Create("ThirdParty");

        AssetDatabase.Refresh();
        Debug.Log("Folder structure created!");
    }

    private static void CreateRoot()
    {
        if (!AssetDatabase.IsValidFolder(root))
        {
            AssetDatabase.CreateFolder("Assets", projectName);
        }
    }

    private static void Create(string path)
    {
        string fullPath = $"{root}/{path}";

        if (!AssetDatabase.IsValidFolder(fullPath))
        {
            string parent = Path.GetDirectoryName(fullPath).Replace("\\", "/");
            string newFolder = Path.GetFileName(fullPath);

            AssetDatabase.CreateFolder(parent, newFolder);
        }
    }
}
