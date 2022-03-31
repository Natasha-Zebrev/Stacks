using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;

    [SerializeField] private Image healthBar;
    [SerializeField] private TMP_Text gameTime;
    [SerializeField] private TMP_Text stackSize;
    [SerializeField] private int allyCount;
    [SerializeField] private int targetSize;
    private float gameTimeFloat = 0;

     void Start()
    {
        //This condition protects against the game trying to divide by zero or ending with just the player in the stack.
        if(targetSize < 2) {
            targetSize = 2;
        }
        instance = this;
    }

    void Update()
    {
        ShowGameTime();
        ShowStackSize();

        //Restart the level
        if(Input.GetKeyDown(KeyCode.R) && allyCount != targetSize)
        {
            LoadingScreen.LoadScene("Level1");
        }
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

    //Displays the size of the player's stack and handles the win
    public void ShowStackSize()
    {
        int stackCount = PlayerController.instance.stack.Count;
        allyCount = stackCount - 1;
        stackSize.text = "Allies: " + allyCount + "/" + targetSize;
    }

    //Checks to see if the player has won the level; changes to the level select if true
    public void CheckWin(int allyCount)
    {
        if (allyCount / targetSize >= 1)
        {
            stackSize.color = new Color(0, 255, 0, 2.5f);
            StartCoroutine(winWait());
        }
    }

    //Makes the game wait after winning (intended to be used before loading the level select scene)
    private IEnumerator winWait()
    {
        float timeUntilReload = Time.realtimeSinceStartup + 3;
        while (timeUntilReload > Time.realtimeSinceStartup)
        {
            yield return null;
        }
        LoadingScreen.LoadScene("LevelSelect");
    }
}
