using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StartSceneTransition : MonoBehaviour
{
    [SerializeField] private Image _sprite;

    void Start()
    {
        _sprite.DOFade(0, 1);
    }
    
}
