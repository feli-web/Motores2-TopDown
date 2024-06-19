using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject recover;
    public GameObject[] enemySpawners;
    public GameObject[] recoverSpawners;
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, 3f);
        InvokeRepeating("SpawnRecover", 5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnEnemy()
    {
        int r = Random.Range(0, enemySpawners.Length);
        Instantiate(enemy, enemySpawners[r].transform.position, enemySpawners[r].transform.rotation);
    }
    void SpawnRecover()
    {
        int r = Random.Range(0, recoverSpawners.Length);
        var a =Instantiate(recover, recoverSpawners[r].transform.position, recoverSpawners[r].transform.rotation);
        Destroy(a, 5f);
    }
}
