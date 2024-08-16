using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public float duringObj;

    private void Start()
    {
        Destroy(gameObject,duringObj);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
