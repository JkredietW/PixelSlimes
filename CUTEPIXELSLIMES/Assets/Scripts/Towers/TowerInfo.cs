using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "towerInfo", menuName = "info/tower", order = 1)]
public class TowerInfo : ScriptableObject
{
    [SerializeField] protected float damage;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float projectileSpeed = 10;
    [SerializeField] protected float range;
    [SerializeField] protected UseExplosiveProjectiles explosive;
    [SerializeField] protected float manaCost;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected DamageType damageType;
    [SerializeField] protected TargetPriority targetPriority;
    [SerializeField] protected GameObject towerMesh;

    public float Damage => damage;
    public float AttackSpeed => attackSpeed;
    public float ProjectileSpeed => projectileSpeed;
    public float Range => range;
    public UseExplosiveProjectiles Explosive => explosive;
    public float ManaCost => manaCost;
    public float RotationSpeed => rotationSpeed;
    public DamageType DamageType => damageType;
    public TargetPriority TargetPriority => targetPriority;
    public GameObject TowerMesh => towerMesh;
}
[Serializable]
public enum DamageType
{
    Physical,
    Cold,
    Fire,
    Electro,
    Dark,
}
[Serializable]
public enum TargetPriority
{
    Nearest,
    Farest,
    LowestHp,
    HighestHp,
}
[Serializable]
public class UseExplosiveProjectiles
{
    public Explosion useExplosions;
    public float explosionRadius;
}
[Serializable]
public enum Explosion
{
    No,
    Yes,
}

