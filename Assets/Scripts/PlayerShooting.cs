using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [Header("Bullet Prefabs")]
    public GameObject basicBulletPrefab;
    public GameObject fireBulletPrefab;
    public GameObject waterBulletPrefab;

    [Header("Fire Point")]
    public Transform firePoint;
    public float bulletForce = 10f;

    [Header("Shoot Settings")]
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    [Header("Weapons Unlocked")]
    public bool hasBasic = true;
    public bool hasFire = true;
    public bool hasWater = true;

    private int currentWeapon = 0;  // 0 = basic, 1 = fire, 2 = water

    [Header("Mana System")]
    public float maxMana = 20f;
    public float currentMana;
    public float manaCostPerShot = 1f;
    public float manaRegenRate = 5f;
    public float manaRegenDelay = 1f;

    public Slider manaBar;
    private float lastShotTime;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentWeapon = 0;

        currentMana = maxMana;
        if (manaBar != null)
        {
            manaBar.maxValue = maxMana;
            manaBar.value = currentMana;
        }
    }

    void Update()
    {
        HandleWeaponSwitching();
        HandleShooting();
        RegenerateMana();
    }

    
    // shooting control
    void HandleShooting()
    {
        bool canShoot =
            Input.GetButton("Fire1") &&
            Time.time >= nextFireTime &&
            currentMana >= manaCostPerShot;

        if (canShoot)
        {
            nextFireTime = Time.time + fireRate;
            anim.SetBool("IsAttacking", true);

            Shoot();
            SpendMana();
        }
        else
        {
            anim.SetBool("IsAttacking", false);
        }
    }

    
    // shoot function
    
    void Shoot()
    {
        GameObject prefabToUse;
        ElementType chosenElement;

        
        if (currentWeapon == 1 && fireBulletPrefab != null)
        {
            prefabToUse = fireBulletPrefab;
            chosenElement = ElementType.Fire;
        }
        else if (currentWeapon == 2 && waterBulletPrefab != null)
        {
            prefabToUse = waterBulletPrefab;
            chosenElement = ElementType.Water;
        }
        else
        {
            prefabToUse = basicBulletPrefab;
            chosenElement = ElementType.None;
        }

        // spawn bullet
        GameObject bullet = Instantiate(prefabToUse, firePoint.position, firePoint.rotation);

        //  assign the correct element to the bullet
        Bullet b = bullet.GetComponent<Bullet>();
        if (b != null)
            b.element = chosenElement;

    
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    
    
    // weapon switching
    void HandleWeaponSwitching()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && hasBasic)
            currentWeapon = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2) && hasFire)
            currentWeapon = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3) && hasWater)
            currentWeapon = 2;
    }

    
    // mana system
    
    void SpendMana()
    {
        currentMana -= manaCostPerShot;
        if (currentMana < 0)
            currentMana = 0;

        lastShotTime = Time.time;

        if (manaBar != null)
            manaBar.value = currentMana;
    }

    void RegenerateMana()
    {
        if (Time.time > lastShotTime + manaRegenDelay && currentMana < maxMana)
        {
            currentMana += manaRegenRate * Time.deltaTime;
            currentMana = Mathf.Clamp(currentMana, 0, maxMana);

            if (manaBar != null)
                manaBar.value = currentMana;
        }
    }

  
    public void UnlockFire() => hasFire = true;
    public void UnlockWater() => hasWater = true;
}
