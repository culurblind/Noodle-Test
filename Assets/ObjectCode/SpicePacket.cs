using UnityEngine;

public class SpicePacket : MonoBehaviour
{
    public float speedMultiplier = 2f;
    public float duration = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.StartSpeedChanger(speedMultiplier, duration);
            }
        }
    }
}