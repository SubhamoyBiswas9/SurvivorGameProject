using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject gameEndScreen;
    [SerializeField] TMP_Text killCountText;
    [SerializeField] TMP_Text coinsCountText;

    private void OnEnable()
    {
        ScoreManager.Instance.OnGameOver += ScoreManager_OnGameOver;
    }

    private void OnDisable()
    {
        ScoreManager.Instance.OnGameOver -= ScoreManager_OnGameOver;
    }

    private void ScoreManager_OnGameOver(int kills, int coins)
    {
        gameEndScreen.SetActive(true);
        killCountText.text = kills.ToString();
        coinsCountText.text = coins.ToString();
    }

    public void Replay()
    {
        ScoreManager.Instance.ResetGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
