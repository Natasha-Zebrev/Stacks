using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class WinFailUI : MonoBehaviour
{   
    [SerializeField] private GameObject failMenu;
    [SerializeField] private Button restart;
    [SerializeField] private Button quit;

    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        failMenu.SetActive(false);
    }
    void Awake() {
        restart.onClick.AddListener(() => SceneManager.LoadScene("Level1"));
        quit.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        
    }

    //Used to disable the win and fail menus at the start of each level.
    public void Disable() {
        failMenu.SetActive(false);
    }

    public void Fail() {
        failMenu.SetActive(true);
    }
}