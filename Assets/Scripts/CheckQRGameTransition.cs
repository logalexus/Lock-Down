using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CheckQRGameTransition : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private Transform _scanner;
    
    private float _offsetHide = 30;
    private Transform _player;


    private void Start()
    {
        _hand.DOMoveY(_hand.position.y + _offsetHide, 0);
        _scanner.DOMoveY(_scanner.position.y - _offsetHide, 0);
        _player = Player.Instance.GetComponent<Transform>();

    }

    public void ShowGame(TweenCallback tweenCallback)
    {
        transform.position = new Vector3(_player.position.x, transform.position.y, 0);

        _hand.DOMoveY(_hand.position.y - _offsetHide, 1).SetEase(Ease.InOutQuart).OnComplete(tweenCallback);
        _scanner.DOMoveY(_scanner.position.y + _offsetHide, 1).SetEase(Ease.InOutQuart);

    }

    public void HideGame(TweenCallback tweenCallback)
    {
        _hand.DOMoveY(_hand.position.y + _offsetHide, 1).SetEase(Ease.InOutQuart).OnComplete(tweenCallback);
        _scanner.DOMoveY(_scanner.position.y - _offsetHide, 1).SetEase(Ease.InOutQuart);

    }



}
