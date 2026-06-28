using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform m_PlayerTransform;
    public float m_DistanceSpawnedFromPlayerMin = 50;
    public float m_DistanceSpawnedFromPlayerMax = 100;
    public int m_SpawnsPerSecond = 1;
    public int m_MaxSpawnedEnemies = 100;
    public List<GameObject> m_EnemyTypes;


    private List<GameObject> m_SpawnedEnemies;


    private static EnemySpawner instance;

    public static EnemySpawner Instance { get => instance; }



    private float m_NextSpawnTimer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            Enemy.OnEnemyDied += OnEnemyKilled;
        }
        else
        {
            Debug.LogError("enemy spawner was already created. only one enemy spawner allowed per scene!");
        }

        m_SpawnedEnemies = new List<GameObject>();

        // for (int i = 0; i < m_MaxSpawnedEnemies; ++i)
        // {
        //     SpawnNextEnemy();
        // }
    }

    // Update is called once per frame
    void Update()
    {
        m_NextSpawnTimer += Time.deltaTime;

        if (m_NextSpawnTimer >= 1.0f / m_SpawnsPerSecond)
        {
            m_NextSpawnTimer = 0.0f;

            if (m_SpawnedEnemies.Count < m_MaxSpawnedEnemies)
            {
                SpawnNextEnemy();
            }
        }
    }
    public void OnEnemyKilled(Enemy enemy)
    {
        Debug.Log("EnemySpawner::OnEnemyKilled");

        if (m_SpawnedEnemies.Contains(enemy.gameObject))
            m_SpawnedEnemies.Remove(enemy.gameObject);        
    }
    private Vector3 GetNextSpawnLocation()
    {
        Vector2 ret = Random.onUnitCircle * Random.Range(m_DistanceSpawnedFromPlayerMin, m_DistanceSpawnedFromPlayerMax);

        return new Vector3(ret.x + m_PlayerTransform.position.x, 1, ret.y + m_PlayerTransform.position.z);
    }
    private GameObject SpawnNextEnemy()
    {
        int idx = Random.Range(0,m_EnemyTypes.Count);

        GameObject enemy = Instantiate(m_EnemyTypes[idx]);
        enemy.transform.position = GetNextSpawnLocation();
        enemy.transform.LookAt(m_PlayerTransform.position);

        m_SpawnedEnemies.Add(enemy);

        return enemy;
    }
}
