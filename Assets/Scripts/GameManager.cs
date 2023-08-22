using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameConfiguration activeConfiguration;
    [SerializeField] private Text timeToWinText;
    [SerializeField] private AudioSource music;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;

    private Coroutine timerCoroutine;

    public static GameManager Instance { get; private set; }
    public static GameConfiguration Config => Instance.activeConfiguration;

    public bool IsOver { get; private set; } = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        timerCoroutine = StartCoroutine(Timer());
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1f;
        }
    }

    public void Win()
    {
        Finish();
        winPanel.SetActive(true);
        music.PlayOneShot(winSound);
    }

    public void GameOver()
    {
        Finish();
        gameOverPanel.SetActive(true);
        music.PlayOneShot(loseSound);
    }

    private IEnumerator Timer()
    {
        timeToWinText.text = Config.Shared.TimeToWin.ToString();

        for (int time = Config.Shared.TimeToWin - 1; time > 0; time--)
        {
            yield return new WaitForSeconds(1f);
            timeToWinText.text = time.ToString();
        }

        Win();
    }

    private void Finish()
    {
        StopCoroutine(timerCoroutine);
        music.Stop();
        IsOver = true;
        Time.timeScale = 0f;
    }

}
