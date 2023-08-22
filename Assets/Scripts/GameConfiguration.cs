using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Game Configuration")]
public class GameConfiguration : ScriptableObject
{
    private const string gameUnitInfo = "Unit = tile size";

    [Serializable]
    public class _Shared
    {
        [field: SerializeField]
        [field: Range(10, 600)]
        [field: Tooltip("Time to win in seconds")]
        public int TimeToWin { get; private set; }
    }

    [Serializable]
    public class _Player
    {
        [field: SerializeField]
        [field: Range(1f, 10f)]
        [field: Tooltip("Units per second. " + gameUnitInfo)]
        public float HorizontalVelocity { get; private set; } = 2.5f;

        [field: SerializeField]
        [field: Range(1f, 10f)]
        [field: Tooltip("Units per second. " + gameUnitInfo)]
        public float VerticalVelocity { get; private set; } = 2.5f;

        [field: SerializeField]
        [field: Range(0.5f, 3f)]
        [field: Tooltip("In units. " + gameUnitInfo)]
        public float InteractionRange { get; private set; } = 1.5f;

        [field: SerializeField]
        public KeyCode[] InteractionKeys { get; private set; } = new KeyCode[]
        {
            KeyCode.Space,
            KeyCode.Mouse0
        };
    }

    [Serializable]
    public class _Passerby
    {
        [field: SerializeField]
        [field: Range(0.5f, 5f)]
        [field: Tooltip("In seconds")]
        public float MinSpawnPeriod { get; private set; } = 1f;

        [field: SerializeField]
        [field: Range(0.5f, 5f)]
        [field: Tooltip("In seconds")]
        public float MaxSpawnPeriod { get; private set; } = 3f;

        [field: SerializeField]
        [field: Range(1f, 10f)]
        [field: Tooltip("Units per second. " + gameUnitInfo)]
        public float MinHorizontalVelocity { get; private set; } = 2.5f;

        [field: SerializeField]
        [field: Range(1f, 10f)]
        [field: Tooltip("Units per second. " + gameUnitInfo)]
        public float MaxHorizontalVelocity { get; private set; } = 5f;

        public void Validate()
        {
            if (MinSpawnPeriod > MaxSpawnPeriod)
                MinSpawnPeriod = MaxSpawnPeriod;

            if (MinHorizontalVelocity > MaxHorizontalVelocity)
                MinHorizontalVelocity = MaxHorizontalVelocity;
        }
    }

    [Serializable]
    public class _Warmth
    {
        [field: SerializeField]
        [field: Range(0.01f, 0.2f)]
        [field: Tooltip("Reduction per second")]
        public float Reduction { get; private set; }
    }

    [field: SerializeField] public _Shared Shared { get; private set; }
    [field: SerializeField] public _Player Player { get; private set; }
    [field: SerializeField] public _Warmth Warmth { get; private set; }
    [field: SerializeField] public _Passerby Passerby { get; private set; }

    private void OnValidate()
    {
        Passerby.Validate();
    }
}
