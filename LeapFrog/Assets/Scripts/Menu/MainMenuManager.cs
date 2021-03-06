using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button play;
    [SerializeField] private Button controls;
    [SerializeField] private Button credits;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        play.onClick.AddListener(() => LoadingScreen.LoadScene("LevelSelect"));
        controls.onClick.AddListener(() => LoadingScreen.LoadScene("Controls"));
        credits.onClick.AddListener(() => LoadingScreen.LoadScene("Credits"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
