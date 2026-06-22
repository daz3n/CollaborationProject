using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int m_MaxHealth = 100;
    public int m_CurrentHealth = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnValidate()
    {
        m_CurrentHealth = m_MaxHealth;
    }
}
