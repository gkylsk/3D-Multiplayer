using UnityEngine;
using UnityEngine.UI;

public class ShowName : MonoBehaviour
{
    public PlayerName playerName;

    void Update()
    {
        GetComponent<Text>().text = playerName.playerName.ToString();
    }
}
