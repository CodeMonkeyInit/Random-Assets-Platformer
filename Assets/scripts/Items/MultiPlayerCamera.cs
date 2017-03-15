using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Camera2D
{
    public struct CameraConstraints
    {
        public float minX;
        public float minY;
        public float maxX;
        public float maxY;

        public CameraConstraints(float minX, float minY, float maxX, float maxY)
        {
            this.minX = minX;
            this.minY = minY;
            this.maxX = maxX;
            this.maxY = maxY;
        }
    }

    public class MultiPlayerCamera : BasicGameObject
    {
        [SerializeField]
        private CameraConstraints cameraConstraints;
        [SerializeField]
        private List<Transform> players = new List<Transform>();

        private CameraConstraints CameraUpdateConstraints
        {
            get
            {
                float halfCameraHeight = Camera.main.orthographicSize;
                float cameraPositionY = transform.position.y;

                return new CameraConstraints(
                    0, 
                    cameraPositionY,
                    0, 
                    cameraPositionY + halfCameraHeight); 
            }
        }

        private Vector3 GetSlowestPlayerCoordinates()
        {
            Transform slowestPlayer = players[0];

            for (int i = 1; i < players.Count; i++)
            {
                if (slowestPlayer.position.x > players[i].position.x)
                {
                    slowestPlayer = players[i];
                }
            }
            return slowestPlayer.position;
        }

        protected override void Awake()
        {
            base.Awake();
        }

        protected void LateUpdate()
        {
            if (players.Count > 0)
            {
                Vector3 slowestPlayer = GetSlowestPlayerCoordinates();
                CameraConstraints updateConstraints = CameraUpdateConstraints;

                float xCamera = Mathf.Clamp(slowestPlayer.x, cameraConstraints.minX, cameraConstraints.maxX);
                float yCamera = transform.position.y;

                if (slowestPlayer.y > updateConstraints.maxY || slowestPlayer.y < updateConstraints.minY)
                {
                    yCamera = Mathf.Clamp(slowestPlayer.y, cameraConstraints.minY, cameraConstraints.maxY);
                }

                Vector3 newCameraCoordinates = new Vector3(xCamera, yCamera, transform.position.z);

                transform.position = newCameraCoordinates;
            }
        }

        public void SetCameraConstrains(float minX, float minY, float maxX, float maxY)
        {
            cameraConstraints = new CameraConstraints(minX, minY, maxX, maxY);
        }

        public void SetPlayers<PlayersType>() where PlayersType : MonoBehaviour
        {
            PlayersType[] monoBehaviorObjects = GameObject.FindObjectsOfType<PlayersType>();
            players = new List<Transform>(monoBehaviorObjects.Length);

            foreach (var monoBehaviorObject in monoBehaviorObjects)
            {
                players.Add(monoBehaviorObject.transform);                
            }
        }

        public void AttachPlayer(Transform playerTransform)
        {
            players.Add(playerTransform);
        }

        public void DetachPlayer(Transform playerTraform)
        {
            players.Remove(playerTraform);
        }
    }
}