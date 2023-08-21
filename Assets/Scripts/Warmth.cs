using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Warmth : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float value = 0.5f;
    [Min(0f)]
    [SerializeField] private float selfReduction = 0.05f;
    [SerializeField] private Gradient colorGradient;
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;

    private bool gameLost = false;

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }

        if (gameLost)
            return;

        Value -= selfReduction * Time.deltaTime;

        if (value >= 1f)
            Win();
        else if (Value <= 0f)
            GameOver();
    }

    private void Win()
    {
        music.Stop();
        gameLost = true;
        Time.timeScale = 0f;
        winPanel.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(winSound);
    }

    private void GameOver()
    {
        music.Stop();
        gameLost = true;
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(loseSound);
    }
}
