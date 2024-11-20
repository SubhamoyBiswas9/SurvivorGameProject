using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth = 100;

    int currentHealth;

    public event Action<int,int> OnHealthUpdate;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        OnHealthUpdate?.Invoke(currentHealth, maxHealth);

        GetComponent<IHitHandler>().OnHit();

        if(currentHealth == 0)
        {
            GetComponent<IDeathHandler>()?.OnDeath();
        }    
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
