using UnityEngine;

public class PlayerLoseScript : MonoBehaviour
{
    [SerializeField] private SceneManagerScript m_sceneManager;
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private float m_secondsFallingForLose = 5.0f;
    private float m_secondsFalling = 0.0f;

    void Update()
    {
        bool isFalling = m_rigidbody.linearVelocityY < 0.0f;
        if (isFalling)
        {
            m_secondsFalling += Time.deltaTime;
            if (m_secondsFalling >= m_secondsFallingForLose)
            {
                m_sceneManager.SwitchToShop();
            }
        }
        else
        {
            m_secondsFalling = 0.0f;
        }
    }
}
