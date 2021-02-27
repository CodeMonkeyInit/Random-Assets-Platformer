using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using GameObjects;

namespace Checkpoint
{

    public class CheckpointController : BasicGameObject
    {
        private static int? currentCheckpoint;
        private GameObjectSpawner gameObjectSpawner;

        private async void Start()
        {
            //TODO hack remove later
            await Task.Delay(TimeSpan.FromMilliseconds(200));
            
            if (gameObjectSpawner == null)
            {
                gameObjectSpawner = FindObjectOfType<GameObjectSpawner>();
            }

            gameObjectSpawner[currentCheckpoint].Spawn();
        }

        public static void SetCheckpoint(int checkpointID)
        {
            currentCheckpoint = checkpointID;
        }

        public static void ResetCheckpoint()
        {
            currentCheckpoint = 0;
        }
    }
}
