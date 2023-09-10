using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weather : MonoBehaviour
{
    [SerializeField] private GameConfig config;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color coldColor;
    [SerializeField] private Warmth warmth;
    [SerializeField] private Image weatherCurtain;
    [SerializeField] private GameObject coldIndicator;

    private Coroutine coldCoroutine;
    private float startSwitchTime;

    private void Start()
    {
        startSwitchTime = Time.time - config.WeatherColorSwitchDuration;
    }

    private void Update()
    {
        float t = Mathf.InverseLerp(
            startSwitchTime,
            startSwitchTime + config.WeatherColorSwitchDuration,
            Time.time);

        if (coldCoroutine != null)
        {
            warmth.Value -= config.WeatherAdditionalWarmthDecrease * Time.deltaTime;

            coldIndicator.SetActive(true);
            weatherCurtain.color = Color.Lerp(normalColor, coldColor, t);
        }
        else
        {
            coldIndicator.SetActive(false);
            weatherCurtain.color = Color.Lerp(coldColor, normalColor, t);
        }
    }

    private void OnEnable()
    {
        warmth.Changed += OnWarmthChanged;
    }

    private void OnDisable()
    {
        warmth.Changed -= OnWarmthChanged;
    }

    private void OnWarmthChanged(float value)
    {
        if (value < config.WeatherColdThreshold || coldCoroutine != null)
            return;

        float chance = Mathf.Lerp(
            config.WeatherSwitchChanceAtThresholdValue,
            config.WeatherSwitchChanceAtMaxValue, 
            value);

        if (Random.value < chance)
            coldCoroutine = StartCoroutine(Cold());
    }

    private IEnumerator Cold()
    {
        startSwitchTime = Time.time;
        yield return new WaitForSeconds(config.WeatherColdDuration);
        startSwitchTime = Time.time;
        coldCoroutine = null;
    }
}
