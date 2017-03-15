using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameInput;
using LevelGenerator;
using Coins;

namespace Character
{
    public class CustomCharacterController : BasicGameObject
    {
        [SerializeField]
        private GameObject mobileInput;

        private static int playersCount;
        private CharacterCameraController cameraController;
        private CustomInput input;
        private GenerateLevel levelGenerator;


        public static List<MainCharacter> activeCharacters;
        public CharacterCoinController coinController;
        public MainCharacter character;


        // Use this for initialization
        protected void Start()
        {
            activeCharacters = new List<MainCharacter>();
            playersCount++;
            coinController = new CharacterCoinController();
            levelGenerator = GameObject.FindObjectOfType<GenerateLevel>();
            input = InputFactory.GetInput(mobileInput);
            character = GetComponentInParent<MainCharacter>();
            activeCharacters.Add(character);

            //FIXME wtf
            cameraController = character.GetComponentInChildren<CharacterCameraController>();
        }

        // Update is called once per frame
        protected void Update()
        {
            UserInput userInput = input.GetInput();

            if (CharacterCoinController.CoinCount > 100)
            {
                coinController.Reset();
                character.AddLives(1);
            }

            if (character.IsDead)
            {
                if (cameraController != null)
                {
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
            activeCharacters.Remove(character);

            if (playersCount == 0)
            {
                levelGenerator.Restart(0);
            }
        }
    }
}