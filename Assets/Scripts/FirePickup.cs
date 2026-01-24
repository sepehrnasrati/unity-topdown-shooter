using UnityEngine;

public class FirePickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // only react to player
        if (other.CompareTag("Player"))
        {
            PlayerShooting shooting = other.GetComponent<PlayerShooting>();

            if (shooting != null)
            {
                shooting.UnlockFire();
            }

            // remove after pickup
            Destroy(gameObject);
        }
    }
}
