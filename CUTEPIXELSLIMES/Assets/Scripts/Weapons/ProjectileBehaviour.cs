using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControllerNameSpace;

public class ProjectileBehaviour : MonoBehaviour
{
    private GameObject sender;
    private BaseTower statSource;
    public GameObject Sender => sender;

    int pierces; // not used yet

    public void Setup(GameObject _sender)
    {
        sender = _sender;
        statSource = _sender.GetComponent<BaseTower>();
        pierces = 0;// hier data ophalen
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == sender.GetComponent<Collider>())
        {
            return;
        }
        if(other.GetComponent<EnemyHealth>())
        {
            EnemyHealth health = other.GetComponent<EnemyHealth>();
            health.DoDamage(statSource.CurrentDamage, statSource.CurrentDamageType);

            Destroy(gameObject);
        }
    }
}
