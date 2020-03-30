using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


public class scri_GameController : MonoBehaviour
{
    public int
        enemiesKilled,
        playerHit,
        healthPickedUp,
        playerDeaths;

    public float playTime;

    void Start()
    {
        CheckSingeleton();
        DontDestroyOnLoad(this.gameObject);
        ResetStats();
    }
    private void CheckSingeleton()
    {
        if (FindObjectsOfType<scri_GameController>().Length > 1)
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        playTime += Time.deltaTime;
    }

    public void EnemKilled()
    {
        enemiesKilled++;
    }
    public void PlayHit()
    {
        playerHit++;
    }
    public void HealthUp()
    {
        healthPickedUp++;
    }
    public void PlayDead()
    {
        playerDeaths++;
    }

    public void ResetStats()
    {
        enemiesKilled = 0;
        playerHit = 0;
        healthPickedUp = 0;
    }

    public void ReportData()
    {
        Analytics.CustomEvent("PlayData", new Dictionary<string, object>
        {
            {"amount_of_enemies_killed", enemiesKilled},
            {"amount_of_times_player_hit", playerHit},
            {"amount_of_health_potions_picked_up", healthPickedUp},
            {"amount_of_player_deaths", playerDeaths},
            {"play_time", playTime }
        });
        Debug.Log("data sent");
    }
}


