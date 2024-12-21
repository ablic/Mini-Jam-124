using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Snow : MonoBehaviour
{
    [SerializeField] private int normalEmission = 20;
    [SerializeField] private int warmthEmission = 5;
    [SerializeField] private int coldEmission = 50;

    private ParticleSystem.EmissionModule emissionModule;

    private void Awake()
    {
        emissionModule = GetComponent<ParticleSystem>().emission;
    }

    private void Start()
    {
        SetNormalEmission();
    }

    public void SetWarmthEmission()
    {
        emissionModule.rateOverTime = warmthEmission;
    }

    public void SetColdEmission()
    {
        emissionModule.rateOverTime = coldEmission;
    }

    public void SetNormalEmission()
    {
        emissionModule.rateOverTime = normalEmission;
    }
}
