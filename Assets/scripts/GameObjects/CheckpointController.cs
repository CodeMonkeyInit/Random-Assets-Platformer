using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Checkpoint
{

    public class CheckpointController : MonoBehaviour
    {
        private static int currentCheckpoint = 0;
        private GameObjectSpawner gameObjectSpawner;

        private void Start()
        {
            if (gameObjectSpawner == null)
            {
                gameObjectSpawner = GameObject.FindObjectOfType<GameObjectSpawner>();
            }

            gameObjectSpawner[currentCheckpoint].Spawn();
        }

        public static void SetCheckpoint(int id)
        {
            currentCheckpoint = id;
        }

        public static void ResetCheckpoint()
        {
            currentCheckpoint = 0;
        }
    }
}
