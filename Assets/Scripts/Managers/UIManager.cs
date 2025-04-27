using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] Text coinsText;

    [Header("Lobby")]
    public Text _roomCode;
    public InputField _inputCode;

    [Header("Screens")]
    [SerializeField] GameObject _mainmenu;
    [SerializeField] GameObject _loadingScreen;
    [SerializeField] GameObject _victoryScreen;
    [SerializeField] GameObject _loseScreen;
    [SerializeField] GameObject _leaderBoard;
    [SerializeField] GameObject _roomFilled;

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
        _inputCode.onValueChanged.AddListener(OnRoomCodeEntered);
    }
    public void UpdateCoinText(int points)
    {
        coinsText.text = "Coins:" + points.ToString();
    }

    public void Host()
    {
        string roomCode = GenerateRoomCode();
        _roomCode.text = roomCode;
    }

    void OnRoomCodeEntered(string text)
    {
        _inputCode.text = text.ToUpper();
    }
    public static string GenerateRoomCode()
    {
        char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
        string code = "";
        for (int i = 0; i < 6; i++)
        {
            code += letters[Random.Range(0, letters.Length)];
        }

        return code;
    }

    public void VictoryScreen()
    {
        _victoryScreen.SetActive(true);
    }

    public void LoseScreen()
    {
        _loseScreen.SetActive(true);
    }

    public void LeaderBoard()
    {
        _leaderBoard.SetActive(true);
    }
    public void LoadingScreen()
    {
        _mainmenu.SetActive(false);
        _loadingScreen.SetActive(true);
    }

    public void RoomFilled()
    {
        _roomFilled.SetActive(true);
    }
    public void SceneLoaded()
    {
        _loadingScreen.SetActive(false);
        _mainmenu.SetActive(true);
    }
}
