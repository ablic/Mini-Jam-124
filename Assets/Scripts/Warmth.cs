using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Warmth : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float value = 0.5f;
    [SerializeField] private Gradient colorGradient;
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;

    public float Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
            slider.value = this.value;
            fill.color = colorGradient.Evaluate(this.value);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.IsOver)
            return;

        Value -= GameManager.Config.Warmth.Reduction * Time.deltaTime;

        if (value >= 1f)
            GameManager.Instance.Win();
        else if (Value <= 0f)
            GameManager.Instance.GameOver();
    }
}
