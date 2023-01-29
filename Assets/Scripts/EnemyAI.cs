using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.UI;


public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //Increase Score
    private Level01Controller incScore;

    //Health pack 
    public GameObject healthPackPrefab;

    //Enemy counter
    public int totalEnemy;
    public int enemyKillCounter;

    //Win state
    //private Level01Controller winState;

    //Enemy explosion and sound
    public ParticleSystem enemyExplosion;
    public AudioSource enemyDeathSFX;

    private void Awake()
    {
        player = GameObject.Find("First Person Player (Orientation)").transform;
        agent = GetComponent<NavMeshAgent>();
        incScore = FindObjectOfType<Level01Controller>();
        //winState = FindObjectOfType<Level01Controller>();
        //damagePlayer = FindObjectOfType<PlayerHealth>();
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
        

    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2.5f, whatIsGround))
        {
            walkPointSet = true;
        }

    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);

    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            //Attack code here
            if (gameObject.tag == "EnemyBlue")
            {
                Instantiate(projectile, transform.position, transform.rotation);
            }
            if (gameObject.tag == "EnemyRed")
            {
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 20f, ForceMode.Impulse);
            }
            //damagePlayer.DamagePlayer(10);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

     
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            incScore.IncreaseScore(10);
            incScore.PlayEnemyDeathSFX();
            Invoke(nameof(DestroyEnemy), 0.01f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
        Instantiate(enemyExplosion, transform.position, Quaternion.identity);
        Instantiate(healthPackPrefab, transform.position, Quaternion.identity);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }



}
