using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [SerializeField] private Button back;
    [SerializeField] private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        back.onClick.AddListener(() => LoadingScreen.LoadScene("MainMenu"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
