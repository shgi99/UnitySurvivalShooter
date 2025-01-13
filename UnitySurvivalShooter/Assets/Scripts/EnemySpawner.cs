using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy[] Enemies;
    public Transform[] spawnPoints;

    private List<Enemy> enemyList= new List<Enemy>();
    private GameManager gameManager;
    private float spawnInterval;
    private float lastSpawnTime = 0f;
    private float nextLevelTime = 30f;
    private int maxEnemyIndex = 1;
   
    void Start()
    {
        spawnInterval = Random.Range(2f, 5f);
        lastSpawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            SpawnEnemies();
            spawnInterval = Random.Range(2f, 5f);
            lastSpawnTime = Time.time;
        }

        if (Time.time >= nextLevelTime)
        {
            LevelUp();
        }
    }
    private void SpawnEnemies()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            int enemyIndex = Random.Range(0, maxEnemyIndex + 1);
            var enemy = Instantiate(Enemies[enemyIndex], spawnPoint.position, spawnPoint.rotation);
            enemyList.Add(enemy);

            enemy.onDeath += () => enemyList.Remove(enemy);
        }
    }
    private void LevelUp()
    {
        // 생성 가능한 적의 최대 인덱스를 증가시킴
        if (maxEnemyIndex < Enemies.Length - 1)
        {
            maxEnemyIndex++;
            
        }
    }
}
