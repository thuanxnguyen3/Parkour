using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float _playerHealth = 100f;
    //private float _damageAmount = 5f;
    private UIManager _uiManager;
    //private float _minHealth = 0f;
    private float _maxHealth = 100f;

    public bool isLoseState = false;

    Level01Controller LoseLevel;


    public AudioSource playerTakeDmg;

    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
        LoseLevel = FindObjectOfType<Level01Controller>();
    }

    private void Start()
    {
        _uiManager.UpdateHealthSliderMax(_maxHealth);
    }

    // Update is called once per frame
    void Update() 
    {
        //DamagePlayer(0);
        if (_playerHealth <= 0f)
        {
            isLoseState = true;
            LoseLevel.LoseState();
        }
    }

    public void DamagePlayer(float damageAmount)
    {
        playerTakeDmg.Play();
        _playerHealth -= damageAmount;
        //Debug.Log("The player's health is " + _playerHealth);
        _uiManager.UpdateHealthSlider(_playerHealth);
        
    }

    public void HealPlayer(float healAmount)
    {
        _playerHealth += healAmount;
        _uiManager.UpdateHealthSlider(_playerHealth);
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyProjectile")
        {
            DamagePlayer(10);
        }
    }*/
    
    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyProjectile"))
        {
            DamagePlayer(10);
        }
    }*/

}
