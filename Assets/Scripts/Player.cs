using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;

    public static Player Instance { get { return instance; } }

    public float m_MaxHealth = 100;
    public float m_CurrentHealth = 100;

    public static Action OnPlayerDeath;

    private float m_DamageVisibilityCounter = 0;
    private float m_MaxDamageVisibilityCounter = 0.5f;
    private Material m_DamageVisibilityMaterial;
    private Color m_OriginalDamageVisibilityColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Player instance already exists in scene");
        }

        m_DamageVisibilityMaterial = GetComponent<MeshRenderer>().materials[0];
        m_OriginalDamageVisibilityColor = m_DamageVisibilityMaterial.color;

        OnPlayerDeath += () => { Debug.Log("Player Died"); };
    }

    // Update is called once per frame
    void Update()
    {
        // movement is already handled
        if (m_DamageVisibilityCounter > 0)
        {
            m_DamageVisibilityCounter -= Time.deltaTime;
        }
        else
        {
            m_DamageVisibilityCounter = 0;
        }
        
        m_DamageVisibilityMaterial.color = Color.Lerp(m_OriginalDamageVisibilityColor,Color.red, m_DamageVisibilityCounter / m_MaxDamageVisibilityCounter);
    }

    public void TakeDamage(float damage)
    {
        m_CurrentHealth -= damage;
        m_DamageVisibilityCounter = m_MaxDamageVisibilityCounter;

        if (m_CurrentHealth < 0)
        {
            OnPlayerDeath();
        }
    }


    private void OnValidate()
    {
        m_CurrentHealth = m_MaxHealth;
    }
}
