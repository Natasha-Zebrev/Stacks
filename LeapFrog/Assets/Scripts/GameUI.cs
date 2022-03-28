using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private TMP_Text gameTime;
    private float gameTimeFloat = 0;

    void Update()
    {
        ShowGameTime();
    }

    string FormatSeconds(float elapsed)
    {
        int seconds = (int)elapsed % 60;
        int minutes = (int)elapsed / 60;
        return String.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void showHealthFraction(float fraction)
    {
        healthBar.fillAmount = fraction;
    }

    public void ShowGameTime()
    {
        gameTimeFloat += Time.deltaTime;
        gameTime.text = FormatSeconds(gameTimeFloat);//new System.DateTime((long)gameTimeFloat * System.TimeSpan.TicksPerSecond).ToString();
    }
}
