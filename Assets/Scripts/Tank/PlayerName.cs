using Fusion;

public class PlayerName : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(SetName))]
    public NetworkString<_32> playerName { get; set; }

    void Start()
    {
        playerName = GameManager.Instance.GetPlayerName();
    }

    void Update()
    {
        if (Object.HasInputAuthority)
        {
            playerName = GameManager.Instance.GetPlayerName();
        }
    }

    public void SetName()
    {
        gameObject.name = playerName.ToString();
    }
}
