using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


public class scri_GameController : MonoBehaviour
{
    //the variables for tracking analytics
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
        //track playtime
        playTime += Time.deltaTime;
    }

    public void EnemKilled()
    {
        //track total amount of enemies killed (if players kill all enemies)
        enemiesKilled++;
    }
    public void PlayHit()
    {
        //Track how many times the player is getting hit
        playerHit++;
    }
    public void HealthUp()
    {
        //track how many times playre picks up a health potion
        healthPickedUp++;
    }
    public void PlayDead()
    {
        //how many times a player dies on this playthrough
        playerDeaths++;
    }

    public void ResetStats()
    {
        //resets all the states for this playthrough
        enemiesKilled = 0;
        playerHit = 0;
        healthPickedUp = 0;
    }

    public void ReportData()
    {
        //report all the data
        //this is called in multiple places including the player's events, the enemies events,
        //and the PlayerPickup script made by us
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


