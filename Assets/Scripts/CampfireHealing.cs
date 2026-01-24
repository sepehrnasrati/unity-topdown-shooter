using UnityEngine;

public class CampfireHealing : MonoBehaviour
{
    public int healAmount = 1;        // How much to heal per tick
    public float healInterval = 0.5f; // How fast to heal (seconds)

    private float nextHealTime;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.time >= nextHealTime)
        {
            PlayerHealth hp = collision.GetComponent<PlayerHealth>();

            if (hp != null)
            {
                hp.TakeDamage(-healAmount);  // negative damage = heal
            }

            nextHealTime = Time.time + healInterval;
        }
    }
}
