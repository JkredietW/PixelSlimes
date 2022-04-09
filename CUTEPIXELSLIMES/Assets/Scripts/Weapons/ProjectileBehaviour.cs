using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControllerNameSpace;

public class ProjectileBehaviour : MonoBehaviour
{
    private GameObject sender;
    private PlayerInfo statSource;
    public GameObject Sender => sender;

    int pierces;

    public void Setup(GameObject _sender)
    {
        sender = _sender;
        statSource = FindObjectOfType<PlayerController>().BaseStats;
        pierces = 0;// hier data ophalen
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == sender.GetComponent<Collider>())
        {
            return;
        }
        print(other);
        if(other.GetComponent<EnemyHealth>())
        {
            EnemyHealth health = other.GetComponent<EnemyHealth>();
            health.DoDamage(statSource.BaseDamage);

            Destroy(gameObject);
        }
    }
}
