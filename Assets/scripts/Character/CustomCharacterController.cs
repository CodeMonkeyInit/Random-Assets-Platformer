using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameInput;
using LevelGenerator;
using UnityEngine.SceneManagement;

namespace Character
{
    public class CustomCharacterController : MonoBehaviour
    {
        private static int instanceCount;
        private CustomInput input;
        private GenerateLevel levelGenerator;


        public CharacterCoinController coinController;
        public MainCharacter character;


        // Use this for initialization
        private void Start()
        {
            instanceCount++;
            coinController = new CharacterCoinController();
            levelGenerator = GameObject.FindObjectOfType<GenerateLevel>();
            input = InputFactory.GetInput();
            character = GetComponentInParent<MainCharacter>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (coinController.CoinCount > 100)
            {
                coinController.Reset();
                character.AddLives(1);
            }

            if (character == null)
            {
                Debug.LogError("Dead");
                levelGenerator.Restart(4);
            }
            UserInput userInput = input.GetInput();
            character.Move(userInput.move, userInput.status);
        }

        private void OnDestroy()
        {
            instanceCount--;
            input.Destroy();

            if (instanceCount == 0)
            {
                levelGenerator.Restart(0);
            }
        }
    }
}