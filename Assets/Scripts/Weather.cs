using ReactiveProperties;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weather : MonoBehaviour
{
    [SerializeField] private FloatProperty warmthReduction;
    [SerializeField] private FloatProperty additionalWarmthReduction;
    [SerializeField] private FloatProperty coldThreshold;
    [SerializeField] private FloatProperty coldDuration;
    [SerializeField] private FloatProperty colorSwitchDuration;
    [SerializeField] private ColorProperty normalColor;
    [SerializeField] private ColorProperty coldColor;
    [SerializeField] private FloatProperty switchChanceAtThresholdValue;
    [SerializeField] private FloatProperty switchChanceAtMaxValue;
    [SerializeField] private Warmth warmth;
    [SerializeField] private Image weatherCurtain;
    [SerializeField] private GameObject coldIndicator;

    private Coroutine coldCoroutine;
    private WaitForSeconds waitColdDuration;
    private float startSwitchTime;

    private void Awake()
    {
        startSwitchTime = Time.time - colorSwitchDuration.Value;
        waitColdDuration = new WaitForSeconds(coldDuration.Value);
        coldDuration.Changed += (value) => waitColdDuration = new WaitForSeconds(value);
    }

    private void Update()
    {
        float t = Mathf.InverseLerp(
            startSwitchTime,
            startSwitchTime + colorSwitchDuration.Value,
            Time.time);

        if (coldCoroutine != null)
        {
            coldIndicator.SetActive(true);
            weatherCurtain.color = Color.Lerp(normalColor.Value, coldColor.Value, t);
        }
        else
        {
            coldIndicator.SetActive(false);
            weatherCurtain.color = Color.Lerp(coldColor.Value, normalColor.Value, t);
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
        if (value < coldThreshold.Value || coldCoroutine != null)
            return;

        float chance = Mathf.Lerp(
            switchChanceAtThresholdValue.Value, 
            switchChanceAtMaxValue.Value, 
            value);

        if (Random.value < chance)
            coldCoroutine = StartCoroutine(Cold());
    }

    private IEnumerator Cold()
    {
        warmthReduction.Value += additionalWarmthReduction.Value;
        startSwitchTime = Time.time;
        yield return waitColdDuration;
        warmthReduction.Value -= additionalWarmthReduction.Value;
        startSwitchTime = Time.time;
        coldCoroutine = null;
    }
}
