using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Life
{
    private NavMeshAgent agent;
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
    }
    public void ChaseCharacter()
    {
        agent.SetDestination(Character.main.transform.position);
        Move(agent.steeringTarget - transform.position);
    }
}
