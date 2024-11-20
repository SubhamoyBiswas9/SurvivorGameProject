using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] GameObject gameEndScreen;
    [SerializeField] TMP_Text killCountText;
    [SerializeField] TMP_Text coinsCountText;

    int coinsCollected;
    int enemiesKilled;

    public void OnCollectedCoin()
    {
        coinsCollected++;
    }

    public void OnEnemyKilled()
    {
        enemiesKilled++;
    }

    public void GameOver()
    {
        gameEndScreen.SetActive(true);
        killCountText.text = enemiesKilled.ToString();
        coinsCountText.text = coinsCollected.ToString();
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
