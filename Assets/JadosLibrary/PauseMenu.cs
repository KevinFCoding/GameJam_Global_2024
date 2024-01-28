using UnityEngine;
using UnityEngine.SceneManagement;

// Script a placer sur un Objet "PauseManager" ou "PauseMenu"
// Placer en enfant un canva qui servira de menu pause 

public class PauseMenu : MonoBehaviour
{
    [HideInInspector] public static bool gameIsPaused = false;
    [Header("Canva Menu Pause")]
    [SerializeField] GameObject _pauseMenuUI;
    [SerializeField] PlayerShoot _playershoot;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _playershoot.isAlive)
        {
            if (gameIsPaused) { Resume(); } else { Paused(); }
        }
    }

    public void Resume() // Fonction de reprise de jeu
    {
        gameIsPaused = false;
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    public void Paused() // Fonction de pause 
    {
        gameIsPaused = true;
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Retry() // Fonction qui relance la scene 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameIsPaused = false;
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void Menu() // Fonction qui relance la scene 
    {
        gameIsPaused = false;
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }
}   
