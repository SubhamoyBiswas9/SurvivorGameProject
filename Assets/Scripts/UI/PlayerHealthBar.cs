using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Slider healthBar;

    Health playerHealth;
    
    private void OnEnable()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        playerHealth.OnHealthUpdate += PlayerHealth_OnHealthUpdate;
    }

    private void OnDisable()
    {
        playerHealth.OnHealthUpdate -= PlayerHealth_OnHealthUpdate;
    }

    private void PlayerHealth_OnHealthUpdate(int currentHealth, int maxHealth)
    {
        //Debug.Log(currentHealth);
        healthBar.value = (float)currentHealth / maxHealth;

    }
}
