using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ReactiveProperties;
using System;

public class Warmth : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float value = 0.5f;
    [SerializeField] private FloatProperty reduction;
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

        Value -= reduction.Value * Time.deltaTime;

        /*if (value >= 1f)
            GameManager.Instance.Win();*/
        if (Value <= 0f)
            GameManager.Instance.GameOver();
    }
}
