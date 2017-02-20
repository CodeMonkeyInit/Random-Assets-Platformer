using System;
using UnityEngine;
using LevelGenerator;
using UnityStandardAssets._2D;

namespace Character
{
    public class CharacterCameraController : MonoBehaviour
    {
        private void SetCamera()
        {
            GameObject.FindObjectOfType<Camera2DFollow>().target = GameObject.FindObjectOfType<MainCharacter>().transform;
        }

        private void Start()
        {
            GenerateLevel.OnMainCharacterCreated += SetCamera;
        }
    }
}

