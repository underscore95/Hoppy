using UnityEngine;

public class BasicPlatform : MonoBehaviour
{
    private const float FADE_SPEED = 1.0f;
    private readonly Vector3 SCALE_SPEED = new Vector3(FADE_SPEED, FADE_SPEED, 0.0f) / 3.0f;
    [SerializeField] private float m_velocityBoost;
    [SerializeField] private SpriteRenderer m_sprite;
    [SerializeField] private PlayerDataScript m_playerData;
    private bool m_isDestroying = false;

    void Start()
    {

    }

    void Update()
    {
        if (m_isDestroying)
        {
            Color color = m_sprite.color;
            color.a -= FADE_SPEED * Time.deltaTime;
            m_sprite.color = color;
            transform.localScale -= SCALE_SPEED * Time.deltaTime;
            if (m_sprite.color.a <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    // Boost player when they pass through platform top
    void OnTriggerExit2D(Collider2D collision)
    {
        if (m_isDestroying) return;
        if (!collision.gameObject.CompareTag("Player")) return;

        Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

        playerRigidbody.linearVelocityY = m_playerData.GetBoostedPlayerVelocity(playerRigidbody.linearVelocityY, m_velocityBoost);
        m_isDestroying = true;
    }

    // Boost player when they fall and hit the platform
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

        if (playerRigidbody.linearVelocityY > 0.0f) return;

        playerRigidbody.linearVelocityY = m_velocityBoost;
        m_isDestroying = true;
    }
}
