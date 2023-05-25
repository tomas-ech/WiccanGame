using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform player;
    private Animator enemyAnimator;

    public NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;
    private PlayerStats playerStats;

    [Header("Patroling")]
    public Vector3 walkpoint;
    private bool walkPointSet;
    public float walkPointRange;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public GameObject spell1;

    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); 
        playerStats = GetComponent<PlayerStats>();
        enemyAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (GameObject.FindWithTag("Player"))
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        //Verificar si esta en rango de visión o rango de ataque
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(playerStats.characterCurrentHealth > 1)
        {
            if (!playerInSightRange && !playerInAttackRange) 
            {
                Patroling();               
            }
            if (playerInSightRange && !playerInAttackRange) {ChasePlayer();}
            if (playerInSightRange && playerInAttackRange) {AttackPlayer();}
        }

        

    }

    /*private void Patroling()
    {
        if (!walkPointSet) {SearchWalkPoint();}

        if (walkPointSet == true) {agent.SetDestination(walkpoint);}

        Vector3 distanceToWalkPoint = transform.position - walkpoint;

        //Al llegar al walkpoint
        if (distanceToWalkPoint.magnitude < 1) {walkPointSet = false;}
    }

    private void SearchWalkPoint()
    {
        //Calcula un punto aleatorio dentro del rango
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkpoint, -transform.up, 5f, whatIsGround))
        {
            walkPointSet = true;
        }
    }*/

    private void Patroling()
    {
        if(agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(this.transform.position + transform.up, walkPointRange, out point)) //pass in our centre point and radius of area
            {
                Debug.Log("Caminando" + point);
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }

            enemyAnimator.SetBool("IsRunning", true); 
        }
        else {enemyAnimator.SetBool("IsRunning", false);}
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 5.0f, NavMesh.AllAreas)) //
        { 
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        enemyAnimator.SetBool("IsRunning", true);
    }

    private void AttackPlayer()
    {
        //Que no se muevan al atacar
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            enemyAnimator.SetBool("Attack1", true);
            Instantiate(spell1, transform.position + transform.forward*5 + transform.up*2, this.transform.localRotation);
            alreadyAttacked = true;
            Invoke("ResetAttack", timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
