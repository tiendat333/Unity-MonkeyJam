using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaStats : CharacterStats
{
    [SerializeField] private int damage;
    public float attackSpeed;

    [SerializeField] private bool canAttack;

    private void Start()
    {
        InitVariables();
    }

    public void DealDamage(CharacterStats statsToDamage)
    {
        statsToDamage.TakeDamage(damage);
    }

    public override void Die()
    {
        base.Die();
        PlayerHUD.instance.UpdateScoreAmount();
        Destroy(gameObject);
    }

    public override void InitVariables()
    {
        maxHealth = 25;
        SetHealthTo(maxHealth);
        isDead = false;

        damage = 10;
        attackSpeed = 1.5f;
        canAttack = true;
    }

}
