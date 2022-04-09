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
    private EnemyHealth aimTarget;
    private Vector3 rotation;
    private Vector3 projectileOrigin;
    CapsuleCollider detectionCollider;
    float nextAttack;

    private void Awake()
    {
        targets = new List<EnemyHealth>();
        detectionCollider = GetComponent<CapsuleCollider>();
        Setup(towerInfo);
    }
    private void Update()
    {
        LookAtTarget();
    }
    void Setup(TowerInfo info)
    {
        TowerModelInfo towerMesh = Instantiate(info.TowerMesh, MeshParent).GetComponent<TowerModelInfo>();
        projectileOrigin = towerMesh.ProjectileOrigin.position;
        detectionCollider.radius = info.Range / 2;
        detectionCollider.height = info.Range * 2;
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
        float prioValue = Mathf.Infinity; ;
        EnemyHealth newTarget = null;
        switch (towerInfo.TargetPriority)
        {
            case TargetPriority.Nearest:
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
            case TargetPriority.Farest://<-
                break;
            case TargetPriority.LowestHp://<-
                break;
            case TargetPriority.HighestHp://<-
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
        //return linecast doesnt work...
    }

    void LookAtTarget() //moet rotation speed gebruiken
    {
        RemoveEmptyObjects();
        aimTarget = FilterTarget();
        if(aimTarget == null)
        {
            return;
        }
        rotation = aimTarget.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(rotation);

        if (Time.time > nextAttack)
        {
            nextAttack = (1 / towerInfo.AttackSpeed) + Time.time;
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab); //projectile origin gebruiken
        newBullet.GetComponent<ProjectileBehaviour>().Setup(gameObject);
        newBullet.transform.rotation = Quaternion.LookRotation(rotation);
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * towerInfo.ProjectileSpeed;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * towerInfo.Range);
    }
}
