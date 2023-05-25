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
    private Vector3 initialPosition;
    public Vector3 walkpoint;
    private bool walkPointSet;
    public float walkPointRange = 2f;

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
        initialPosition = transform.position;
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

        if(playerStats.characterCurrentHealth > 1 && player.GetComponent<PlayerStats>().characterCurrentHealth > 1)
        {
            if (!playerInSightRange && !playerInAttackRange) 
            {
                Patroling();               
            }
            if (playerInSightRange && !playerInAttackRange) {ChasePlayer();}
            if (playerInSightRange && playerInAttackRange) {AttackPlayer();}
        }
        
        else if (playerStats.characterCurrentHealth > 1 && player.GetComponent<PlayerStats>().characterCurrentHealth < 1)
        {
            agent.SetDestination(initialPosition);
        }

    }

    private void Patroling()
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

        if (Physics.Raycast(walkpoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
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
        enemyAnimator.SetBool("Attack1", false);
    }
}
