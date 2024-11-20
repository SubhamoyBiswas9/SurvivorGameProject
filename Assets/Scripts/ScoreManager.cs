using System;

public class ScoreManager : Singleton<ScoreManager>
{
    int coinsCollected;
    int enemiesKilled;

    public event Action<int, int> OnGameOver;

    public void ResetGame()
    {
        coinsCollected = 0;
        enemiesKilled = 0;
    }

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
        OnGameOver?.Invoke(enemiesKilled, coinsCollected);
    }
}
