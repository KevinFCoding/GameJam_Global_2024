using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CringeBar : MonoBehaviour
{
    [SerializeField] private GameObject _cringeBar;
    public InputField cringeSlider;
    private float cringeValue;
    // Start is called before the first frame update
    void Start()
    {
        SetCringe(0);

    }

    public void SetCringe(float cringe)
    {
        Debug.Log("Cringe value is increasing "+cringeValue);

        cringeValue = cringe;
        //cringeSlider.text = cringeValue.ToString();
    }
    // get cringe
    public float GetCringe()
    {
        return cringeValue;
    }

    void Update()
    {
        float newCringeValue = GetCringe();

        if (cringeValue != newCringeValue)
        {
            //SetCringe(newCringeValue);
        }
    }

}
