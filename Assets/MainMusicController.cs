using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class MainMusicController : MonoBehaviour
{

    [SerializeField]
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat(Constants.mainMusicLevelName);
    }
}
