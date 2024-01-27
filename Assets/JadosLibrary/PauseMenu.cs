using UnityEngine;
using UnityEngine.SceneManagement;

// Script a placer sur un Objet "PauseManager" ou "PauseMenu"
// Placer en enfant un canva qui servira de menu pause 

public class PauseMenu : MonoBehaviour
{
    [HideInInspector] public static bool gameIsPaused = false;
    [Header("Canva Menu Pause")]
    [SerializeField] GameObject _pauseMenuUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused) { Resume(); } else { Paused(); }
        }
    }

    public void Resume() // Fonction de reprise de jeu
    {
        gameIsPaused = false;
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }
    public void Paused() // Fonction de pause 
    {
        gameIsPaused = true;
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Retry() // Fonction qui relance la scene 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }

    public void Menu() // Fonction qui relance la scene 
    {
        Resume();
        SceneManager.LoadScene("MenuScene");
    }
}   
