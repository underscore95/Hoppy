using UnityEngine;
using UnityEngine.Assertions;

public class CoinCollectScript : MonoBehaviour
{
    [SerializeField] private int m_coinValue = 1;
    [SerializeField] private PlayerDataScript m_playerDataScript;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Assert.IsNotNull(m_playerDataScript);
            Assert.IsNotNull(m_playerDataScript.SessionData);
            m_playerDataScript.SessionData.AddCoins(m_coinValue);
            Destroy(gameObject);
        }
    }
}
