using System.Text;
using TMPro;
using UnityEngine;

public class LaunchPowerButtonScript : MonoBehaviour
{
    [SerializeField] private PlayerDataScript m_playerData;
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private float m_increaseAmount = 5.0f;

    void Update()
    {
        int cost = GetCost(m_playerData.StartingVelocity);
        m_text.color = m_playerData.Coins >= cost ? Color.black : Color.red;
        m_text.text = new StringBuilder()
            .Append("Launch Power ")
            .Append(m_playerData.StartingVelocity)
            .Append(" -> ")
            .Append(m_playerData.StartingVelocity + m_increaseAmount)
            .Append("\n")
            .Append(cost)
            .Append(" coins")
            .ToString();
    }

    public void TryPurchase()
    {
        int cost = GetCost(m_playerData.StartingVelocity);
        if (m_playerData.Coins >= cost)
        {
            m_playerData.Coins -= cost;
            m_playerData.StartingVelocity += m_increaseAmount;
        }
    }

    int GetCost(float current)
    {
        return (int)(current / 3.0f + Mathf.Pow(1.2f, current / 3.0f) * 1.39f - 1.0f);
    }
}
