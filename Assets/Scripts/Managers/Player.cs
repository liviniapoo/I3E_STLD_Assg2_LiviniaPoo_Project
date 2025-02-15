/*
 * Author: Livinia Poo
 * Date: 24/06/2024
 * Description: 
 * Player variables and inputs
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Determines player's starting max health
    /// </summary>
    public static float playerHealth = 100;
    public static float playerMaxHealth = 100;
    public static bool isDead = false;

    /// <summary>
    /// Variables to check whether player has a gun
    /// </summary>
    public static bool hasGun = false;
    /// <summary>
    /// Attaching gun to player
    /// </summary>
    public GameObject gunOnPlayer;

    /// <summary>
    /// Variables to check how much ammo player has
    /// </summary>
    public static int ammoBoxCount = 0;
    public static int ammoCount = 0;

    /// <summary>
    /// Variable to check how many medkits player has
    /// </summary>
    public static int medkitCount = 0;

    ///<summary>
    /// Referencing Audio Clips for Effects
    /// </summary>
    public AudioSource sfxHealAudio;

    /// <summary>
    /// Determines how much health is gained upon using medkit
    /// </summary>
    public int healthRestoredPerHeal = 20;

    /// <summary>
    /// Variable to check how many gears player has
    /// </summary>
    public static int gearCount = 0;

    /// <summary>
    /// Declares that player starts without gemstone
    /// </summary>
    public static bool gemstoneCollected = false;

    /// <summary>
    /// Declares that player starts without crystal essence
    /// </summary>
    public static bool essenceCollected = false;
    public static int essenceCount = 0;

    /// <summary>
    /// Player heals health when hit keybind
    /// </summary>
    private void OnHeal()
    {
        if (playerHealth < 100 && medkitCount > 0)
        {
            playerHealth += healthRestoredPerHeal;
            medkitCount -= 1;
            Debug.Log(playerHealth);
        }
        else if (medkitCount == 0)
        {
            Debug.Log("No more medkits!");
        }
    }

    /// <summary>
    /// Allows player to take damage when attacked by enemies, die when health is 0
    /// </summary>
    /// <param name="damageAmt"></param>
    public void TakeDamage(float damageAmt)
    {
        playerHealth -= damageAmt;

        if (playerHealth <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    /// <summary>
    /// Destroys player object upon death
    /// </summary>
    void Die()
    {
        GameManager.instance.ShowDeathUI();
        ResetHealth();
        GameManager.instance.ResetHealthBar();
    }

    /// <summary>
    /// Resetting the player's health
    /// </summary>
    public static void ResetHealth()
    {
        playerHealth = playerMaxHealth;
    }

    /// <summary>
    /// Disables gun on player on start
    /// </summary>
    private void Start()
    {
        gunOnPlayer.SetActive(false);
    }

    /// <summary>
    /// Enables gun on player once they collect a gun
    /// </summary>
    private void Update()
    {
        if(hasGun)
        {
            gunOnPlayer.SetActive(true);
        }
    }

    /// <summary>
    /// Handles signal for pausing the game
    /// </summary>
    public void OnEscape()
    {
        if (PauseMenu.isPaused)
        {
            GameManager.instance.HidePauseUI();
        }
        else
        {
            GameManager.instance.ShowPauseUI();
        }
    }
}
