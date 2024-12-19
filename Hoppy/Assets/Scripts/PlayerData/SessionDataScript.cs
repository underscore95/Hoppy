using UnityEngine;
using UnityEngine.Assertions;

public class SessionDataScript : MonoBehaviour
{
    public PlayerDataScript PlayerData;

    public float CoinsCollected { get; private set; } = 0;
    public void AddCoins(float coins)
    {
        Assert.IsNotNull(PlayerData.SessionData);
        CoinsCollected += coins;
        PlayerData.Coins += coins;
    }

    void Start()
    {
        Assert.IsNull(PlayerData.SessionData);
        PlayerData.SessionData = this;
    }

    void OnDestroy()
    {
        Assert.IsTrue(PlayerData.SessionData == this);
        PlayerData.SessionData = null;
    }
}
