using Fusion;
using UnityEngine;

public class TankBehaviour : NetworkBehaviour
{
    #region Managers
    UIManager uiManager;
    GameManager gameManager;
    #endregion

    #region Variables
    [Networked, OnChangedRender(nameof(OnHealthChanged))]
    public int HP { get; set; }
    public int Points = 0;
    int pointsToGetHealth = 30;
    int maxHealth = 100;
    [SerializeField] HealthBar healthBar;
    #endregion

    public override void Spawned()
    {
        uiManager = UIManager.instance;
        gameManager = GameManager.Instance;

        HP = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    private void OnEnable()
    {
        Coin.OnCoinCollect += AddScore;
        HealthItem.OnHealthCollect += AddHealth;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollect -= AddScore;
        HealthItem.OnHealthCollect -= AddHealth;
    }

    void TakeDamage(int damage)
    {
        HP -= damage;
    }

    public void AddScore(int score)
    {
        Points += score;
        uiManager.UpdateCoinText(Points);
    }

    void AddHealth(int health)
    {
        if(HP < maxHealth && Points >= pointsToGetHealth)
        {
            Points -= pointsToGetHealth;
            uiManager.UpdateCoinText(Points);
            HP += health;
        }
    }

    private void OnHealthChanged()
    {
        healthBar.SetHealth(HP);
        if (HP == 0)
        {
            gameManager.RemovePlayer(Object);
            Runner.Despawn(Object);
            if(HasInputAuthority)
            {
                uiManager.LoseScreen();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10);
        }
    }

}
