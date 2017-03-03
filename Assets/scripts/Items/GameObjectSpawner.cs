using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using LevelGenerator;

public class GameObjectSpawner : BasicGameObject
{
    private static  SortedDictionary<int, GameObjectSpawner> spawnerDictionary = new SortedDictionary<int, GameObjectSpawner>();

    public GameObject prefabToSpawn;

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

        spawnerDictionary.Add(id, this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        spawnerDictionary.Remove(id);
    }

    public void Spawn()
    {
        GameObject prefab = (GameObject) Instantiate(prefabToSpawn, transform.transform.position, Quaternion.identity);
        prefab.transform.SetParent(GetComponentInParent<GenerateLevel>().transform);
    }
}
