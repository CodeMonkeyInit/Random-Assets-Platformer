using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject PauseMenu;
    
    private bool gameIsPaused;
    
    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;

        Time.timeScale = gameIsPaused ? 0 : 1;
        
        AudioListener.pause = gameIsPaused;
        
        PauseMenu.SetActive(gameIsPaused);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
