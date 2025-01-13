using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public bool isGameOver { get; private set; }
    private int score = 0;
    public UIManager uiManager;
    private EnemySpawner enemySpawner;
    private void Awake()
    {
        enemySpawner = GetComponent<EnemySpawner>();
    }
    private void Start()
    {
        var playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.onDeath += OnGameOver;
    }
    private void Update()
    {
    }
    public void AddScore(int newScore)
    {
        if (isGameOver)
        {
            return;
        }

        score += newScore;
    }
    public void OnGameOver()
    {
        isGameOver = true;
        enemySpawner.enabled = false;
    }
}
