using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataScript", menuName = "Scriptable Objects/PlayerDataScript")]
public class PlayerDataScript : ScriptableObject
{
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
            return MaxVelocity;
        }
        return Mathf.Min(current + boostAmount, MaxVelocity);
    }
}
