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

    public void Shoot()
    {
        if (m_AmmoCount > 0)
        {
            --m_AmmoCount;

            if (Physics.Raycast(transform.position,transform.forward,out RaycastHit hit, m_BulletRange, m_LayerMask))
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
        m_AmmoCount = m_MaxAmmoCount;
    }
}
