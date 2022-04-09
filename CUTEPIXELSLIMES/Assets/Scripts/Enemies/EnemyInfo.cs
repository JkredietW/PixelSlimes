using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "enemyInfo", menuName = "info/enemy", order = 1)]
public class EnemyInfo : ScriptableObject
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float healthRegen;
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float range;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private EnemyType enemyType;

    public float Damage => damage;
    public float MaxHealth => maxHealth;
    public float HealthRegen => healthRegen;
    public float AttackSpeed => attackSpeed;
    public float Range => range;
    public float RotationSpeed => rotationSpeed;
    public EnemyType EnemyType => enemyType;
}
[Serializable]
public enum EnemyType
{
    none, // placeholders
    type1,
    type2,
}