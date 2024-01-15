using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    [SerializeField] private TMP_Text textMoney;
    [SerializeField] private TMP_Text textLevel;
    [SerializeField] private TMP_Text textFPS;  
    [SerializeField] private Slider sliderLevel;
    [SerializeField] private float valueToNextLevel;

    private void Start()
    {
        sliderLevel.maxValue = valueToNextLevel;
    }
    private void Update()
    {
        UpdateFPS();
    }
    public void UpdateXP(float valueToIncrease)
    {
        float newValue = sliderLevel.value + valueToIncrease;

        if (newValue >= valueToNextLevel)
        {
            float excessValue = newValue - valueToNextLevel;
            sliderLevel.value = excessValue;

            DataManager.Instance.LevelUp();

            textLevel.text = DataManager.Instance.BaseLevel.ToString();
        }
        else
        {
            sliderLevel.value = newValue;
        }
    }
    public void UpdateMoney(float value)
    {
        textMoney.text = $" $ {value}";
    }
    private void UpdateFPS()
    {
        float currentFPS = 1.0f / Time.deltaTime;
        textFPS.text = $"FPS: {currentFPS:0}";
    }
}