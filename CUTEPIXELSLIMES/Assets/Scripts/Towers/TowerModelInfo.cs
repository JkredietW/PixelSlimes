using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerModelInfo : MonoBehaviour
{
    [SerializeField] private Transform projectileOrigin;
    [SerializeField] private Transform towerHead;

    public Transform ProjectileOrigin => projectileOrigin;
    public Transform TowerHead => towerHead;
}
