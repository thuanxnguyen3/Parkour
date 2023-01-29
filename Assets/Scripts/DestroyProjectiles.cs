using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectiles : MonoBehaviour
{
    public GameObject projectiles;
    //public AudioSource playerTakeDmg;
    public Transform playerPos;
    //[SerializeField] string instantiator;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(projectiles, 3f);
    }

    private void Update()
    {
        if (projectiles.tag == "EnemyProjectileBlue")
        {

            transform.position = Vector3.MoveTowards(transform.position, playerPos.position, 10f * Time.deltaTime);
        }
        if (projectiles.tag == "EnemyProjectileRed")
        {
            transform.Translate(Vector3.zero * 20f * Time.deltaTime);
        }
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().DamagePlayer(10);
            Destroy(projectiles);
        }
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHP = FindObjectOfType<PlayerHealth>();
            if(playerHP != null)
            {
                playerHP.DamagePlayer(5);
                Destroy(gameObject, 0.01f);
            }
            
        }
    }*/





}