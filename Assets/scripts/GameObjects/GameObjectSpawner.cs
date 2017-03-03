using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelGenerator;

public class GameObjectSpawner : MonoBehaviour
{
    private static Dictionary<int, GameObjectSpawner> spawnerDictionary = new Dictionary<int, GameObjectSpawner>();
    private static int instanceCount;

    protected int id;

    public GameObject prefabToSpawn;
    public int ID { get { return id; } }
    public GameObjectSpawner this [int i]
    { 
        get { return spawnerDictionary[i]; } 
    }

    protected virtual void Awake()
    {
        id = instanceCount;
        instanceCount++;
        spawnerDictionary.Add(id, this);
    }

    protected virtual void OnDestroy()
    {
        instanceCount = 0;
        spawnerDictionary.Remove(id);
    }

    public void Spawn()
    {
        GameObject prefab = (GameObject) Instantiate(prefabToSpawn, transform.transform.position, Quaternion.identity);
        prefab.transform.SetParent(GetComponentInParent<GenerateLevel>().transform);
    }
}
