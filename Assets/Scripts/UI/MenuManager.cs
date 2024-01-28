using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] ParticleSystem _feuArtifice;
    [SerializeField] ParticleSystem _planete;

    [SerializeField] GameObject _credits;

    [SerializeField] GameObject _menu;
    void Start()
    {
        _planete.Play();
        Invoke("CallFeuArtifice", 8f);
    }

    public void CallFeuArtifice()
    {
        _planete.Stop();
        _feuArtifice.Play();
        Invoke("ShowMenu", 3f);
    }

    public void ShowMenu()
    {
        _menu.SetActive(true);
        _feuArtifice.Stop();

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
