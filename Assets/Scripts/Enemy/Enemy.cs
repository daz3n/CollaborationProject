using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int m_MaxHealth = 100;
    public int m_CurrentHealth = 100;
    public float m_MoveSpeed = 500;
    public float m_DamageAmount = 2;

    public static Action<Enemy> OnEnemyDied;

    public float m_MaxDamageTick = 1;
    private float m_DamageTick = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Player.Instance.transform.position - transform.position).normalized * m_MoveSpeed * Time.deltaTime,Space.World);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_DamageTick -= Time.deltaTime;
        
        
            if (m_DamageTick <= 0 )
            {
                m_DamageTick = m_MaxDamageTick;

                collision.gameObject.GetComponent<Player>().TakeDamage(m_DamageAmount);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_DamageTick = m_MaxDamageTick;
        }
    }
    void OnValidate()
    {
        m_CurrentHealth = m_MaxHealth;
        m_DamageTick = 0;
    }
    void TakeDamage(int amount)
    {
        m_CurrentHealth -= amount;

        if (m_CurrentHealth < 0)
        {
            Die();
        }
    }
    void Die()
    {
        OnEnemyDied(this);
        Destroy(gameObject);
    }
}
