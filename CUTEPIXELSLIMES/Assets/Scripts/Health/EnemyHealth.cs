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
        maxHealth = enemyInfo.MaxHealth;
        currentHealth = maxHealth;
        //hier andere gekke dingen doen!
    }
}
