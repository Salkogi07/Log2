using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject[] items;
    public float spanwInterval;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    private Transform player;

    private void Start()
    {
        player = transform.parent;
        StartCoroutine(SpawnItem());
    }

    private void Update()
    {
        GameManager.instance.item_ShieldTimer -= Time.deltaTime;
        GameManager.instance.item_StopEnemyTimer -= Time.deltaTime;

        if (GameManager.instance.Is_ItemSheild())
        {
            GameManager.instance.isDamge = false;
        }
        else
        {
            GameManager.instance.isDamge = true;
        }

        if(GameManager.instance.Is_ItemStopEnemy())
        {
            GameManager.instance.isEnemyMove = false;
        }
        else
        {
            GameManager.instance.isEnemyMove = true;
        }
    }

    private IEnumerator SpawnItem()
    {
        while (true)
        {
            if (GameManager.instance.IsLive)
            {
                SpawnRandomItem();
            }

            yield return new WaitForSeconds(spanwInterval);
        }
    }

    private void SpawnRandomItem()
    {
        int randomIndex = Random.Range(0, items.Length);
        Vector2 randomPos = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );
        Vector2 spawnPos = (Vector2)player.position + randomPos;
        Instantiate(items[randomIndex], spawnPos, Quaternion.identity);
    }
}
