using System;
using UnityEngine;

[CreateAssetMenu]
public class GameConfig : ScriptableObject
{
    private const string line = "---------------------------------------------------------------------------------------------------------------------";

    #region Player
    [field: Header("Player")]

    [field: SerializeField]
    [field: Range(0.1f, 10f)]
    public float PlayerHorizontalVelocity { get; private set; } = 5f;

    [field: SerializeField]
    [field: Range(0.1f, 10f)]
    public float PlayerVerticalVelocity { get; private set; } = 5f;

    [field: SerializeField]
    [field: Range(0.1f, 5f)]
    public float PlayerInteractionRange { get; private set; } = 1.5f;

    [field: SerializeField]
    public KeyCode[] PlayerInteractionKeys { get; private set; } = new KeyCode[] { KeyCode.Space };
    #endregion
    [field: Header(line)]
    #region Warmth
    [field: Header("Warmth")]

    [field: SerializeField]
    [field: Range(0.01f, 0.25f)]
    public float WarmthDecreaseRate { get; private set; } = 0.1f;
    #endregion
    [field: Header(line)]
    #region Passerbies
    [field: Header("Passerbies")]

    [field: SerializeField]
    [field: Range(0.1f, 5f)]
    public float MinPasserbySpawnPeriod { get; private set; } = 1f;

    [field: SerializeField]
    [field: Range(0.1f, 5f)]
    public float MaxPasserbySpawnPeriod { get; private set; } = 3f;

    [field: SerializeField]
    [field: Range(0.1f, 10f)]
    public float MinPasserbySpeed { get; private set; } = 1f;

    [field: SerializeField]
    [field: Range(0.1f, 10f)]
    public float MaxPasserbySpeed { get; private set; } = 3f;

    [field: SerializeField]
    [field: Range(0.03f, 0.5f)]
    public float PasserbyWarmthIncrease { get; private set; } = 0.2f;
    #endregion
    [field: Header(line)]
    #region Weather
    [field: Header("Weather")]

    [field: SerializeField]
    [field: Range(0f, 0.5f)]
    public float WeatherAdditionalWarmthDecrease { get; private set; } = 0.1f;

    [field: SerializeField]
    [field: Range(0f, 1f)]
    public float WeatherColdThreshold { get; private set; } = 0.6f;

    [field: SerializeField]
    [field: Range(1f, 20f)]
    public float WeatherColdDuration { get; private set; } = 10f;

    [field: SerializeField]
    [field: Range(0.5f, 5)]
    public float WeatherColorSwitchDuration { get; private set; } = 2f;

    [field: SerializeField]
    [field: Range(0f, 1f)]
    public float WeatherSwitchChanceAtThresholdValue { get; private set; } = 0.5f;

    [field: SerializeField]
    [field: Range(0f, 1f)]
    public float WeatherSwitchChanceAtMaxValue { get; private set; } = 0.9f;
    #endregion
    [field: Header(line)]
    #region Game
    [field: Header("Game")]

    [field: SerializeField]
    [field: Range(10, 300)]
    public int TimeToWin { get; private set; } = 60;
    #endregion

    private void OnValidate()
    {
        if (MinPasserbySpawnPeriod > MaxPasserbySpawnPeriod)
            MinPasserbySpawnPeriod = MaxPasserbySpawnPeriod;

        if (MinPasserbySpeed > MaxPasserbySpeed)
            MinPasserbySpeed = MaxPasserbySpeed;

        if (WeatherSwitchChanceAtThresholdValue > WeatherSwitchChanceAtMaxValue)
            WeatherSwitchChanceAtThresholdValue = WeatherSwitchChanceAtMaxValue;
    }
}
