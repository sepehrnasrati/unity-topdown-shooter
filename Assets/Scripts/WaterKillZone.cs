using UnityEngine;

public class WaterKillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player touched water!");

            PlayerHealth hp = collision.GetComponent<PlayerHealth>();

            if (hp != null)
            {
                hp.TakeDamage(999);  // instant kill
            }
        }
    }
}
