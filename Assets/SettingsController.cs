using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField]
    private Slider mainLoopVolumeSlider;
    
    void Start()
    {
        if (PlayerPrefs.HasKey(Constants.mainMusicLevelName))
        {
            mainLoopVolumeSlider.value = PlayerPrefs.GetFloat(Constants.mainMusicLevelName);
        }
        else
        {
            PlayerPrefs.SetFloat(Constants.mainMusicLevelName, mainLoopVolumeSlider.value);
            PlayerPrefs.Save();
        }
    }

    public void MainLoopVolumeSliderChanged()
    {
        PlayerPrefs.SetFloat(Constants.mainMusicLevelName, mainLoopVolumeSlider.value);
    }
}
