using UnityEngine;

public class SlimeDamage : MonoBehaviour
{
    public int contactDamage = 1;   // damage per hit

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        if (collision.gameObject.CompareTag("Player"))
        {
            
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(contactDamage);
            }
        }
    }
}
