using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game over")]
    [SerializeField] private GameObject gameOverScreen;
    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;
    private tIME currentTime;
    private tIME maxTime;
    private tIME runningTime;
    private Health currentHealth;

    private void Awake()
    {
        currentHealth = GetComponent<Health>();
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseScreen.activeInHierarchy)
            {
                PauseGame(true);
            }
            else
            {
                PauseGame(false);
            }
        }
    }

    public void Gameover()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        currentTime = maxTime;
        //runningTime = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit(); //Only works on build
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //only on editor
#endif
    }

    public void PauseGame(bool status)
    {
        Debug.Log(status);
        pauseScreen.SetActive(status);

        if (status)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

        public void Level1()
    {
        SceneManager.LoadScene(1);
        currentHealth.AddHealth(3);
    }

    public void Level2()
    {
        SceneManager.LoadScene(2);
        currentHealth.AddHealth(3);
    }

}
