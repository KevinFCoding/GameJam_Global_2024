using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject _credits;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenCredits()
    {
        _credits.SetActive(true);
    }
    
    public void CloseCredits()
    {
        _credits.SetActive(false);

    }

    public void Quit()
    {
        Application.Quit();
    }

}
