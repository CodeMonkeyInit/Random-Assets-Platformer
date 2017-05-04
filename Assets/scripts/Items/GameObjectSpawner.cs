using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Level;
using GameObjects;

public class GameObjectSpawner : BasicGameObject
{
    private static  SortedDictionary<int, GameObjectSpawner> spawnerDictionary = new SortedDictionary<int, GameObjectSpawner>();

    private int prefabsSpawned;

    public GameObject prefabToSpawn;

    public int prefabsToSpawnCount = 1;

    public GameObjectSpawner this [int? i]
    { 
        get 
        { 
            if (i == null)
            {
                return spawnerDictionary.FirstOrDefault().Value;
            }
            return spawnerDictionary[(int) i]; 
        } 
    }

    protected override void Awake()
    {
        base.Awake();

        prefabsSpawned = 0;
        spawnerDictionary.Add(id, this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        spawnerDictionary.Remove(id);
    }

    public void Spawn()
    {
        while (prefabsSpawned != prefabsToSpawnCount)
        {
            GameObject prefab = (GameObject) Instantiate(prefabToSpawn, transform.transform.position, Quaternion.identity);
			prefab.transform.SetParent(GetComponentInParent<LevelGenerator>().transform);
            prefabsSpawned++;
        }
    }
}
