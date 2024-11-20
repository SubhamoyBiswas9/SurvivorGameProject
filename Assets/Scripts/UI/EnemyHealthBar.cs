using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] Slider healthBar;

    private void OnEnable()
    {
        EnemySM.OnHitEvent += OnHitEvent;
    }

    private void OnDisable()
    {
        EnemySM.OnHitEvent -= OnHitEvent;
    }

    private void OnHitEvent(Health health)
    {
        if (health.GetCurrentHealth() <= 0)
        {
            healthBar.gameObject.SetActive(false);
            GetComponent<FollowWorld>().SetLookAt(null);
        }
        else
        {
            if (!healthBar.gameObject.activeInHierarchy)
                healthBar.gameObject.SetActive(true);

            GetComponent<FollowWorld>().SetLookAt(health.transform);
            healthBar.value = (float)health.GetCurrentHealth() / health.GetMaxHealth();

        }

    }
}
