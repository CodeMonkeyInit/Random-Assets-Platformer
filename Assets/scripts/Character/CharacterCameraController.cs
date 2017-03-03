using System;
using UnityEngine;
using LevelGenerator;
using UnityStandardAssets._2D;

namespace Character
{
    public class CharacterCameraController : MonoBehaviour
    {
        private Camera2DFollow camera;

        private void Start()
        {
            camera = GameObject.FindObjectOfType<Camera2DFollow>();
            camera.target = GetComponentInParent<MainCharacter>().transform;
        }

        private void OnDestroy()
        {
            camera.target = null;
        }
    }
}

