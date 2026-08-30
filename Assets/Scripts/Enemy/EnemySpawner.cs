using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : SceneSingleton<EnemySpawner>
{
    [Header("Spawn Settings")]
    public string[] enemyTags;
    public Transform player;             
    public float spawnRadius = 15f;      // 플레이어로부터 스폰 거리
    public float safeRadius = 5f;        // 플레이어 너무 근처는 제외
    public float spawnInterval = 2f;     // 스폰 주기

    private float timer = 0f;
    private float elapsedTime = 0f;     //경과 시간
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Update()
    {
        elapsedTime += Time.deltaTime;
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }

        // 죽은 적 제거
        activeEnemies.RemoveAll(enemy => enemy == null);
    }
    
    void SpawnEnemy()
    {
        string tag = enemyTags[Random.Range(0, enemyTags.Length)];

        if (ObjectPoolManager.Instance.IsTagFullyActive(tag)) return; //한계치만큼 스폰되어있는지 확인
        
        // 플레이어 주변 랜덤 위치 계산
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(safeRadius, spawnRadius);
        Vector3 spawnPos = player.position + new Vector3(randomDir.x, 0, randomDir.y) * randomDistance;

        var enemy = ObjectPoolManager.Instance.SpawnFromPool(tag, spawnPos, Quaternion.identity).GetComponent<Enemy>();
        enemy.GetPlayerPosition(player);
    }
    
}
