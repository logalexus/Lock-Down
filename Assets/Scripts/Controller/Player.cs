using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private TextMeshProUGUI _impostorsText;

    public event UnityAction BeginInteract;
    public event UnityAction CompleteInteract;
    public int ImpostersFound
    {
        get => _impostors;
        set
        {
            _impostors = value;
            _impostorsText.text = _impostors.ToString();
        }
    }

    private int _impostors = 0;

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
