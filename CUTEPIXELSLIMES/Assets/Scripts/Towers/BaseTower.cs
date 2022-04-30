using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    //monobahaviour moet vervangen worden met enemy health script
    private List<EnemyHealth> targets;
    [SerializeField] private TowerInfo towerInfo;
    [SerializeField] private Transform MeshParent;
    [SerializeField] private Transform meshHead;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private List<TowerItem> heldItems;
    private EnemyHealth aimTarget;
    private Vector3 rotation;
    private Vector3 projectileOrigin;
    CapsuleCollider detectionCollider;
    float nextAttack;
    private float currentDamage;
    private float attackSpeed;
    private DamageType currentDamageType;

    public TowerInfo TowerInfo => towerInfo;

    public float CurrentDamage => currentDamage;
    public float AttackSpeed => attackSpeed;
    public int HeldItemsCount => heldItems.Count;
    public DamageType CurrentDamageType => currentDamageType;

    private void Awake()
    {
        targets = new List<EnemyHealth>();
        detectionCollider = GetComponent<CapsuleCollider>();
        GrandItem(ItemGenerator.instance.GenerateItem(3));
        Setup();
    }
    private void Update()
    {
        LookAtTarget();
    }
    public void GrandItem(TowerItem newItem)
    {
        heldItems.Add(newItem);
        CalculateDamage();
    }
    public void RemoveHeldItem(int indexNumber)
    {
        heldItems.RemoveAt(indexNumber);
        heldItems.RemoveAll(item => item == null);
        CalculateDamage();
    }
    void Setup()
    {
        TowerModelInfo towerMesh = Instantiate(towerInfo.TowerMesh, MeshParent).GetComponent<TowerModelInfo>();
        projectileOrigin = towerMesh.ProjectileOrigin.position;
        detectionCollider.radius = towerInfo.Range / 2;
        detectionCollider.height = towerInfo.Range * 2;

        CalculateDamage();
        print(attackSpeed);
        print(currentDamage);
    }
    public void CalculateDamage()
    {
        currentDamage = towerInfo.Damage;
        attackSpeed = towerInfo.AttackSpeed;
        //flat
        for (int i = 0; i < heldItems.Count; i++)
        {
            foreach (ItemStat stat in heldItems[i].itemStats)
            {
                switch(stat.statType)
                {
                    case ItemStats.DamageFlat:
                        currentDamage += stat.amount;
                        break;
                    case ItemStats.AttackSpeedFlat:
                        attackSpeed += stat.amount;
                        break;
                }
            }
        }
        //%
        for (int i = 0; i < heldItems.Count; i++)
        {
            foreach (ItemStat stat in heldItems[i].itemStats)
            {
                switch (stat.statType)
                {
                    case ItemStats.DamagePercentage: //deze moet later
                        currentDamage *=  1 + stat.amount;
                        break;
                    case ItemStats.AttackSpeedPercentage:
                        attackSpeed *= 1 + stat.amount;
                        break;
                }
            }
        }
    }
    public void GetTarget()
    {
        RemoveEmptyObjects();

    }
    void RemoveEmptyObjects()
    {
        targets.RemoveAll(item => item == null);
        targets.RemoveAll(item => item.IsDead);
    }


    protected void OnTriggerEnter(Collider _target)
    {
        EnemyHealth newTarget = _target.GetComponent<EnemyHealth>();

        if(!CheckIfTargetIsAvailable(newTarget))
        {
            targets.Add(newTarget);
        }
        else
        {
            return;
        }
    }
    protected void OnTriggerExit(Collider _target)
    {
        EnemyHealth newTarget = _target.GetComponent<EnemyHealth>();

        if (CheckIfTargetIsAvailable(newTarget))
        {
            targets.Remove(newTarget);
        }
        else
        {
            return;
        }
    }
    bool CheckIfTargetIsAvailable(EnemyHealth _target)
    {
        return targets.Contains(_target);
    }
    EnemyHealth FilterTarget()
    {
        EnemyHealth newTarget = null;
        switch (towerInfo.TargetPriority)
        {
            case TargetPriority.Nearest:
                float prioValue = Mathf.Infinity;
                for (int i = 0; i < targets.Count; i++)
                {
                    if (!WallCheck(targets[i].transform))
                    {
                        float dist = Vector3.Distance(targets[i].transform.position, transform.position);
                        if (dist < prioValue)
                        {
                            prioValue = dist;
                            newTarget = targets[i];
                        }
                    }
                    else if(targets.Count <= 1)
                    {
                        return null;
                    }
                }
                break;
            case TargetPriority.Farest:
                prioValue = 0;
                for (int i = 0; i < targets.Count; i++)
                {
                    if (!WallCheck(targets[i].transform))
                    {
                        float dist = Vector3.Distance(targets[i].transform.position, transform.position);
                        if (dist > prioValue)
                        {
                            prioValue = dist;
                            newTarget = targets[i];
                        }
                    }
                    else if (targets.Count <= 1)
                    {
                        return null;
                    }
                }
                break;
            case TargetPriority.LowestCurrentHp:
                prioValue = Mathf.Infinity;
                for (int i = 0; i < targets.Count; i++)
                {
                    if (!WallCheck(targets[i].transform))
                    {
                        float health = targets[i].CurrentHealth;
                        if (health < prioValue)
                        {
                            prioValue = health;
                            newTarget = targets[i];
                        }
                    }
                    else if (targets.Count <= 1)
                    {
                        return null;
                    }
                }
                break;
            case TargetPriority.HighestCurrentHp:
                prioValue = 0;
                for (int i = 0; i < targets.Count; i++)
                {
                    if (!WallCheck(targets[i].transform))
                    {
                        float health = targets[i].CurrentHealth;
                        if (health > prioValue)
                        {
                            prioValue = health;
                            newTarget = targets[i];
                        }
                    }
                    else if (targets.Count <= 1)
                    {
                        return null;
                    }
                }
                break;
            case TargetPriority.LowestMaxHp:
                prioValue = Mathf.Infinity;
                for (int i = 0; i < targets.Count; i++)
                {
                    if (!WallCheck(targets[i].transform))
                    {
                        float health = targets[i].MaxHealth;
                        if (health < prioValue)
                        {
                            prioValue = health;
                            newTarget = targets[i];
                        }
                    }
                    else if (targets.Count <= 1)
                    {
                        return null;
                    }
                }
                break;
            case TargetPriority.HighestMaxHp:
                prioValue = 0;
                for (int i = 0; i < targets.Count; i++)
                {
                    if (!WallCheck(targets[i].transform))
                    {
                        float health = targets[i].MaxHealth;
                        if (health > prioValue)
                        {
                            prioValue = health;
                            newTarget = targets[i];
                        }
                    }
                    else if (targets.Count <= 1)
                    {
                        return null;
                    }
                }
                break;
        }
        return newTarget;
    }
    bool WallCheck(Transform _target)
    {
        if(Physics.Linecast(transform.position, _target.transform.position, wallMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void LookAtTarget()
    {
        if(targets.Count < 1)
        {
            return;
        }
        RemoveEmptyObjects();
        aimTarget = FilterTarget();
        if(aimTarget == null)
        {
            return;
        }
        rotation = aimTarget.transform.position + aimTarget.transform.forward * (aimTarget.EnemyInfo.MovementSpeed / towerInfo.ProjectileSpeed) - transform.position; //werkt meschien?
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(rotation), towerInfo.RotationSpeed * Time.deltaTime);

        if (Time.time > nextAttack)
        {
            nextAttack = (1 / attackSpeed) + Time.time;
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, projectileOrigin, Quaternion.LookRotation(transform.forward));
        newBullet.GetComponent<ProjectileBehaviour>().Setup(gameObject);
        //newBullet.transform.rotation = Quaternion.LookRotation(transform.forward);
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * towerInfo.ProjectileSpeed;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * towerInfo.Range);
    }
}
