using UnityEngine;

public class Chopsticks : MonoBehaviour
{
    public float speedMultiplier = 0.5f;
    public float duration = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.StartSpeedChanger(speedMultiplier, duration);
                Destroy(gameObject);
            }
        }
    }
}