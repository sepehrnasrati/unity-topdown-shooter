using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int baseDamage = 1;
    public float lifeTime = 2f;

    public ElementType element = ElementType.None;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        EnemyHealth enemy = collision.collider.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            EnemyElement ee = collision.collider.GetComponent<EnemyElement>();

            int finalDamage = baseDamage;

            if (ee != null)
            {
                finalDamage = CalculateElementalDamage(baseDamage, element, ee.element);
            }

            enemy.TakeDamage(finalDamage);
            Destroy(gameObject);
            return;
        }

        
        BossHealth boss = collision.collider.GetComponent<BossHealth>();
        if (boss != null)
        {
            boss.TakeHit(element, baseDamage);
            Destroy(gameObject);
            return;
        }

        
        if (!collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    int CalculateElementalDamage(int baseDamage, ElementType attack, ElementType target)
    {
        float multiplier = 1f;

        if (attack == ElementType.Fire && target == ElementType.Water)
            multiplier = 2f;

        if (attack == ElementType.Water && target == ElementType.Fire)
            multiplier = 2f;

        if (attack == ElementType.Fire && target == ElementType.Ice)
            multiplier = 2f;

        if (attack == target && attack != ElementType.None)
            multiplier = 0.5f;

        return Mathf.RoundToInt(baseDamage * multiplier);
    }
}
