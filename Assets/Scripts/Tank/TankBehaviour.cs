using Fusion;
using UnityEngine;

public class TankBehaviour : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnHealthChanged))]
    public int HP {  get; set; }
    UIManager uiManager;
    public int Points = 0;
    int pointsToGetHealth = 30;
    int maxHealth = 100;
    [SerializeField] HealthBar healthBar;
    
    public override void Spawned()
    {
        uiManager = UIManager.instance;
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

        //healthBar.SetHealth(currentHealth);
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
            //healthBar.SetHealth(currentHealth);
        }
    }

    private void OnHealthChanged()
    {
        healthBar.SetHealth(HP);
        Debug.Log("HealthChanged");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10);
        }
    }

}
