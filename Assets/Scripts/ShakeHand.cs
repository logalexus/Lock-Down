using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeHand : MonoBehaviour
{
    private Vector2 _mainPosition;
    private float _durationOffset = 1;
    private float _offset = 1;


    private void Start()
    {
        
    }

    public void StartShake()
    {
        _mainPosition = transform.position;
        StartCoroutine(Shake());
    }
    public void StopShake()
    {
        StopAllCoroutines();
    }

    private IEnumerator Shake()
    {
        while (true)
        {
            Vector2 offsetPosition = _mainPosition + new Vector2(Random.Range(-_offset, _offset), Random.Range(-_offset, _offset));
            transform.DOMove(offsetPosition, _durationOffset).SetEase(Ease.InOutQuad);
            yield return new WaitForSeconds(_durationOffset);
        }
        
    }
    
}
