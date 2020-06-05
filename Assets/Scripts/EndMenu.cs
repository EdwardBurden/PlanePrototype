using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private Text Title = null;
    [SerializeField] private InputField NameField = null;
    [SerializeField] private Button Cancel;
    [SerializeField] private Button Confirm;

    private const string StartText = "Game Over: Save to Leaderboard?";
    private const string NameText = "Enter name to continue";
    private const string OverWriteText = "Save Already exists , overwrite or cancel?";

    private void Awake()
    {
        gameObject.SetActive(false);
    }


    public void Open()
    {
        gameObject.SetActive(true);

        Confirm.onClick.RemoveAllListeners();
        Confirm.onClick.AddListener(() => Save());

        Cancel.onClick.RemoveAllListeners();
        Cancel.onClick.AddListener(() => CancelSave());

        NameField.gameObject.SetActive(false);

        Title.text = StartText;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Save()
    {
        NameField.gameObject.SetActive(true);
        Title.text = NameText;
        Confirm.onClick.RemoveAllListeners();
        Confirm.onClick.AddListener(() => NameEntered());
    }

    public void NameEntered()
    {
        SaveFile saveFile = SaveFileManager.GetAttempt(NameField.text);
        if (saveFile == null)
        {
            SaveFileManager.SaveAttempt(NameField.text, GameManger.Instance.Wave.WaveNumber);
            SceneManager.LoadScene(0);
        }
        else
        {
            Title.text = OverWriteText;
            Confirm.onClick.RemoveAllListeners();
            Confirm.onClick.AddListener(() => OverWrite());
        }
    }

    public void CancelSave()
    {
        SceneManager.LoadScene(0);
    }

    public void OverWrite()
    {
        SaveFileManager.SaveAttempt(NameField.text, GameManger.Instance.Wave.WaveNumber);
        SceneManager.LoadScene(0);
    }
}
