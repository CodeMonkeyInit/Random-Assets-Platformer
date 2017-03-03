using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                gameObjectSpawner = GameObject.FindObjectOfType<GameObjectSpawner>();
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
