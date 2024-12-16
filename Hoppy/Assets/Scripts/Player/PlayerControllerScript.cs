using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_speed = 5.0f;
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private SpriteRenderer m_sprite;
    [SerializeField] private PlayerDataScript m_playerData;

    void Start()
    {
        m_rigidbody.linearVelocityY = m_playerData.StartingVelocity;
    }

    void Update()
    {
        // Input
        float direction = Input.GetKey(KeyCode.A) ? -1.0f : Input.GetKey(KeyCode.D) ? 1.0f : 0.0f;
        m_rigidbody.linearVelocityX = direction * m_speed;

        // Flip sprite
        if (direction != 0.0f)
        {
            m_sprite.flipX = direction < 0.0f;
        }
    }
}
