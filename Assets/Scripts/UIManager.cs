using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Slider _healthSlider;

    private void Start()
    {
        
    }


    public void UpdateHealthSliderMax(float health)
    {

        _healthSlider.maxValue = health;
        _healthSlider.value = health;
    }

    public void UpdateHealthSlider(float health)
    {

        _healthSlider.value = health;
    }

    
}
