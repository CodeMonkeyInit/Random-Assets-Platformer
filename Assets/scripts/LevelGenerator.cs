using System;
using System.Collections.Generic;
using UnityEngine;
using GameInput;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Level
{

    [Serializable]
    struct ColorToPrefab
    {
        public Color32 color;
        public GameObject prefab;
    }

    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField]
        private Texture2D level;

        [SerializeField]
        private ColorToPrefab[] colorPrefabsArray;

        private Dictionary<Color32, GameObject> colorAndPrefabs;

        public int LevelHeight
        {
            get { return level.height; }
        }
        public int LevelWidth
        {
            get { return level.width; }
        }

        private void EmptyMap()
        {
            while (transform.childCount > 0)
            {
                Transform child = transform.GetChild(0);

                child.SetParent(null);
                Destroy(child.gameObject);
            }
        }

        private void FillDictionary()
        {
            foreach (var colorPrefab in colorPrefabsArray)
            {
                colorAndPrefabs.Add(colorPrefab.color, colorPrefab.prefab);
            }
            colorPrefabsArray = null;
        }

        private void SpawnPrefab(Color32 color, Vector2 position)
        {
            // if element not trasperent or alpha > 0
            if (color.a > 0)
            {
                if (colorAndPrefabs.ContainsKey(color))
                {
                    GameObject prefab = (GameObject)Instantiate(colorAndPrefabs[color], position, Quaternion.identity);
                    prefab.transform.SetParent(this.transform);
                    return;
                }
                Debug.LogWarning("Prefab not found " + color);
            }
			
        }

        private void LoadMap()
        {
            Color32[] prefabsPositions = level.GetPixels32();
            EmptyMap();

            for (int x = 0; x < LevelWidth; x++)
            {
                for (int y = 0; y < LevelHeight; y++)
                {
                    SpawnPrefab(prefabsPositions[(LevelWidth * y) + x], new Vector2(x, y));
                }
            }
        }

        // Use this for initialization
        private void Start()
        {
            
            colorAndPrefabs = new Dictionary<Color32, GameObject>();
            FillDictionary();
            LoadMap();
        }

        private IEnumerator Reload(int seconds)
        {
            yield return new WaitForSeconds(seconds);

			//FIXME properLoader
            SceneManager.LoadScene(0);
        }

        public void Restart(int seconds)
        {
            StartCoroutine(Reload(seconds));
        }
    }
}