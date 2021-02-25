using System;
using System.Collections.Generic;
using UnityEngine;
using GameInput;
using System.Collections;
using DefaultNamespace;
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
        public Texture2D Level;

        [SerializeField] private ColorToPrefab[] colorPrefabsArray;

        private Dictionary<Color32, GameObject> colorAndPrefabs;

        public int LevelHeight
        {
            get { return Level.height; }
        }

        public int LevelWidth
        {
            get { return Level.width; }
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
                    GameObject prefab = Instantiate(colorAndPrefabs[color], position, Quaternion.identity);
                    prefab.transform.SetParent(transform);
                    return;
                }

                Debug.LogWarning("Prefab not found " + color);
            }
        }

        private void LoadMap()
        {
            Color32[] prefabsPositions = Level.GetPixels32();
            EmptyMap();

            for (int x = 0; x < LevelWidth; x++)
            {
                for (int y = 0; y < LevelHeight; y++)
                {
                    SpawnPrefab(prefabsPositions[(LevelWidth * y) + x], new Vector2(x, y));
                }
            }
        }

        private void Start()
        {
            if (Globals.level != null)
                Level = Globals.level;

            colorAndPrefabs = new Dictionary<Color32, GameObject>();
            FillDictionary();

            LoadMap();
        }

        private IEnumerator Reload(int seconds)
        {
            yield return new WaitForSeconds(seconds);

            //FIXME properLoader
            SceneManager.LoadScene("Level");
        }

        public void Restart(int seconds)
        {
            StartCoroutine(Reload(seconds));
        }
    }
}