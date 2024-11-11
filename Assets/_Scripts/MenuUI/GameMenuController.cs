using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    public static GameMenuController instance;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject player;

    private PlayerComponentsManager playerComponents;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (player != null)
        {
            
            playerComponents = player.GetComponent<PlayerComponentsManager>();
            if (playerComponents != null)
            {
                playerComponents.healthScript.OnDeath += ShowDeathMenu;
                playerComponents.playerInput.actions["Pause"].performed += PauseInput;
            }

        }
  
    }

    private void PauseInput(InputAction.CallbackContext context)
    {
        if (pauseMenu.activeInHierarchy)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        TimeController.StopTime(this);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        TimeController.ResumeTimeNoCaller();
    }

 

    public void ShowDeathMenu()
    {
        deathMenu.SetActive(true);
        TimeController.StopTime(this);
    }

    private void OnDestroy()
    {
        if (playerComponents != null)
        {
            playerComponents.healthScript.OnDeath -= ShowDeathMenu;
            playerComponents.playerInput.actions["Pause"].performed -= PauseInput;
        }
    }
    public void RestartGame()
    {
        TimeController.ResumeTimeNoCaller();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
