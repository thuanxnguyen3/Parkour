using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView;

    int _currentScore;
    public AudioSource enemyDeathSFX;

    // Start is called before the first frame update
    void Start()
    {
        enemyDeathSFX = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Exit Level
        // TODO bring up popup menu for navigation
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitLevel();
        }
        if(_currentScore >= 40)
        {
            WinState();
        }
    }

    public void IncreaseScore(int scoreIncrease)
    {
        // increase score
        _currentScore += scoreIncrease;
        //enemyDeathSFX.Play();
        // update score display, so we can see the new score
        _currentScoreTextView.text = "Score: " + _currentScore.ToString();
    }

    public void ExitLevel()
    {
        // compare score to high score
        int highScore = PlayerPrefs.GetInt("HighScore");
        if(_currentScore > highScore)
        {
            // save current score as new high score
            PlayerPrefs.SetInt("HighScore", _currentScore);
            Debug.Log("New high score: " + _currentScore);
        }

        // load new level
        SceneManager.LoadScene("MainMenu");
    }

    public void LoseState()
    {
        Debug.Log("you lost");
        // compare score to high score
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (_currentScore > highScore)
        {
            // save current score as new high score
            PlayerPrefs.SetInt("HighScore", _currentScore);
            Debug.Log("New high score: " + _currentScore);
        }
        SceneManager.LoadScene("LoseMenu");
        
    }

    public void WinState()
    {
        Debug.Log("you won");
        // compare score to high score
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (_currentScore > highScore)
        {
            // save current score as new high score
            PlayerPrefs.SetInt("HighScore", _currentScore);
            Debug.Log("New high score: " + _currentScore);
        }
        SceneManager.LoadScene("WinMenu");
    }

    public void PlayEnemyDeathSFX()
    {
        Debug.Log("Play enemy death sfx");
        enemyDeathSFX.Play();
    }
}
