using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;
    UIManager uiManager;
    public List<NetworkObject> _alivePlayers = new List<NetworkObject>();

    public int maxPlayers = 6;
    public bool gameStarted = false;
    bool gameEnded = false;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        uiManager = UIManager.instance;
    }

    public void AddPlayers(NetworkObject obj)
    {
        _alivePlayers.Add(obj);
    }
    public void RemovePlayer(NetworkObject obj)
    {
        _alivePlayers.Remove(obj);
        Debug.Log($"Game Lost: {obj}");
        CheckWinCondition();
    }
    public void GameWon()
    {
        Debug.Log($"Game Won: {_alivePlayers[0]}");
        uiManager.VictoryScreen();
    }

    void CheckWinCondition()
    {
        if(gameEnded) return;

        if(_alivePlayers.Count == 1)
        {
            gameEnded = true;
            GameWon();
        }
        else if(_alivePlayers.Count == 0)
        {
            gameEnded = true;
            Debug.Log("No winners");
        }
    }
}
