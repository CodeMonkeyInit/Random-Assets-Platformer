using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Character;
using Checkpoint;

public class HealthController : MonoBehaviour
{
    private Text healthCount;
    // Use this for initialization
    void Start()
    {
        healthCount = GetComponent<Text>();
    }
	
    // Update is called once per frame
    void Update()
    {
        int lives = 0;

        foreach (MainCharacter character in CustomCharacterController.activeCharacters)
        {
            lives += character.Health;
        }

        healthCount.text = lives.ToString();
    }
}
