using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _citizen;
    [SerializeField] private GameObject _maskSprite;

    private bool _isMove = true;
    private float _speed = 4f;
    private Animator _animator;
    private List<int> _direction;
    private bool _haveMask;

    void Start()
    {
        _direction = new List<int>() { -1, 1 };
        _speed *= _direction[Random.Range(0, _direction.Count)];
        _animator = GetComponent<Animator>();
        Player.Instance.CompleteInteract += () => _isMove = true;
        Player.Instance.BeginInteract += StopMove;

        if(Random.Range(0, 10) > 3)
           _haveMask = true;
    }

    private void FixedUpdate()
    {
        if (_isMove)
            _citizen.velocity = new Vector2(_speed, _citizen.velocity.y);
        SetDirection();
        _animator.SetBool("isMove", _isMove);
        _animator.SetBool("haveMask", _haveMask);
    }

    private void SetDirection()
    {
        if (_citizen.velocity.x < 0) GetComponent<SpriteRenderer>().flipX = false;
        else if (_citizen.velocity.x > 0) GetComponent<SpriteRenderer>().flipX = true;
    }

    private void StopMove()
    {
        _isMove = false;
        _citizen.velocity = Vector3.zero;
        
    }

    private void OnDestroy()
    {
        Player.Instance.BeginInteract -= StopMove;
    }
}
