using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CringeBar : MonoBehaviour
{
    public Slider cringeSlider;
    public PlayerShoot _playerShoot;

    public void UpdateSliderLifeBar()
    {
        cringeSlider.value = _playerShoot._cringe;
    }
}
