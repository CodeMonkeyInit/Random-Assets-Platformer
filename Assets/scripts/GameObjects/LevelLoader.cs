using System;
using System.IO;
using DefaultNamespace;
using Level;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private LevelGenerator[] levelGeneratorPrefabs;
    
    // Start is called before the first frame update
    void Start()
    {
        byte[] worldPng = File.ReadAllBytes(@"C:\Users\Den\Random-Assets-Platformer\Assets\maps\World 1-1M.png");

        var texture = new Texture2D(0, 0);
        
        texture.LoadImage(worldPng);

        Globals.level = texture;

        SceneManager.LoadSceneAsync("Level");
    }
}
