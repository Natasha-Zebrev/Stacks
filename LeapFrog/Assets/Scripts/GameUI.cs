using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private TMP_Text gameTime;

    public void showHealthFraction(float fraction)
    {
        healthBar.fillAmount = fraction;
    }

    public void ShowGameTime(float oldTime)
    {
        gameTime.text = (oldTime + 1).ToString();
    }
}
