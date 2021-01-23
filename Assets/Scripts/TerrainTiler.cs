using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TerrainTiler : MonoBehaviour
{

    public GameObject trackedObject;
    public GameObject terrain;
    public int spawnRadius = 3;
    public float height = 0f;
    private Dictionary<Tuple<int, int>, GameObject> spawnedTerrains = new Dictionary<Tuple<int, int>, GameObject>();
    private Vector3 terrainSize;


    // Start is called before the first frame update
    void Start()
    {
        terrainSize = terrain.GetComponent<Terrain>().terrainData.size;
    }

    void FixedUpdate()
    {
        var x = (int) Mathf.Round(trackedObject.transform.position.x / terrainSize.x);
        var z = (int) Mathf.Round(trackedObject.transform.position.z / terrainSize.z);
        var keysToRemove = new List<Tuple<int, int>>();
        foreach (var item in spawnedTerrains)
        {
            if (Math.Max(Math.Abs(item.Key.Item1 - x), Math.Abs(item.Key.Item2 - z)) > spawnRadius)
            {
                print($"Deleting object at {item.Key}");
                Destroy(item.Value);
                keysToRemove.Add(item.Key);
            }
        }
        foreach(var key in keysToRemove)
        {
            spawnedTerrains.Remove(key);
        }
        for (int i = -spawnRadius; i <= spawnRadius; i++)
        {
            for (int j = -spawnRadius; j <= spawnRadius; j++)
            {
                var loc = new Tuple<int, int>(x + i, j + z);
                if (!spawnedTerrains.ContainsKey(loc))
                {
                    var posToSpawn = new Vector3(loc.Item1 * terrainSize.x, height, loc.Item2 * terrainSize.z);
                    print($"Instantiating object at {loc}");
                    var go = Instantiate(terrain, posToSpawn, Quaternion.identity);
                    go.SetActive(true);
                    spawnedTerrains[loc] = go;
                }
            }
        }
    }
}
