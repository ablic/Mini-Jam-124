using System;
using UnityEngine;

[CreateAssetMenu]
public class GameConfig : ScriptableObject
{
    [Serializable]
    public class PlayerModule
    {
        [Range(0.1f, 10f)]
        public float HorizontalVelocity = 5f;

        [Range(0.1f, 10f)]
        public float VerticalVelocity = 5f;

        [Range(0.1f, 5f)]
        public float InteractionRange = 1.5f;

        public KeyCode[] InteractionKeys = new KeyCode[] { KeyCode.Space };
    }

    [Serializable]
    public class PasserbyModule
    {
        [Range(0.1f, 5f)]
        public float MinSpawnPeriod = 1f;

        [Range(0.1f, 5f)]
        public float MaxSpawnPeriod = 3f;

        [Range(0.1f, 10f)]
        public float MinSpeed = 1f;

        [Range(0.1f, 10f)]
        public float MaxSpeed = 3f;

        [Range(0.03f, 0.5f)]
        public float WarmthIncrease = 0.2f;
    }

    [Serializable]
    public class WarmthModule
    {
        [Range(0.01f, 0.25f)]
        public float DecreaseRate = 0.1f;
    }

    [Serializable]
    public class WeatherModule
    {
        [Header("Cold")]

        [Range(0f, 0.5f)]
        public float AdditionalColdDecrease = 0.05f;

        [Range(0f, 1f)]
        public float ColdThreshold = 0.7f;

        [Range(1f, 20f)]
        public float ColdDuration = 5f;

        [Range(0f, 1f)]
        public float ColdSwitchChance = 0.5f;

        [Header("Warmth")]

        [Range(0f, 0.5f)]
        public float AdditionalWarmthIncrease = 0.05f;

        [Range(0f, 1f)]
        public float WarmthThreshold = 0.3f;

        [Range(1f, 20f)]
        public float WarmthDuration = 5f;

        [Range(0f, 1f)]
        public float WarmthSwitchChance = 0.5f;
    }

    [Serializable]
    public class GameModule
    {
        [Range(10, 300)]
        public int TimeToWin = 60;
    }

    public PlayerModule Player;
    public PasserbyModule Passerby;
    public WarmthModule Warmth;
    public WeatherModule Weather;
    public GameModule Game;
}
