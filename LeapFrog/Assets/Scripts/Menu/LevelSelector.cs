using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private List<GameObject> levelButtons;
    [SerializeField] private GameObject previous;
    [SerializeField] private GameObject next;

    private int previousLevels = -3;
    private int currentLevels = 0;
    private int nextLevels = 3;
    void Start()
    {
        enableOnlyFirst();

        next.GetComponent<Button>().onClick.AddListener(() => changeNextButton());
        previous.GetComponent<Button>().onClick.AddListener(() => changePreviousButton());

        Time.timeScale = 1;
        levelButtons[0].GetComponent<Button>().onClick.AddListener(() => LoadingScreen.LoadScene("Level1"));
        levelButtons[1].GetComponent<Button>().onClick.AddListener(() => LoadingScreen.LoadScene("Level2"));
        levelButtons[2].GetComponent<Button>().onClick.AddListener(() => LoadingScreen.LoadScene("Level3"));
        levelButtons[3].GetComponent<Button>().onClick.AddListener(() => LoadingScreen.LoadScene("Level4"));
        levelButtons[4].GetComponent<Button>().onClick.AddListener(() => LoadingScreen.LoadScene("Level5"));
        levelButtons[5].GetComponent<Button>().onClick.AddListener(() => LoadingScreen.LoadScene("Level6"));
        levelButtons[6].GetComponent<Button>().onClick.AddListener(() => LoadingScreen.LoadScene("Level7"));
        levelButtons[7].GetComponent<Button>().onClick.AddListener(() => LoadingScreen.LoadScene("Level8"));
        levelButtons[8].GetComponent<Button>().onClick.AddListener(() => LoadingScreen.LoadScene("Level9"));
        levelButtons[9].GetComponent<Button>().onClick.AddListener(() => LoadingScreen.LoadScene("Level10"));
        levelButtons[10].GetComponent<Button>().onClick.AddListener(() => LoadingScreen.LoadScene("Level11"));
    }
    
    private void enableOnlyFirst()
    {
        for(int i = 3; i < levelButtons.Count; i++)
            levelButtons[i].SetActive(false);
    }

    private void changeNextButton()
    {
        disableCurrentLevels();
        enableNextLevels();
        Debug.Log("Prev: " + previousLevels + "\nCurr: " + currentLevels + "\nNext: " + nextLevels);
    }

    private void changePreviousButton()
    {
        disableCurrentLevels();
        enablePreviousLevels();
        Debug.Log("Prev: " + previousLevels + "\nCurr: " + currentLevels + "\nNext: " + nextLevels);
    }
    private void disableCurrentLevels()
    {
        for (int i = currentLevels; i < nextLevels; i++)
            levelButtons[i].SetActive(false);
    }
    private void enableNextLevels()
    {
        for (int i = nextLevels; i < nextLevels + 3; i++)
            levelButtons[i].SetActive(true);
        previousLevels = currentLevels;
        currentLevels = nextLevels;
        nextLevels += 3;
    }

    private void enablePreviousLevels()
    {
        for(int i = previousLevels; i < currentLevels; i++)
            levelButtons[i].SetActive(true);
        nextLevels = currentLevels;
        currentLevels = previousLevels;
        previousLevels -= 3;
    }


    void Update()
    {
        if (nextLevels >= levelButtons.Count)
        {
            next.SetActive(false);
            previous.SetActive(true);
        }
        else if (previousLevels < 0)
        {
            previous.SetActive(false);
            next.SetActive(true);
        }
        else
        {
            next.SetActive(true);
            previous.SetActive(true);
        }
    }
}
