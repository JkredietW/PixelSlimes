using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    protected float maxHealth;
    protected float currentHealth;
    protected DamageType damageType;
    protected float armor, fireRes, iceRes, electroRes;
    protected ElementalShield elementalShield;
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
    public void DoDamage(float damageAmount, DamageType _type)
    {
        if (!isDead)
        {
            if(elementalShield.shieldValue > 0 && _type != DamageType.Physical)
            {
                if(elementalShield.damageType == _type)
                {
                    return;
                }
                else if(elementalShield.damageType == DamageType.Fire && _type == DamageType.Electro)
                {
                    damageAmount *= 2;
                }
                else if (elementalShield.damageType == DamageType.Cold && _type == DamageType.Fire)
                {
                    damageAmount *= 2;
                }
                else if (elementalShield.damageType == DamageType.Electro && _type == DamageType.Cold)
                {
                    damageAmount *= 2;
                }

                elementalShield.shieldValue = Mathf.Clamp(elementalShield.shieldValue - damageAmount, 0, Mathf.Infinity);
                return;
            }
            switch(_type)
            {
                case DamageType.Dark:
                    if(damageType == _type)
                    {
                        return;
                    }
                    break;
                case DamageType.Physical:
                    damageAmount = Mathf.Clamp(damageAmount - armor, 1, Mathf.Infinity);
                    break;
                case DamageType.Fire:
                    damageAmount = Mathf.Clamp(damageAmount * (fireRes / 100), 1, Mathf.Infinity);
                    break;
                case DamageType.Cold:
                    damageAmount = Mathf.Clamp(damageAmount * (iceRes / 100), 1, Mathf.Infinity);
                    break;
                case DamageType.Electro:
                    damageAmount = Mathf.Clamp(damageAmount * (electroRes / 100), 1, Mathf.Infinity);
                    break;
            }

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
[System.Serializable]
public class HealthStats
{
    public float maxHealth;
    public float healthRegen;
    public float armor;
    public float fireResistance, iceResistance, electroResistance;
    public ElementalShield elementalShield;
    //shields will be done later
}
[System.Serializable]
public class ElementalShield
{
    public DamageType damageType;
    public float shieldValue;
}
[System.Serializable]
public enum DamageType
{
    Physical,
    Cold,
    Fire,
    Electro,
    Dark,
}