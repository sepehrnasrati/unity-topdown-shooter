using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 5;
    private int currentHealth;

    [Header("UI")]
    public Slider healthBar;      
    public Image fill;            
    public Image damageFill;      
    public float damageShrinkSpeed = 0.4f;

    private Animator anim;
    private bool isDead = false;

    void Start()
    {
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (fill != null)
            fill.fillAmount = 1f;

        if (damageFill != null)
            damageFill.fillAmount = 1f;
    }

    void Update()
    {
        
        if (damageFill != null && fill != null)
        {
            if (damageFill.fillAmount > fill.fillAmount)
            {
                damageFill.fillAmount -= damageShrinkSpeed * Time.deltaTime;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // upate slider value 
        if (healthBar != null)
            healthBar.value = currentHealth;

        // update normal fill instantly
        if (fill != null)
            fill.fillAmount = (float)currentHealth / maxHealth;



        // hurt animation
        anim.SetBool("IsHurt", true);
        Invoke(nameof(ResetHurt), 0.2f);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void ResetHurt()
    {
        anim.SetBool("IsHurt", false);
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        anim.SetTrigger("Die");

       
        Invoke(nameof(ReloadLevel), 1.2f);
    }

    void ReloadLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
