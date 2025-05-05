using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    [SerializeField] float spawnRangeX = 5f;
    public void PlayerJoined(PlayerRef player)
    {
        if(Runner.ActivePlayers.Count() > GameManager.Instance.maxPlayers)
        {
            Debug.Log("True");
            return;
        }
        float randomPosX = Random.Range(-spawnRangeX, spawnRangeX);
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(PlayerPrefab, new Vector3(randomPosX, 1, 0), Quaternion.identity, player);
            UIManager.instance.SceneLoaded();
        }
    }
}
