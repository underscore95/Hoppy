using UnityEngine;

public class BasicPlatform : MonoBehaviour
{
    [SerializeField] private float m_velocityBoost;
    [SerializeField] private PlayerDataScript m_playerData;

    void Start()
    {

    }

    void Update()
    {
    }

    // Boost player when they pass through platform top
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

        playerRigidbody.linearVelocityY = m_playerData.GetBoostedPlayerVelocity(playerRigidbody.linearVelocityY, m_velocityBoost);
        Destroy(gameObject, 1.0f);
    }

    // Boost player when they fall and hit the platform
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

        if (playerRigidbody.linearVelocityY > 0.0f) return;

        playerRigidbody.linearVelocityY = m_velocityBoost;
        Destroy(gameObject, 1.0f);
    }
}
