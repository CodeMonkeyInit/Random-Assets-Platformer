using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameInput;
using Level;
using GameObjects;
using Sound;

namespace Character
{
    public class CustomCharacterController : BasicGameObject
    {
        [SerializeField]
        private GameObject mobileInput;

        [SerializeField]
        private float PathLengthInLevelEnd = 300f;
        [SerializeField]
        private static readonly float PlayerFullspeed = 1f;

        private static int playersCount;
        private CharacterCameraController cameraController;
        private CustomInput input;
		private LevelGenerator levelGenerator;
        private float autoWalkLength;

        private bool levelCompleted;

		private bool characterDied;

		private bool playerFinishedLevel;
            
        public static List<MainCharacter> activeCharacters;
        public CharacterCoinController coinController;
        public MainCharacter character;

		private void PlayerFinishedLevel()
		{
			if(!playerFinishedLevel)
			{
				SoundGenerator soundGenerator = GameObject
					.FindGameObjectsWithTag("MusicGenerator")[0]
					.GetComponent<SoundGenerator>();
				soundGenerator.PlayLevelFinished();
				playerFinishedLevel = true;
			}
		}

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
			levelGenerator = GameObject.FindObjectOfType<LevelGenerator>();
            input = InputFactory.GetInput(playersCount, mobileInput);
            character = GetComponentInParent<MainCharacter>();
            activeCharacters.Add(character);

            //FIXME wtf
            cameraController = character.GetComponentInChildren<CharacterCameraController>();
        }

		protected void FixedUpdate()
		{
			if (levelCompleted)
			{
				if (activeCharacters.Count == 0)
				{
					PlayerFinishedLevel();

					FinishLevel();
				} else
				{
					//TODO remove
					character.Move(0f, CharacterStatus.Normal);
				}
			}
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

				if (character.IsDead & !characterDied)
                {
					SoundGenerator soundGenerator = GameObject
						.FindGameObjectsWithTag("MusicGenerator")[0]
						.GetComponent<SoundGenerator>();

					if (character.Health <= 0)
					{
						soundGenerator.PlayPlayerIsDead(true);
					}
					else
					{
						soundGenerator.PlayPlayerIsDead();
					}

                    if (cameraController != null)
                    {
                        Destroy(this.cameraController);
                    }

					characterDied = true;
                }

                character.Move(userInput.move, userInput.status);
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
	        if (levelCompleted) 
		        return;
	        
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