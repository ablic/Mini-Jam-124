using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weather : MonoBehaviour
{
    [SerializeField] private GameConfig config;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color warmthColor;
    [SerializeField] private Color coldColor;
    [SerializeField] private Warmth warmth;
    [SerializeField] private Image weatherCurtain;
    [SerializeField] private GameObject warmthIndicator;
    [SerializeField] private GameObject coldIndicator;
    [SerializeField] private float colorSwitchDuration = 1f;

    private Coroutine warmthCoroutine;
    private Coroutine coldCoroutine;

    private void Start()
    {
    }

    private void Update()
    {
        TryStartCold();
        TryStartWarmth();
    }

    private void TryStartWarmth()
    {
        if (warmthCoroutine != null || warmth.Value > config.Weather.WarmthThreshold)
            return;

        if (Random.value < config.Weather.WarmthSwitchChance)
            warmthCoroutine = StartCoroutine(Warmth());
    }

    private void TryStartCold()
    {
        if (coldCoroutine != null || warmth.Value < config.Weather.ColdThreshold)
            return;

        if (Random.value < config.Weather.ColdSwitchChance)
            coldCoroutine = StartCoroutine(Cold());
    }

    private IEnumerator Warmth()
    {
        Debug.Log("Warmth start");

        warmthIndicator.SetActive(true);
        weatherCurtain.DOColor(warmthColor, colorSwitchDuration);

        for (float time = 0f; time < config.Weather.ColdDuration; time += Time.deltaTime)
        {
            warmth.Value += config.Weather.AdditionalWarmthIncrease * Time.deltaTime;
            yield return null;
        }

        weatherCurtain.DOColor(normalColor, colorSwitchDuration);
        warmthIndicator.SetActive(false);
        warmthCoroutine = null;

        Debug.Log("Warmth end");
    }

    private IEnumerator Cold()
    {
        Debug.Log("Cold start");

        coldIndicator.SetActive(true);
        weatherCurtain.DOColor(coldColor, colorSwitchDuration);

        for (float time = 0f; time < config.Weather.ColdDuration; time += Time.deltaTime)
        {
            warmth.Value -= config.Weather.AdditionalColdDecrease * Time.deltaTime;
            yield return null;
        }

        weatherCurtain.DOColor(normalColor, colorSwitchDuration);
        coldIndicator.SetActive(false);
        coldCoroutine = null;

        Debug.Log("Cold end");
    }
}
