using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SoundManager.Instance.PlayCoinCollectSFX();
            ScoreManager.Instance.OnCollectedCoin();
            Destroy(gameObject);
        }
    }
}
