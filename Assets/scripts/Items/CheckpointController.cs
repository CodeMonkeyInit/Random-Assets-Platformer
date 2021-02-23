using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameObjects;

namespace Checkpoint
{

    public class CheckpointController : BasicGameObject
    {
        private static int? currentCheckpoint;
        private GameObjectSpawner gameObjectSpawner;

        private void Start()
        {
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
