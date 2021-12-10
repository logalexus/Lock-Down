using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CheckQRGameTransition : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private Transform _scanner;

    private Vector2 _mainPositionHand;
    private Vector2 _mainPositionScanner;
    private float _offsetHide = 30;
    private Vector2 _offsetHand;
    private Vector2 _offsetScanner;
    private Transform _player;


    private void Start()
    {
        _mainPositionHand = _hand.position;
        _mainPositionScanner = _scanner.position;

        _offsetHand = new Vector2(_scanner.position.x, _scanner.position.y + _offsetHide);
        _offsetScanner = new Vector2(_scanner.position.x, _scanner.position.y - _offsetHide);
        
        _hand.position += (Vector3)_offsetHand;
        _scanner.position += (Vector3)_offsetScanner;

        _player = Player.Instance.GetComponent<Transform>();

    }

    public void ShowGame(TweenCallback tweenCallback)
    {
        _hand.position = new Vector3(_player.position.x, _hand.position.y, 0);
        _scanner.position = new Vector3(_player.position.x, _scanner.position.y, 0);
        _mainPositionHand.x = _player.position.x;
        _mainPositionScanner.x = _player.position.x;
        
        _hand.DOMove(_mainPositionHand, 1).SetEase(Ease.InOutQuart).OnComplete(tweenCallback);
        _scanner.DOMove(_mainPositionScanner, 1).SetEase(Ease.InOutQuart);

    }

    public void HideGame(TweenCallback tweenCallback)
    {
        _hand.DOMove(_offsetHand, 1).SetEase(Ease.InOutQuart).OnComplete(tweenCallback);
        _scanner.DOMove(_offsetScanner, 1).SetEase(Ease.InOutQuart);

    }



}
