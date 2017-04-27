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

        [SerializeField]
        private static readonly float PathLengthInLevelEnd = 300f;
        [SerializeField]
        private static readonly float PlayerFullspeed = 1f;

        private static int playersCount;
        private CharacterCameraController cameraController;
        private CustomInput input;
        private GenerateLevel levelGenerator;
        private float autoWalkLength;

        private bool levelCompleted;
            
        public static List<MainCharacter> activeCharacters;
        public CharacterCoinController coinController;
        public MainCharacter character;

        protected void FinishLevel()
        {
            if (autoWalkLength > 0)
            {
                character.Move(PlayerFullspeed, CharacterStatus.Normal);

                autoWalkLength -= character.MaxSpeed * PlayerFullspeed;
            }
            else
            {
                character.Move(0f, CharacterStatus.Normal);
            }
        }


        // Use this for initialization
        protected void Start()
        {
            activeCharacters = new List<MainCharacter>();
            playersCount++;
            coinController = new CharacterCoinController();
            levelGenerator = GameObject.FindObjectOfType<GenerateLevel>();
            input = InputFactory.GetInput(playersCount, mobileInput);
            character = GetComponentInParent<MainCharacter>();
            activeCharacters.Add(character);

            //FIXME wtf
            cameraController = character.GetComponentInChildren<CharacterCameraController>();
        }

        // Update is called once per frame
        protected void Update()
        {
            if (!levelCompleted)
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
            else if (activeCharacters.Count == 0)
            {
                FinishLevel();
            }
            else 
            {
                //TODO remove
                character.Move(0f, CharacterStatus.Normal);
            }
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

        public void SetLevelCompleted()
        {
            if (!levelCompleted)
            {
                levelCompleted = true;

                autoWalkLength = PathLengthInLevelEnd;

                Destroy(cameraController);

                input.Destroy();

                input = null;
                cameraController = null;

                activeCharacters.Remove(character);
            }
        }
    }
}