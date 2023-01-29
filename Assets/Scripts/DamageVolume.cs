using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVolume : MonoBehaviour
{
    PlayerHealth playerHP;

    private void Start()
    {
        playerHP = gameObject.GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other != null && playerHP != null)
        {
            playerHP.DamagePlayer(100);
        }
    }
}
