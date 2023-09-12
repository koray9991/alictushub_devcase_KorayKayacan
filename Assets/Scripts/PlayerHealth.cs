using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public float maxHealth, health;
    public Image healthBar;
    public bool dead;
    PlayerMovement playerMovement;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        health = maxHealth;
        healthBar.fillAmount = health / maxHealth;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>())
        {
            health -= other.GetComponent<Bullet>().bulletDamage;
            healthBar.fillAmount = health / maxHealth;

            if (health <= 0)
            {
                if (!dead)
                {
                    dead = true;
                    playerMovement.Death();
                    GetComponent<PlayerMovement>().enabled = false;
                    GetComponent<PlayerAttack>().enabled = false;

                }
               
            }
        }
    }
}
