using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataScript", menuName = "Scriptable Objects/PlayerDataScript")]
public class PlayerDataScript : ScriptableObject
{
    public SessionDataScript SessionData;

    [field: SerializeField]

    public float StartingVelocity { get; set; }

    [field: SerializeField]
    public float MaxVelocity { get; set; }
    /// <summary>
    /// What is the new velocity of the player after applying the boost?
    /// </summary>
    /// <param name="current"></param> The current velocity of the player
    /// <param name="boostAmount"></param> The amount to boost the player velocity by
    /// <returns></returns>
    public float GetBoostedPlayerVelocity(float current, float boostAmount)
    {
        if (current > MaxVelocity)
        {
            return current;
        }
        return Mathf.Min(Mathf.Max(0, current) + boostAmount, MaxVelocity);

    }

    public float Coins { get; set; }

    private void OnEnable()
    {
        Coins = 0;
        StartingVelocity = 20;
        MaxVelocity = 10;
    }
}