using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyHealth : BaseHealth
{
    [SerializeField] private EnemyInfo enemyInfo;
    public EnemyInfo EnemyInfo => enemyInfo;


    protected override void Setup()
    {
        base.Setup();
        maxHealth = enemyInfo.HealthStats.maxHealth;
        currentHealth = maxHealth;
        armor = enemyInfo.HealthStats.armor;
        fireRes = enemyInfo.HealthStats.fireResistance;
        iceRes = enemyInfo.HealthStats.iceResistance;
        electroRes = enemyInfo.HealthStats.electroResistance;
        elementalShield = enemyInfo.HealthStats.elementalShield;
        //hier andere gekke dingen doen!
    }
}
