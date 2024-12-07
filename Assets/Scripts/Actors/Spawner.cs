using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy prefab;
    private List<GameObject> enemies;
    private float SpawnCooldown = 6f;

    private bool isSpawning;

    private void Start()
    {
        Enemy.onEnemyKill += NewSpawn;
        isSpawning = false;
        enemies = new List<GameObject>();
        for (int i = 0; i < 30; i++)
        {
            var obj = Instantiate(prefab);
            obj.gameObject.transform.parent = transform;
            enemies.Add(obj.gameObject);
            obj.gameObject.SetActive(false);
        }
        NewSpawn();
    }

    private void Spawn()
    {
        if (GetEnemy() != null)
        {
            var enemy = GetEnemy();
            enemy.SetActive(true);
            enemy.transform.position = new Vector3(UnityEngine.Random.Range(-0.6f, 11.5f), UnityEngine.Random.Range(-0.49f, -1.51f), 0);
            StartCoroutine(SpawningDelay());
        }
        else
        {
            StopCoroutine(SpawningDelay());
        }
    }

    private void NewSpawn()
    {
        if (isSpawning) return;

        StartCoroutine(SpawningDelay());
    }

    private GameObject GetEnemy()
    {
        foreach (var obj in enemies)
        {
            if (!obj.gameObject.activeSelf)
            {
                return obj;
            }
        }
        return null;
    }

    private IEnumerator SpawningDelay()
    {
        isSpawning = true;
        yield return new WaitForSeconds(SpawnCooldown);
        Spawn();
    }

    private void OnDestroy()
    {
        Enemy.onEnemyKill -= NewSpawn;
        StopAllCoroutines();
    }
}