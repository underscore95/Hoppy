using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BouncinessButtonScript : MonoBehaviour
{
    [SerializeField] private PlayerDataScript m_playerData;
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private float m_increaseAmount = 0.2f;

    void Update()
    {
        int cost = GetCost(m_playerData.Bounciness);
        m_text.color = m_playerData.Coins >= cost ? Color.black : Color.red;
        m_text.text = new StringBuilder()
            .Append("Bounciness ")
            .Append(m_playerData.Bounciness)
            .Append(" -> ")
            .Append(m_playerData.Bounciness + m_increaseAmount)
            .Append("\n")
            .Append(cost)
            .Append(" coins")
            .ToString();
    }

    public void TryPurchase()
    {
        int cost = GetCost(m_playerData.Bounciness);
        if (m_playerData.Coins >= cost)
        {
            m_playerData.Coins -= cost;
            m_playerData.Bounciness += m_increaseAmount;
        }
    }

    int GetCost(float currentBounciness)
    {
        return (int)(10.0f * Mathf.Pow(2, 1.5f * currentBounciness) - 18.0f);
    }
}
