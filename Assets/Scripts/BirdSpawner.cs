using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject bird;
    public float spawnDistance = 50f;
    public float spawnRate = 0.25f;
    private float lastSpawnTime;
    private GameObject balloon;
    // Start is called before the first frame update
    void Start()
    {
        balloon = GameObject.Find("balloon");
        lastSpawnTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - lastSpawnTime) * spawnRate > 1)
        {
            lastSpawnTime = Time.time;
            var xz = Random.insideUnitCircle.normalized * spawnDistance;
            var spawnPos = new Vector3(xz.x, 0, xz.y) + balloon.transform.position;
            Instantiate(bird, spawnPos, Quaternion.identity);
        }
    }
}
