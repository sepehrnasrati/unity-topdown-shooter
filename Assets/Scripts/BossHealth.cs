using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [Header("Boss Health Values")]
    public int shieldHealth = 20;
    public int coreHealth = 30;

    private int maxShieldHealth;
    private int maxCoreHealth;

    public bool shieldBroken => shieldHealth <= 0;

    [Header("Boss Mana Values")]
    public int maxMana = 100;
    public int currentMana;

    [Header("UI Bars (Screen) - Using Images")]
    public Image shieldFill;   // Blue fill image
    public Image coreFill;     // Red fill image

    [Header("UI Bars (World Space)")]
    public Image worldHealthFill;
    public Image worldManaFill;

    void Start()
    {
        maxShieldHealth = shieldHealth;
        maxCoreHealth = coreHealth;

        currentMana = maxMana;

        // Initialize screen-space UI
        if (shieldFill != null)
        {
            shieldFill.fillAmount = 1f;
            shieldFill.transform.parent.gameObject.SetActive(true);
        }

        if (coreFill != null)
        {
            coreFill.fillAmount = 1f;
            coreFill.transform.parent.gameObject.SetActive(false); // Hide core bar
        }

        UpdateWorldBars();
    }

    void Update()
    {
        UpdateWorldBars();
    }

    void UpdateWorldBars()
    {
        // World health bar logic
        if (worldHealthFill != null)
        {
            if (!shieldBroken)
                worldHealthFill.fillAmount = (float)shieldHealth / maxShieldHealth;
            else
                worldHealthFill.fillAmount = (float)coreHealth / maxCoreHealth;
        }

        if (worldManaFill != null)
            worldManaFill.fillAmount = (float)currentMana / maxMana;
    }

    public void TakeHit(ElementType attackElement, int damage)
    {
        
        // shield phase
        
        if (!shieldBroken)
        {
            if (attackElement == ElementType.Fire)
            {
                shieldHealth -= damage;

                if (shieldFill != null)
                {
                    shieldFill.fillAmount = Mathf.Clamp01((float)shieldHealth / maxShieldHealth);
                }

                if (shieldBroken)
                    BreakShield();
            }

            return;
        }

        
        // core phase
        
        coreHealth -= damage;

        if (coreFill != null)
        {
            coreFill.fillAmount = Mathf.Clamp01((float)coreHealth / maxCoreHealth);
        }

        if (coreHealth <= 0)
            Die();
    }

    void BreakShield()
    {
        // hide shield UI
        if (shieldFill != null)
            shieldFill.transform.parent.gameObject.SetActive(false);

        // show core UI
        if (coreFill != null)
            coreFill.transform.parent.gameObject.SetActive(true);
    }

    void Die()
    {
        SceneManager.LoadScene("WinScene");
    }
}
