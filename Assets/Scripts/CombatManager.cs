using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    public List<GameObject> Enemies;
    public GameObject Player;
    private double _timer;

    public static int RemainingPlayerHP;
    public static double TimePlayed;
    public static bool Win;
    void Start()
    {
        Enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Character"));
        Player = GameObject.FindGameObjectWithTag("Player");
        foreach(Combat combat in FindObjectsOfType<Combat>())
        {
            combat.CombatManager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
    }

    public void Kill(GameObject victim)
    {
        if(victim.tag == "Player")
        {
            KillPlayer();
        }
        else
        {
            KillEnemy(victim);
        }
    }

    public void KillEnemy(GameObject enemy)
    {
        Enemies.Remove(enemy);
        Destroy(enemy);
        if(Enemies.Count == 0)
        {
            Win = true;
            RemainingPlayerHP = Player.GetComponent<Combat>().CurrentHitPoints;
            TimePlayed = _timer;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void KillPlayer()
    {
        Win = false;
        RemainingPlayerHP = 0;
        TimePlayed = _timer;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
