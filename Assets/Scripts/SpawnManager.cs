using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Terrain terrain;
    [SerializeField] private int maxCoins = 10;
    [SerializeField] private ScoreManager scoreManager;

    private bool spawningActive = false;

    public void StartSpawning()
    {
        spawningActive = true;
        RespawnCoins();
    }

    public void StopSpawning()
    {
        spawningActive = false;
    }

    private void Update()
    {
        if (!spawningActive) return;

        int currentCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        if (currentCoins < maxCoins)
        {
            SpawnCoin();
        }
    }

    public void RespawnCoins()
    {
        foreach (var coin in GameObject.FindGameObjectsWithTag("Coin"))
        {
            Destroy(coin);
        }

        for (int i = 0; i < maxCoins; i++)
        {
            SpawnCoin();
        }
    }

    private void SpawnCoin()
    {
        const float minDistance = 1.0f;
        const int maxAttempts = 20;

        Vector3 spawnPosition = Vector3.zero;
        bool validPositionFound = false;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            float x = Random.Range(0f, terrain.terrainData.size.x);
            float z = Random.Range(0f, terrain.terrainData.size.z);
            float y = terrain.SampleHeight(new Vector3(x, 0f, z));

            spawnPosition = new Vector3(x, y + 0.5f, z);

            bool tooClose = false;
            foreach (var coin in GameObject.FindGameObjectsWithTag("Coin"))
            {
                if (Vector3.Distance(spawnPosition, coin.transform.position) < minDistance)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
            {
                validPositionFound = true;
                break;
            }
        }

        if (validPositionFound)
        {
            float randomY = Random.Range(0f, 360f);
            Quaternion rotation = Quaternion.Euler(90f, randomY, 0f);

            GameObject newCoin = Instantiate(coinPrefab, spawnPosition, rotation);

            Coin coinScript = newCoin.GetComponent<Coin>();
            if (coinScript != null && scoreManager != null)
            {
                coinScript.SetScoreManager(scoreManager);
            }
        }
    }


}
