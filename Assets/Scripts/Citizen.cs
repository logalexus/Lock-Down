using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _citizen;

    private bool _isMove = true;
    private float _speed = 4f;
    private Animator _animator;
    private List<int> _direction;
    private GameObject _door;

    public bool Checked = false;
    public bool HasQRCode;


    void Start()
    {
        HasQRCode = Random.Range(0, 10) <= 5;

        _direction = new List<int>() { -1, 1 };
        _speed *= _direction[Random.Range(0, _direction.Count)];
        _animator = GetComponent<Animator>();
        Player.Instance.CompleteInteract += () => _isMove = true;
        
    }

    private void FixedUpdate()
    {
        if (_isMove)
            _citizen.velocity = new Vector2(_speed, _citizen.velocity.y);
        SetDirection();
        _animator.SetBool("isMove", _isMove);
    }

    private void SetDirection()
    {
        if (_citizen.velocity.x < 0) GetComponent<SpriteRenderer>().flipX = false;
        else if (_citizen.velocity.x > 0) GetComponent<SpriteRenderer>().flipX = true;
    }

    public void StopMove()
    {
        _isMove = false;
        _citizen.velocity = Vector3.zero;
    }

    public void SetDoor(GameObject door)
    {
        _door = door;
    }
    
    public void GoToHome()
    {
        _citizen.transform.DOMove(_door.transform.position, 1f).SetEase(Ease.InOutQuart);
    }

    private void OnDestroy()
    {
        Player.Instance.BeginInteract -= StopMove;
    }
}
