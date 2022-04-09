using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    protected float maxHealth;
    protected float currentHealth;
    bool isDead;

    #region publics fields
    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;
    public bool IsDead => isDead;
    #endregion 

    private void Awake()
    {
        Setup();
    }
    protected virtual void Setup()
    {
        //for child objects
    }
    public void DoDamage(float damageAmount)
    {
        if (!isDead)
        {
            currentHealth = Mathf.Clamp(currentHealth - damageAmount, 0, maxHealth);
            if(currentHealth == 0)
            {
                isDead = true;
                IsKilled();
            }
        }
    }
    protected void IsKilled()
    {
        //hier dood dingen doen
    }


    public void ForceKill()
    {
        IsKilled();
    }
}
