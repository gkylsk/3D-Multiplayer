using Fusion;
using UnityEngine;

public class SpawnManager : NetworkBehaviour
{
    [SerializeField] NetworkPrefabRef coinPrefab;
    [SerializeField] float spawnRange = 60f;
    [SerializeField] float spawnPosY = 0.5f;
    [SerializeField] float startDelay = 2;
    [SerializeField] float spawnInterval = 10f;

    public override void Spawned()
    {
        if(HasStateAuthority)
        {
            InvokeRepeating("SpawnRandomCoins", startDelay, spawnInterval);
        }
    }

    //spawn coins in random locations
    void SpawnRandomCoins()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRange, spawnRange), spawnPosY, Random.Range(-spawnRange, spawnRange));

        Runner.Spawn(coinPrefab, spawnPos);
    }
}
