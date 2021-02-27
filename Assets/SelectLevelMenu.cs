using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelMenu : MonoBehaviour
{
    public LevelLoader levelLoader;

    public GameObject levelUrlInput;

    private TMP_Text levelUrlText;

    private void Start()
    {
        levelUrlText = levelUrlInput.GetComponentsInChildren<TMP_Text>().First(text => text.name == "Text");
    }

    public void LoadDefaultLevel()
    {
        Globals.level = null;
        
        LoadLevel();
    }

    public async void LoadLevelFromUrlAsync()
    {
        string url = levelUrlText.text;
        
        if (string.IsNullOrWhiteSpace(url))
        {
            return;
        }

        using var httpClient = new HttpClient();

        var httpResponseMessage = await httpClient.GetAsync(url);

        if (!httpResponseMessage.IsSuccessStatusCode) 
            //TODO add somekind of alert
            return;
        
        var imageBytes = await httpResponseMessage.Content.ReadAsByteArrayAsync();
            
        LoadLevel(imageBytes);
        
    }
    
    void LoadLevel(byte[] textureData = null)
    {
        if (textureData != null)
        {
            var texture2D = new Texture2D(0, 0);
            
            texture2D.LoadRawTextureData(textureData);

            Globals.level = texture2D;
        }
        
        Instantiate(levelLoader);
    }
}
