using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] Text coinsText;

    //lobby
    public Text _roomCode;
    public InputField _inputCode;

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

    public void UpdateCoinText(int points)
    {
        coinsText.text = "Coins:" + points.ToString();
    }

    public void Host()
    {
        string roomCode = GenerateRoomCode();
        _roomCode.text = roomCode;
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
}
