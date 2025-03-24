using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [Tooltip("Enemy prefabs to spawn")] 
    public GameObject[] enemyPrefabs;
    [Tooltip("Size of the spawn area")]
    public Vector2 spawnAreaSize = new Vector2(10f, 10f);
    [Tooltip("Time between spawns in seconds")]
    public float spawnInterval = 10f;
    [Tooltip("Maximum active enemies at once")]
    public int maxEnemies = 8;
    [Tooltip("Minimum distance from player to spawn")]
    public float minPlayerDistance = 3f;

    [Header("Advanced")]
    [Tooltip("Layers that block spawn positions")]
    public LayerMask spawnBlockingLayers;
    [Tooltip("Maximum attempts to find valid spawn position")]
    public int maxSpawnAttempts = 10;

    private float timer;
    private int currentEnemyCount;
    private Transform player;

    void Start()
    {
        timer = spawnInterval;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (ShouldSpawnEnemy())
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                if (TrySpawnEnemy())
                {
                    timer = spawnInterval;
                }
            }
        }
    }

    bool ShouldSpawnEnemy()
    {
        return LevelManager.instance.levelActive && 
               currentEnemyCount < maxEnemies && 
               enemyPrefabs.Length > 0;
    }

    bool TrySpawnEnemy()
    {
        GameObject enemyPrefab = GetRandomEnemy();
        if (TryGetValidSpawnPosition(out Vector3 spawnPos))
        {
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            currentEnemyCount++;
            return true;
        }
        return false;
    }

    GameObject GetRandomEnemy()
    {
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
    }

    bool TryGetValidSpawnPosition(out Vector3 spawnPos)
    {
        spawnPos = Vector3.zero;
        int attempts = 0;

        while (attempts < maxSpawnAttempts)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                0f,
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
            );

            spawnPos = transform.position + randomOffset;

            if (IsPositionValid(spawnPos))
            {
                return true;
            }

            attempts++;
        }

        return false;
    }

    bool IsPositionValid(Vector3 position)
    {
        // Check distance from player
        if (player != null && Vector3.Distance(position, player.position) < minPlayerDistance)
        {
            return false;
        }

        // Check for obstacles
        if (Physics.CheckSphere(position, 0.5f, spawnBlockingLayers))
        {
            return false;
        }

        return true;
    }

    public void EnemyDestroyed()
    {
        currentEnemyCount = Mathf.Max(0, currentEnemyCount - 1);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawCube(transform.position, new Vector3(spawnAreaSize.x, 0.1f, spawnAreaSize.y));
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, 0.1f, spawnAreaSize.y));

        if (player != null)
        {
            Gizmos.color = new Color(0, 1, 0, 0.2f);
            Gizmos.DrawSphere(player.position, minPlayerDistance);
        }
    }
}