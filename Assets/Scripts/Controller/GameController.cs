using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public event UnityAction GameOver;
    public event UnityAction StartGame;
    
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);
        
    }
    
    

    public void OnGameOver()
    {
        GameOver?.Invoke();
    }

    public void OnStartGame()
    {
        StartGame?.Invoke();
    }
    
}
