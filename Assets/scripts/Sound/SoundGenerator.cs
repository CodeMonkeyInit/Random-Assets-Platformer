using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{

	public class SoundGenerator : MonoBehaviour
	{
		private const int LevelLoopMusic = 0;
		private const int LevelOtherMusic = 1;

		[SerializeField]
		private AudioClip levelStart;

		[SerializeField]
		private AudioClip levelFinished;

		[SerializeField]
		private AudioClip playerDead;

		[SerializeField]
		private AudioClip playerLostHisLives;

		private AudioSource[] audioSource;

		private bool levelIsStarted;

		private bool playersDead;

		private void StopLoopingMusic()
		{
			audioSource[LevelLoopMusic].Stop();
			audioSource[LevelLoopMusic].mute = true;
		}

		void Start()
		{
			audioSource = GetComponentsInChildren<AudioSource>();
			audioSource[LevelOtherMusic].PlayOneShot(levelStart);
		}

		void Update()
		{
			if (!levelIsStarted && !audioSource[LevelOtherMusic].isPlaying)
			{
				audioSource[LevelLoopMusic].loop = true;
				audioSource[LevelLoopMusic].Play();
				levelIsStarted = true;
			}

			if (playersDead && !audioSource[LevelOtherMusic].isPlaying)
			{
				audioSource[LevelOtherMusic].PlayOneShot(playerLostHisLives);
			}
		}

		public void PlayPlayerIsDead(bool noLivesLeft = false)
		{
			StopLoopingMusic();
			audioSource[LevelOtherMusic].PlayOneShot(playerDead);

			playersDead = noLivesLeft;
		}

		public void PlayLevelFinished()
		{
			StopLoopingMusic();

			audioSource[LevelOtherMusic].PlayOneShot(levelFinished);
		}
	}
}
