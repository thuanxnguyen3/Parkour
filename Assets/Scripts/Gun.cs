using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float Damage = 10f;
    public float Range = 100f;


    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioSource gunSoundEffect;
    public AudioSource bulletHitBodySfx;
    public AudioSource enemyDeathSFX;

    /*
    public Transform weaponDefaultPosition;
    public Transform weaponAimingPosition;
    public float adsSpeed;*/

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
        /*
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, weaponAimingPosition.localPosition, Time.deltaTime * adsSpeed);
        } else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, weaponDefaultPosition.localPosition, Time.deltaTime * adsSpeed);
        }*/
    }


    void Shoot()
    {
        muzzleFlash.Play();
        gunSoundEffect.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, Range))
        {
            EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
            if(enemy != null)
            {
                enemy.TakeDamage(20);
                bulletHitBodySfx.Play();
                /*
                EnemyAI enemyHealth = GetComponent<EnemyAI>();
                if(enemyHealth != null && enemyHealth.health <= 0)
                {
                    enemyDeathSFX.Play();
                }*/
            }

            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
          
        }
    }
}
