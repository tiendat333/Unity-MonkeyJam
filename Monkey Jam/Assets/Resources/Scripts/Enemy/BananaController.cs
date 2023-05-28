using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BananaController : MonoBehaviour
{
    private NavMeshAgent agent = null;
    private Animator anim = null;   
    private BananaStats stats = null;
    private float timeOfLastAttack = 0;
    private bool hasStopped = false;

    [SerializeField] private Transform target;
    [SerializeField] private float stoppingDistance = 3;

    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        agent.SetDestination(target.position);
        anim.SetFloat("Speed", 1f, 0.3f, Time.deltaTime);
        RotateToTarget();

        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget <= agent.stoppingDistance)
        {
            anim.SetFloat("Speed", 0f); //Stop moving
            
            //Attack
            if (!hasStopped)
            {
                hasStopped = true;
                timeOfLastAttack = Time.time;  //wait for attackSpeed time before attack when come close
            }

            if (Time.time >= timeOfLastAttack + stats.attackSpeed)
            {
                timeOfLastAttack = Time.time; //wait for attackSpeed time until attack again
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                AttackTarget(targetStats);
            }
            
        }
        else
        {
            if (hasStopped)
            {
                hasStopped = false;
            }
        }
    }

    private void RotateToTarget()
    {
        //transform.LookAt(target);

        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }

    private void AttackTarget(CharacterStats statsToDamage)
    {
        anim.SetTrigger("Attack");
        stats.DealDamage(statsToDamage);
    }

    private void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<BananaStats>();
    }
}
