using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float m_BulletRange = 500;
    public float m_ShotsPerSecond = 1;
    public int m_MaxAmmoCount = 10;
    public int m_BulletDamage = 1;
    public LayerMask m_LayerMask;

    private int m_AmmoCount = 10;

    private void OnValidate()
    {
        m_AmmoCount = m_MaxAmmoCount;
    }

    public void OnShoot()
    {
        if (m_AmmoCount > 0)
        {
            --m_AmmoCount;

            Debug.DrawLine(transform.position, transform.position + new Vector3(transform.forward.x * m_BulletRange, transform.forward.y * m_BulletRange, transform.forward.z * m_BulletRange), Color.red, 1.0f, true);
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, m_BulletRange, m_LayerMask))
            {
                Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.TakeDamage(m_BulletDamage);
                }
            }
        }
        else
        {
            Reload();
        }
    }
    public void Reload()
    {
        GetComponent<Animator>().SetTrigger("OnReload");
    }
    public void OnReload()
    {
        m_AmmoCount = m_MaxAmmoCount;
    }
}
