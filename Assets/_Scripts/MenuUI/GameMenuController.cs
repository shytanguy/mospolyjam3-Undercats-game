using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject player;

    private HealthScript playerHealth;

    private void Start()
    {
        if (player != null)
        {
            playerHealth = player.GetComponent<HealthScript>();
            if (playerHealth != null)
            {
                playerHealth.OnDeath += ShowDeathMenu;
            }
        }
        ShowMainMenu();
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowDeathMenu()
    {
        deathMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnDeath -= ShowDeathMenu;
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
