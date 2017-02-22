using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using GameInput;

namespace LevelGenerator
{
    public delegate void OnCharacterCreation();

    [Serializable]
    struct ColorToPrefab
    {
        public Color32 color;
        public GameObject prefab;
    }

    public class GenerateLevel : MonoBehaviour
    {
        static public OnCharacterCreation OnMainCharacterCreated;
        [SerializeField]
        private Texture2D level;

        [SerializeField]
        private ColorToPrefab[] colorPrefabsArray;

        private Dictionary<Color32, GameObject> colorAndPrefabs;

        private bool mainCharacterCreated;

        private static readonly string mainCharacterName = "MainCharacter(Clone)";

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

                    if (!mainCharacterCreated && prefab.name == mainCharacterName)
                    {
                        mainCharacterCreated = true;
                        OnMainCharacterCreated();
                    }
                    return;
                }
                Debug.LogError("Prefab not found " + color);
            }
			
        }

        private void LoadMap()
        {
            Color32[] prefabsPositions = level.GetPixels32();
            int width = level.width;
            int height = level.height;
            EmptyMap();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    SpawnPrefab(prefabsPositions[(width * y) + x], new Vector2(x, y));
                }
            }
        }

        // Use this for initialization
        void Start()
        {
            mainCharacterCreated = false;
            colorAndPrefabs = new Dictionary<Color32, GameObject>();
            FillDictionary();
            LoadMap();
        }
    }
}