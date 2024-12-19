using System.Text;
using TMPro;
using UnityEngine;

public class MaxVelocityButtonScript : MonoBehaviour
{
    [SerializeField] private PlayerDataScript m_playerData;
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private float m_increaseAmount = 2.0f;

    void Update()
    {
        int cost = GetCost(m_playerData.MaxVelocity);
        m_text.color = m_playerData.Coins >= cost ? Color.black : Color.red;
        m_text.text = new StringBuilder()
            .Append("Max Velocity ")
            .Append(m_playerData.MaxVelocity)
            .Append(" -> ")
            .Append(m_playerData.MaxVelocity + m_increaseAmount)
            .Append("\n")
            .Append(cost)
            .Append(" coins")
            .ToString();
    }

    public void TryPurchase()
    {
        int cost = GetCost(m_playerData.MaxVelocity);
        if (m_playerData.Coins >= cost)
        {
            m_playerData.Coins -= cost;
            m_playerData.MaxVelocity += m_increaseAmount;
        }
    }

    int GetCost(float current)
    {
        return (int)(current + Mathf.Pow(1.2f, current) - 6.0f);
    }
}
