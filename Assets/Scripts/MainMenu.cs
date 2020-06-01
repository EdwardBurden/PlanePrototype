using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button NewGameButton;
    public Button LeaderBoardButton;
    public Button ExitButton;


    public void Awake()
    {
        NewGameButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        LeaderBoardButton.onClick.AddListener(() => SceneManager.LoadScene(2));
        ExitButton.onClick.AddListener(() => Application.Quit(0));
    }


}
