using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    [SerializeField] private Button exit;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        exit.onClick.AddListener(() => LoadingScreen.LoadScene("MainMenu"));
    }
}
