using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    [Header("Bullet Pool Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 80;
    [SerializeField] private bool allowPoolExpansion = true;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateBullet();
        }
    }

    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
        return bullet;
    }

    public GameObject GetBullet()
    {
        if (bulletPool.Count == 0)
        {
            if (allowPoolExpansion)
            {
                Debug.LogWarning("⚠️ Bullet pool exhausted! Expanding...");
                return CreateBullet(); // Expand the pool dynamically
            }
            else
            {
                Debug.LogError("❌ Bullet pool exhausted and expansion is disabled!");
                return null;
            }
        }

        GameObject bullet = bulletPool.Dequeue();
        bullet.SetActive(true);
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        if (bullet == null) return;

        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
