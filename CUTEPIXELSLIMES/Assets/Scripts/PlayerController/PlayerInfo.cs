using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "playerInfo", menuName = "info/player", order = 1)]
public class PlayerInfo : ScriptableObject
{
    [SerializeField] private float baseMovementSpeed;
    [SerializeField] private float baseDamage;
    [SerializeField] private float baseAttackSpeed;
    [SerializeField] private float baseStartingMoney;

    public float BaseMovementSpeed => baseMovementSpeed;
    public float BaseDamage => baseDamage;
    public float BaseAttackSpeed => baseAttackSpeed;
    public float BaseStartingMoney => baseStartingMoney;
}
