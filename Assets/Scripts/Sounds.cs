using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Sound", order = 1)]
public class Sounds : ScriptableObject
{
    [SerializeField] private AudioClip _mainTheme;
    [SerializeField] private AudioClip _human;
    [SerializeField] private AudioClip _error;


    public AudioClip MainTheme => _mainTheme;
    public AudioClip Error => _error;
    public AudioClip Human => _human;


}
