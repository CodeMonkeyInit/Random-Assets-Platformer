using System;
using UnityEngine;
using LevelGenerator;
using Camera2D;

namespace Character
{
    public class CharacterCameraController : MonoBehaviour
    {
        private MultiPlayerCamera levelCamera;
        private MainCharacter charater;
        private GenerateLevel levelGenerator;


        [SerializeField]
        private float cameraOffsetX = 0;
        [SerializeField]
        private float cameraOffsetY = 0;

        private void SetCameraConstrains()
        {
            if (Camera.main != null)
            {
                float halfCameraHeight = Camera.main.orthographicSize;
                float halfCameraWidth = halfCameraHeight * Camera.main.aspect;

                levelCamera.SetCameraConstrains(
                    halfCameraWidth + cameraOffsetX, 
                    halfCameraHeight + cameraOffsetY, 
                    levelGenerator.LevelWidth - halfCameraWidth, 
                    levelGenerator.LevelHeight - halfCameraHeight);
            }
        }

        private void Start()
        {
            if (levelCamera == null)
            {   
                levelCamera = GameObject.FindObjectOfType<MultiPlayerCamera>();
            }
            if (levelGenerator == null)
            {
                levelGenerator = GameObject.FindObjectOfType<GenerateLevel>();
            }
            if (charater == null)
            {
                charater = gameObject.GetComponentInParent<MainCharacter>();
            }

            SetCameraConstrains();
            levelCamera.AttachPlayer(charater.transform);
        }

        private void OnDestroy()
        {
            if (levelCamera != null && charater != null)
            { 
                levelCamera.DetachPlayer(charater.transform);
            }
        }
    }
}

