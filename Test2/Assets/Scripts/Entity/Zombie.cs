using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Life
{
    private NavMeshAgent agent;
    public float attackRange=1.1f;
    private void Start()
    {
        base.Init();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        UpdateZombie();
    }
    public void UpdateZombie()
    {
        if (Character.main != null)
        {
            ChaseCharacter();
        }
        base.LifeUpdate();
    }
    public override void OnDeath()
    {
        base.OnDeath();
        if(!immortal)
            agent.enabled = false;
    }
    public void ChaseCharacter()
    {
        if(Vector3.Distance(transform.position, Character.main.transform.position) > attackRange)
        {
            agent.SetDestination(Character.main.transform.position);
            Shoot(Character.main.transform.position - transform.position, false);
            Move(agent.steeringTarget - transform.position);
        }
        else
        {
            Move(Vector3.zero);
            Shoot(Character.main.transform.position-transform.position, true);
        }
    }
}
