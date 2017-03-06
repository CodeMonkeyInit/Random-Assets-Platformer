using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameInput;
using LevelGenerator;

namespace Character
{
    public class CustomCharacterController : BasicGameObject
    {
        private static int playersCount;
        private CharacterCameraController cameraController;
        private CustomInput input;
        private GenerateLevel levelGenerator;

        public CharacterCoinController coinController;
        public MainCharacter character;


        // Use this for initialization
        protected void Start()
        {
            playersCount++;
            coinController = new CharacterCoinController();
            levelGenerator = GameObject.FindObjectOfType<GenerateLevel>();
            input = InputFactory.GetInput();
            character = GetComponentInParent<MainCharacter>();
            //FIXME wtf
            cameraController = character.GetComponentInChildren<CharacterCameraController>();
        }

        // Update is called once per frame
        protected void Update()
        {
            UserInput userInput = input.GetInput();

            if (coinController.CoinCount > 100)
            {
                coinController.Reset();
                character.AddLives(1);
            }

            if (character.IsDead)
            {
                if (cameraController != null)
                {
                    Debug.Log("Camera found");
                    Destroy(this.cameraController);
                }

            }

            character.Move(userInput.move, userInput.status);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            playersCount--;
            input.Destroy();

            if (playersCount == 0)
            {
                levelGenerator.Restart(0);
            }
        }
    }
}