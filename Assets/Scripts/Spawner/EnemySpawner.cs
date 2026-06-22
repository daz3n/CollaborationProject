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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("enemy spawner was already created. only one enemy spawner allowed per scene!");
        }

        m_SpawnedEnemies = new List<GameObject>();

        for (int i = 0; i < m_MaxSpawnedEnemies; ++i)
        {
            SpawnNextEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnEnemyKilled(GameObject enemy)
    {    
        if (m_SpawnedEnemies.Contains(enemy))
            m_SpawnedEnemies.Remove(enemy);        
    }
    private Vector3 GetNextSpawnLocation()
    {
        Vector2 ret = Random.onUnitCircle * Random.Range(m_DistanceSpawnedFromPlayerMin, m_DistanceSpawnedFromPlayerMax);

        return new Vector3(ret.x, 1, ret.y);
    }
    private GameObject SpawnNextEnemy()
    {
        int idx = Random.Range(0,m_EnemyTypes.Count);

        GameObject enemy = Instantiate(m_EnemyTypes[idx]);
        enemy.transform.position = GetNextSpawnLocation();
        enemy.transform.LookAt(m_PlayerTransform.position);

        Debug.Log(enemy);

        m_SpawnedEnemies.Add(enemy);

        return enemy;
    }
}
