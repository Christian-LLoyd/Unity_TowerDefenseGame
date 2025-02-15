using UnityEngine;

public class enemy_spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject[] enemyPrefabs; // Array of enemy prefabs
    public Vector2 spawnAreaSize = new Vector2(10f, 10f); // Define spawn area size
    public float spawnInterval = 10f; // Time between spawns
    public int maxEnemies = 8; // Maximum number of enemies at a time

    private float timer;
    private int currentEnemyCount;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        if (currentEnemyCount < maxEnemies)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                SpawnEnemy();
                timer = spawnInterval;
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0) return; // Prevent error if no enemies are assigned

        // Pick a random enemy from the array
        GameObject selectedEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Generate a random spawn position within the defined area
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            0f, // Keep it at ground level
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
        );

        // Instantiate enemy and update counter
        Instantiate(selectedEnemy, transform.position + spawnPosition, Quaternion.identity);
        currentEnemyCount++;
    }

    public void EnemyDestroyed()
    {
        currentEnemyCount--; // Decrease enemy count when an enemy is destroyed
    }

    void OnDrawGizmos()
    {
        // Draw spawn area in Scene View for visualization
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, 0.1f, spawnAreaSize.y));
    }
}
