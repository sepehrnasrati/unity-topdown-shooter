using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    int currentHealth;

    
    public Transform healthBar;  
    private float initialBarScaleX;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            initialBarScaleX = healthBar.localScale.x;
            UpdateHealthBar();
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (healthBar != null)
        {
            UpdateHealthBar();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        float normalized = Mathf.Clamp01(currentHealth / (float)maxHealth);
        Vector3 scale = healthBar.localScale;
        scale.x = initialBarScaleX * normalized;
        healthBar.localScale = scale;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
