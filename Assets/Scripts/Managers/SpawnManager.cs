using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : NetworkBehaviour
{
    [SerializeField] NetworkPrefabRef coinPrefab;
    [SerializeField] float spawnRange = 60f;
    [SerializeField] float spawnPosY = 0.5f;
    [SerializeField] float startDelay = 2;
    [SerializeField] float spawnInterval = 10f;
    // Start is called before the first frame update

    public override void Spawned()
    {
        if(HasStateAuthority)
        {
            InvokeRepeating("SpawnRandomCoins", startDelay, spawnInterval);
        }
    }

    void SpawnRandomCoins()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRange, spawnRange), spawnPosY, Random.Range(-spawnRange, spawnRange));

        Runner.Spawn(coinPrefab, spawnPos);
        //Instantiate(coinPrefab, spawnPos, coinPrefab.transform.rotation);
    }
}
