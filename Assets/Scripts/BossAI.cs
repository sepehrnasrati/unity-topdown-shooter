using UnityEngine;

public class BossAI : MonoBehaviour
{
    public float fireRatePhase1 = 1.2f;
    public float fireRatePhase2 = 0.6f;
    public float bulletForce = 8f;

    public GameObject enemyBulletPrefab;
    public Transform firePoint;     // only firePoint rotates!

    private Transform player;
    private Rigidbody2D rb;
    private BossHealth bossHealth;

    private float nextFireTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        bossHealth = GetComponent<BossHealth>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // --- Rotate ONLY the fire point, not the boss ---
        Vector2 dir = (Vector2)player.position - (Vector2)firePoint.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        firePoint.rotation = Quaternion.Euler(0, 0, angle);

        // --- STOP boss rotating completely ---
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (player == null || bossHealth == null) return;

        float currentFireRate = bossHealth.shieldBroken ? fireRatePhase2 : fireRatePhase1;

        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + currentFireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (enemyBulletPrefab == null || firePoint == null) return;

        if (!bossHealth.shieldBroken)
        {
            SpawnBullet(firePoint.rotation);
        }
        else
        {
            float spread = 20f;

            SpawnBullet(firePoint.rotation);
            SpawnBullet(firePoint.rotation * Quaternion.Euler(0, 0, spread));
            SpawnBullet(firePoint.rotation * Quaternion.Euler(0, 0, -spread));
        }
    }

    void SpawnBullet(Quaternion rotation)
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, rotation);
        Rigidbody2D brb = bullet.GetComponent<Rigidbody2D>();
        brb.AddForce((Vector2)(rotation * Vector3.up) * bulletForce, ForceMode2D.Impulse);
    }
}
