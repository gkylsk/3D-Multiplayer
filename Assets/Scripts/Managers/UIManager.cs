using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WebSocketSharp;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    GameManager gameManager;

    [SerializeField] Text coinsText;

    [Header("Main Menu")]
    [SerializeField] GameObject _mainmenu;
    [SerializeField] Text _playerName;
    [SerializeField] GameObject _setPlayerName;
    [SerializeField] GameObject _settings;

    [Header("Screens")]
    [SerializeField] GameObject _mainMenuBG;
    [SerializeField] GameObject _victoryScreen;
    [SerializeField] GameObject _loseScreen;
    [SerializeField] GameObject _leaderBoard;

    [Header("Player Rank")]
    [SerializeField] private Transform rankContainer;
    [SerializeField]private Transform rankTemplate;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        gameManager = GameManager.Instance;

        gameManager.LoadPlayerData();
        SetPlayerName();
    }

    void SetPlayerName()
    {
        if(gameManager.playerName.IsNullOrEmpty())
        {
            _setPlayerName.SetActive(true);
        }
        else
        {
            _setPlayerName.SetActive(false);
        }
        ShowPlayerName();
    }
    public void ShowPlayerName()
    {
        _playerName.text = gameManager.playerName;
    }

    public void ToggleSettings()
    {
        if(_settings.activeSelf)
        {
            _settings.SetActive(false);
        }
        else
        {
            _settings.SetActive(true);
        }
    }

    public void UpdateCoinText(int points)
    {
        coinsText.text = "Coins:" + points.ToString();
    }

    public void DisplayLeaderboard()
    {
        _leaderBoard.SetActive(true);

        float templateHeight = 50f;
        int j = 0;
        rankTemplate.gameObject.SetActive(false);
        for (int i = gameManager.eliminatedPlayer.Count - 1; i >= 0; i--)
        {
            Transform rankTransform = Instantiate(rankTemplate, rankContainer);
            RectTransform rankRectTransform = rankTransform.GetComponent<RectTransform>();

            rankRectTransform.anchoredPosition = new Vector2(0, -templateHeight * j);
            rankTransform.gameObject.SetActive(true);

            rankTransform.Find("Rank").GetComponent<Text>().text = (j + 1).ToString();
            rankTransform.Find("PlayerName").GetComponent<Text>().text = gameManager.eliminatedPlayer[i];

            j++;
        }
    }
   
    public void SceneLoaded()
    {
        _mainMenuBG.SetActive(false);
        _mainmenu.SetActive(true);
    }
}
