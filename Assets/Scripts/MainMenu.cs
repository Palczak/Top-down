using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Toggle _hardMode;
    void Awake()
    {
        _hardMode = FindObjectOfType<Toggle>();
    }

    public void StartGame()
    {
        Config.HardMode = _hardMode.isOn;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
