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
    public Text RemainingHPTitle;
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
        {
            WinLabel.text = "Wygrałeś";
            RemainingHPTitle.text = "Pozostałe zdrowie awatara:";
            RemainingHPLabel.text = _remainingHP.ToString();
        }
        else
        {
            WinLabel.text = "Przegrałeś";
            RemainingHPTitle.text = "Pozostałe zdrowie wszystkich przeciwników:";
            RemainingHPLabel.text = CombatManager.EnemyHPSum.ToString();
        }

        TimeLabel.text = Math.Round(_time, 2).ToString() + "s";
    }
}
