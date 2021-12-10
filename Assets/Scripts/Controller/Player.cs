using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Player : MonoBehaviour
{
    public static Player Instance;

    public event UnityAction BeginInteract;
    public event UnityAction CompleteInteract;

    private void Awake()
    {
            Instance = this;
    }

    public void OnBeginInteract()
    {
        BeginInteract?.Invoke();
    }

    public void OnCompleteInteract()
    {
        CompleteInteract?.Invoke();
    }



}
