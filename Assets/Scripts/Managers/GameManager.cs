using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;
    UIManager uiManager;

    public string playerName;

    public List<PlayerName> AllPlayers = new List<PlayerName>();
    public List<string> players = new List<string>();
    public List<string> eliminatedPlayer = new List<string>();

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
        LoadPlayerData();
    }

    private void Start()
    {
        uiManager = UIManager.instance;
    }

    private void Update()
    {
        PlayerName[] player = GameObject.FindObjectsOfType<PlayerName>();
        
        for (int i = 0; i < player.Length; i++)
        {
            RegisterPlayer(player[i]);
        }
        if (AllPlayers.Count == maxPlayers)
        {
            gameStarted = true;
        }
        for (int i = 0; i < AllPlayers.Count; i++)
        {
            if (AllPlayers[i] == null)
            {
                eliminatedPlayer.Add(players[i]);
                AllPlayers.RemoveAt(i);
            }
        }
        CheckWinCondition();
    }
    public void ReadName(Text name)
    {
        playerName = name.text;
        SavePlayerData();
        uiManager.ShowPlayerName();
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void GameWon()
    {
        uiManager.DisplayLeaderboard();
        uiManager.VictoryScreen();
    }

    public void RegisterPlayer(PlayerName player)
    {
        if (!AllPlayers.Contains(player))
        {
            string name = player.playerName.ToString();
            if(!string.IsNullOrEmpty(name) && !players.Contains(name))
            {
                AllPlayers.Add(player);
                players.Add(name);
            }
        }
    }

    void CheckWinCondition()
    {
        if(gameEnded) return;

        if (AllPlayers.Count == 1 && gameStarted)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] == AllPlayers[0].playerName.ToString())
                {
                    eliminatedPlayer.Add(players[i]);
                }
            }
            gameEnded = true;
            GameWon();
        }
    }


    #region Json Data
    [System.Serializable]
    class SaveData
    {
        public int level;
        public bool music;
        public float volume;
        public string playerName;
    }

    public void SavePlayerData()
    {
        SaveData data = new SaveData();
        //data.music = SoundManager.Instance.music;
        //data.volume = SoundManager.Instance.volume;
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath + "/saveFile.json", json);
    }

    public void LoadPlayerData()
    {
        string path = Application.dataPath + "/saveFile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            //SoundManager.Instance.music = data.music;
            //SoundManager.Instance.volume = data.volume;
            playerName = data.playerName;
        }
    }
    #endregion
}
