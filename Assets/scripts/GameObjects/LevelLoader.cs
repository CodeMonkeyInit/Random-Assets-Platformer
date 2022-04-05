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
        SceneManager.LoadSceneAsync("Level");
    }
}
