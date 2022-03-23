using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button play;
    [SerializeField] private Button credits;
    // Start is called before the first frame update
    void Start()
    {
        play.onClick.AddListener(() => LoadingScreen.LoadScene("LevelSelect"));
        credits.onClick.AddListener(() => LoadingScreen.LoadScene("Credits"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
