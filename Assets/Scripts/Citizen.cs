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
        Player.Instance.BeginInteract += () => _isMove = false;
        Player.Instance.CompleteInteract += () => _isMove = true;

        _citizen = GetComponent<Rigidbody2D>();
        _direction = new List<int>() { -1, 1 };
        _speed *= _direction[Random.Range(0, _direction.Count)];
    }

    private void FixedUpdate()
    {
        if (_isMove)
        {
            _citizen.velocity = new Vector2(_speed, _citizen.velocity.y);
        }
        //PlayAnimation();
        SetDirection();
    }

    void SetDirection()
    {
        if (_citizen.velocity.x < 0) GetComponent<SpriteRenderer>().flipX = false;
        else if (_citizen.velocity.x > 0) GetComponent<SpriteRenderer>().flipX = true;
    }
}
