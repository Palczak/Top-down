using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    private bool _win;
    private double _time;
    private int _remainingHP;

    public Text WinLabel;
    public Text TimeLabel;
    public Text RemainingHPLabel;

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Awake()
    {
        _win = CombatManager.Win;
        _time = CombatManager.TimePlayed;
        _remainingHP = CombatManager.RemainingPlayerHP;

        if (_win)
            WinLabel.text = "Wygrałeś";
        else
            WinLabel.text = "Przegrałeś";

        TimeLabel.text = Math.Round(_time,2).ToString() + "s";

        RemainingHPLabel.text = _remainingHP.ToString();
    }
}
