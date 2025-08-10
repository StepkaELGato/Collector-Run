using UnityEngine;

public class Coin : CollectibleItem
{
    private ScoreManager scoreManager;
    public int value = 1;

    public void SetScoreManager(ScoreManager manager)
    {
        scoreManager = manager;
    }

    public override void Collect(GameObject collector)
    {
        if (scoreManager != null)
        {
            scoreManager.AddPoints(value);
        }

        base.Collect(collector);
    }
}
