using UnityEngine;

public class PlayerDammge : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private int dammge;/*so the enemy choses how much damge the 
    player gets like a strong enemy does more damge*/

    [SerializeField] HealthBar healthbar;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    
    /*void Update()
    {
        when the enemys are implemented detecting 
        if we are hit will go here 
    }
    */

    void TakeDammge()//gets called by a messege from the enemy
    {
        currentHealth -= dammge;
        healthbar.SetHealth(currentHealth);
    }
}
