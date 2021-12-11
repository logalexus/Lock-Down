using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour
{
    private bool _isMove = true;
    private float _speed = 4f;
    private Rigidbody2D _citizen;
    private List<int> _direction;

    void Start()
    {
        _citizen = GetComponent<Rigidbody2D>();
        _direction = new List<int>() { -1, 1 };
        _speed *= _direction[Random.Range(0, _direction.Count)];

        Player.Instance.CompleteInteract += () => _isMove = true;
        Player.Instance.BeginInteract += () =>
        {
            _isMove = false;
            StopMove();
        };
        
    }

    private void FixedUpdate()
    {
        if (_isMove)
            _citizen.velocity = new Vector2(_speed, _citizen.velocity.y);
        //PlayAnimation();
    }

    private void StopMove()
    {
        _citizen.velocity = Vector3.zero;
    }
}
