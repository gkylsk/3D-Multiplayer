using Fusion;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;
    UIManager uiManager;
    SoundManager soundManager;

    public string playerName;

    public List<PlayerName> AllPlayers = new List<PlayerName>();
    public List<string> players = new List<string>();
    public List<string> eliminatedPlayer = new List<string>();

    public int minPlayers = 2;
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
        soundManager = SoundManager.Instance;

        LoadPlayerData();
    }

    private void Update()
    {
        //Register the available players
        PlayerName[] player = GameObject.FindObjectsOfType<PlayerName>();
        
        for (int i = 0; i < player.Length; i++)
        {
            RegisterPlayer(player[i]);
        }

        if (AllPlayers.Count == minPlayers)
        {
            gameStarted = true;
        }

        //if any player exits remove the player
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

    public void GameOver()
    {
        SoundManager.Play("GameEnd");
        uiManager.DisplayLeaderboard();
    }

    public void RegisterPlayer(PlayerName player)
    {
        if (!AllPlayers.Contains(player))
        {
            string name = player.playerName.ToString();
            //check if the player name is not null and if player already exist
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

        //if last remaining player then game is over
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
            GameOver();
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
        data.music = soundManager.music;
        data.volume = soundManager.volume;
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
            soundManager.music = data.music;
            soundManager.volume = data.volume;
            playerName = data.playerName;
        }
    }
    #endregion
}
