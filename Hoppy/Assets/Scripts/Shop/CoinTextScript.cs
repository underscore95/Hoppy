using System.Text;
using UnityEngine;

public class CoinTextScript : MonoBehaviour
{
    [SerializeField] private PlayerDataScript m_playerData;
    [SerializeField] private TMPro.TextMeshProUGUI m_text;

    void Update()
    {
        if (m_playerData.SessionData == null)
        {
            m_text.text = m_playerData.Coins.ToString();
        }
        else
        {
            StringBuilder s = new StringBuilder()
                .Append(m_playerData.Coins)
                .Append(" (")
                .Append(m_playerData.SessionData.CoinsCollected)
                .Append(" this session)");
            m_text.text = s.ToString();
        }
    }
}
