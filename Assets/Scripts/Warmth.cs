using System;
using UnityEngine;
using UnityEngine.UI;

public class Warmth : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 1f)]
    private float value = 0.5f;

    [SerializeField] private GameConfig config;
    [SerializeField] private Gradient colorGradient;
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;

    public event Action<float> Changed;

    public float Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
            Changed?.Invoke(value);
            slider.value = this.value;
            fill.color = colorGradient.Evaluate(this.value);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.IsOver)
            return;

        Value -= config.Warmth.DecreaseRate * Time.deltaTime;

        /*if (value >= 1f)
            GameManager.Instance.Win();*/
        if (Value <= 0f)
            GameManager.Instance.GameOver();
    }
}
