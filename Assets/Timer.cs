using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timeText;
    private float _timeRemaining = 120f;

    private void Start()
    {
        if (Globals.ZenMode)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (_timeRemaining <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        
        _timeRemaining -= Time.deltaTime;
        float minutes = Mathf.FloorToInt(_timeRemaining / 60); 
        float seconds = Mathf.FloorToInt(_timeRemaining % 60);
        timeText.text =  string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
