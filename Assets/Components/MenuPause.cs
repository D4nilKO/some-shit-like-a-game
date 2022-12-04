using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{

    private bool isPaused = false;
    public GameObject pausePanel;
    [FormerlySerializedAs("pauseBTN")] public GameObject pauseBtn;
    
    [FormerlySerializedAs("timeChangerScr")] [SerializeField] private TimeManager timeManagerScr;
    
    public GameObject joystickPlayer;
    
    public void Start()
    {
        pausePanel.SetActive(false);
        pauseBtn.SetActive(true);
        joystickPlayer.SetActive(true);
    }

    public void Update()
    {
        PlayOrPause();
    }

    private void PlayOrPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            pauseBtn.SetActive(false);
            pausePanel.SetActive(true);
            joystickPlayer.SetActive(false);
            Time.timeScale = 0f;
            isPaused = true;
            StopAllCoroutines();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            pauseBtn.SetActive(true);
            pausePanel.SetActive(false);
            joystickPlayer.SetActive(true);
            isPaused = false;
            StopAllCoroutines();
            timeManagerScr.ApplyWaitBeforeContinueTime();
        }
    }

    public void Pause()
    {
        pauseBtn.SetActive(false);
        pausePanel.SetActive(true);
        joystickPlayer.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Play()
    {
        pauseBtn.SetActive(true);
        pausePanel.SetActive(false);
        joystickPlayer.SetActive(true);
        isPaused = false;
        StopAllCoroutines();
        timeManagerScr.ApplyWaitBeforeContinueTime();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Shop()
    {
        pauseBtn.SetActive(true);
        pausePanel.SetActive(false);
        joystickPlayer.SetActive(true);
        Time.timeScale = TimeManager.timeScale;
        isPaused = false;
    }

    public void ShopEx()
    {
        pauseBtn.SetActive(true);
        pausePanel.SetActive(false);
        joystickPlayer.SetActive(true);
        Time.timeScale = TimeManager.timeScale;
        isPaused = false;
    }
}