using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GachaHelper
{
    public static int DoGacha(float[] chances)
    {
        float sum = 0;
        for(int i = 0; i < chances.Length; i++)
            sum += chances[i];

        float rng = Random.Range(0, sum);

        for(int i = 0; i < chances.Length; i++)
        {
            if(rng < chances[i])
                return i;
            else
                rng -= chances[i];
        }

        return -1;
    }
}

public class Spawner : MonoBehaviour
{
    public GameObject[] prefab;
    public Transform[] spanwpoint;
    public float spawnTime;

    float timer;

    private void Awake()
    {
        spanwpoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        if (!GameManager.instance.IsLive)
            return;

        timer += Time.deltaTime;

        if(timer > spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        if (GameManager.instance.isBossSpawned)
            return;

        int enemyRandom = Random.Range(0, prefab.Length);
        int enemyPos = Random.Range(1, spanwpoint.Length);
        Instantiate(prefab[enemyRandom], spanwpoint[enemyPos].position, Quaternion.identity);
    }
}
