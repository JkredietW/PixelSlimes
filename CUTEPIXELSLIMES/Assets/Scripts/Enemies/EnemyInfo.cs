using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "enemyInfo", menuName = "info/enemy", order = 1)]
public class EnemyInfo : ScriptableObject
{
    [SerializeField] private HealthStats healthStats;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float range;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private EnemyType enemyType;

    public HealthStats HealthStats => healthStats;
    public float Damage => damage;
    public float MovementSpeed => movementSpeed;
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