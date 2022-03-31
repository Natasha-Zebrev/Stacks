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
        level1.onClick.AddListener(() => LoadingScreen.LoadScene("RealLevel1"));
    }
}
