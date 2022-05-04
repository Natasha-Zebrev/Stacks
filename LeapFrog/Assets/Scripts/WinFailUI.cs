using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class WinFailUI : MonoBehaviour
{   
    //These fields are all related to the winMenu
    [SerializeField] private GameObject nextLevelGraphicHolder;
    [SerializeField] private Button nextLevelGraphicButton;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private TMP_Text winTimeText;
    [SerializeField] private Button levContinue;
    [SerializeField] private Button winRestart;
    [SerializeField] private Button winQuit;

    //These fields are all related to the failMenu
    [SerializeField] private GameObject failMenu;
    [SerializeField] private TMP_Text failTimeText;
    [SerializeField] private Button failRestart;
    [SerializeField] private Button failQuit;

    //This field gets the scene name to reload whatever level the player is on
    //if restart is clicked

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        disable();
    }
    void Awake() {
        String sceneName = SceneManager.GetActiveScene().name + "";
        int levelNumber = Int32.Parse(sceneName.Remove(0,5));
        if(levelNumber + 1 != 12)
        {   
            levContinue.onClick.AddListener(() => nextLevelGraphicHolder.SetActive(true));
            nextLevelGraphicButton.onClick.AddListener(() => SceneManager.LoadScene("Level" + (levelNumber + 1)));
        }
        else
        {
            levContinue.onClick.AddListener(() => SceneManager.LoadScene("LevelSelect"));
        }
        winRestart.onClick.AddListener(() => SceneManager.LoadScene(sceneName));
        winQuit.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        failRestart.onClick.AddListener(() => SceneManager.LoadScene(sceneName));
        failQuit.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        
    }

    //Used to disable the win and fail menus at the start of each level.
    public void disable() {
        winMenu.SetActive(false);
        failMenu.SetActive(false);
        nextLevelGraphicHolder.SetActive(false);
    }

    //Used to activate the win menu
    public void win() {
        freezeTime();
        winTimeText.text = "Time: " + GameUI.instance.gameTime.text;
        winMenu.SetActive(true);
        nextLevelGraphicHolder.SetActive(false);
    }

    //Used to activate the fail menu
    public void fail() {
        freezeTime();
        failTimeText.text = "Time: " + GameUI.instance.gameTime.text;
        failMenu.SetActive(true);
    }

    //Used to pause the game when a menu appears
    private void freezeTime() {
        Time.timeScale = 0;
    }
}