using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuContainer;
    public Button ResumeButton;
    public Button PauseButton;
    public Button MainMenuButton;
    public Button ExitButton;


    public void Awake()
    {
        ResumeButton.onClick.AddListener(() => Resume());
        PauseButton.onClick.AddListener(() => Pause());
        MainMenuButton.onClick.AddListener(() => SceneManager.LoadScene(0));
        ExitButton.onClick.AddListener(() => Application.Quit(0));
        PauseMenuContainer.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PauseMenuContainer.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        PauseMenuContainer.SetActive(true);
    }
}
