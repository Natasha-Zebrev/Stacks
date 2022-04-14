using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private Button level1;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        level1.onClick.AddListener(() => LoadingScreen.LoadScene("Level1"));
    }
}
