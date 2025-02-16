using System;
using System.Xml.Serialization;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private GameObject shieldGameObject;

    PlayerAnimator playerAnimator;
    PlayerMovement playerMovement;
    PlayerCombat playerCombat;
    PlayerStatRegeneration playerStatRegeneration;

    Rigidbody2D rb2D;

    private void Awake()
    {
        CacheComponents();
        SetupConstructors();
        InitializeStats();
    }

    private void Update() 
    {
        playerCombat.CountCooldowns();
        playerStatRegeneration.RegenerateStats();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        playerMovement.OnCollisionEnter2D(other);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Potion"))
        {
            Potion potion = other.GetComponent<Potion>();
            potion.Use(playerStats);
        }
    }

    private void CacheComponents()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void SetupConstructors()
    {
        playerMovement = new PlayerMovement(rb2D,transform,playerStats);
        playerCombat = new PlayerCombat(playerStats,transform,shieldGameObject);
        playerStatRegeneration = new PlayerStatRegeneration(playerStats);
        playerAnimator = new PlayerAnimator(_playerAnimator);
    }

    private void InitializeStats()
    {
        playerStats.CurrentHealth = playerStats.MaxHealth;
        playerStats.CurrentMana = playerStats.MaxMana;
    }

    public void TakeDamage(float damage, DamageType damageType)
    {
        playerCombat.TakeDamage(damage,damageType);
    }

    public PlayerStats GetPlayerStats()
    {
        return playerStats;
    }

    private void OnDestroy() 
    {
        playerMovement.UnregisterEvents();
        playerAnimator.UnregisterEvents();
        playerCombat.UnregisterEvents();
    }
}
