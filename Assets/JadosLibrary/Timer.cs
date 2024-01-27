using TMPro;
using UnityEngine;

// Script de Timer
// Script
//
// placer sur un GameObject "Timer" ou "TimerManager"

// Configurer depuis l'inspector le temps de depart du timer (en secondes)
// Cocher la case si le timer est en incrementation 
// Configurer les parametres si le timer est visible a l'ecran

public class Timer : MonoBehaviour
{
    private float seconds; // Secondes qui passent 

    [Header("Configuration de depart du Timer")]
    [SerializeField] float _secondToStart; // Temps de depart du Timer
    [SerializeField] bool _isIncrement; // Incrementation ou decrementation du Timer

    [Header("Affichage du Timer")]
    [SerializeField] bool _isTextToShow; // Il y a un text pour afficher le timer
    [SerializeField] TMP_Text scoreText;


    void Start()
    {
        seconds = _secondToStart;
    }
    void Update()
    {
        if (_isIncrement)
        {
            IncreaseTimer();
        }
        else
        {
            DecrementTimer();
        }

    }

    void IncreaseTimer()
    {
        seconds += Time.deltaTime;
        if (_isTextToShow)
        {
            ShowTimer();
        }
    }

    public void DecrementTimer()
    {
        seconds -= Time.deltaTime;
        if (_isTextToShow)
        {
            ShowTimer();
        }
    }

    public void ShowTimer()
    {
        scoreText.text = seconds.ToString();
        float minute = Mathf.FloorToInt(seconds / 60);
        float sec = Mathf.FloorToInt(seconds % 60);
        if (sec < 10)
        {
            scoreText.text = minute + " :0" + sec.ToString();
        }
        else
        {
            scoreText.text = minute + " : " + sec.ToString();
        }
    }
}



