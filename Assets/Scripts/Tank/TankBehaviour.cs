using Fusion;
using UnityEngine;

public class TankBehaviour : NetworkBehaviour
{
    #region Managers
    UIManager uiManager;
    #endregion

    #region Variables
    [Networked, OnChangedRender(nameof(OnHealthChanged))]
    public int HP { get; set; }
    int damage = 10;

    public int Points = 0;
    int pointsToGetHealth = 30;
    int maxHealth = 100;
    [SerializeField] HealthBar healthBar;
    #endregion

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

    public void AddScore(int score)
    {
        Points += score;
        uiManager.UpdateCoinText(Points);
        SoundManager.Play("Coin");
    }

    void AddHealth(int health)
    {
        //increase hp if the points required is acquired and hp is less than max
        if (HP < maxHealth && Points >= pointsToGetHealth)
        {
            Points -= pointsToGetHealth;
            uiManager.UpdateCoinText(Points);
            HP += health;
            SoundManager.Play("HealthItem");
        }
    }

    private void OnHealthChanged()
    {
        healthBar.SetHealth(HP);

        //if hp reaches 0 player loses
        if (HP == 0)
        {
            Runner.Despawn(Object);
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealDamageRpc(int damage)
    {
        // The code inside here will run on the client which owns this object (has state and input authority).
        HP -= damage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            //reduce hp if bullet hits
            DealDamageRpc(damage);
        }
    }
}
