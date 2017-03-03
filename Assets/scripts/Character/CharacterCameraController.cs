using System;
using UnityEngine;
using LevelGenerator;
using UnityStandardAssets._2D;

namespace Character
{
    public class CharacterCameraController : MonoBehaviour
    {
        private void Start()
        {
            GameObject.FindObjectOfType<Camera2DFollow>().target = GetComponentInParent<MainCharacter>().transform;
        }
    }
}

