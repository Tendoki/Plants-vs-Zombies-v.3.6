using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button StartGame;
    public int levelComplete;

    void Start()
    {
        levelComplete = PlayerPrefs.GetInt("LevelComplete");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }
    }

    public void PlayButton()
    {
        Invoke("LoadTo", 1f);
    }

    void LoadTo()
    {
        SceneManager.LoadScene(levelComplete+1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResetGame()
    {
        levelComplete = 0;
        PlayerPrefs.DeleteAll();
    }
}
