using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CheckQRGameTransition : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private Transform _back;
    [SerializeField] private Transform _scanner;
    
    private float _offsetHide = 30;
    private Transform _player;


    private void Start()
    {
        HideGame();
        _player = Player.Instance.GetComponent<Transform>();

    }

    public void ShowGame(TweenCallback tweenCallback)
    {
        transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, 0);

        _hand.DOMoveY(_hand.position.y - _offsetHide, 1).SetEase(Ease.InOutQuart).OnComplete(tweenCallback);
        _scanner.DOMoveY(_scanner.position.y + _offsetHide, 1).SetEase(Ease.InOutQuart);
        _back.DOMoveY(_hand.position.y - _offsetHide, 1).SetEase(Ease.InOutQuart);
    }

    public void HideGame()
    {
        _hand.DOMoveY(_hand.position.y + _offsetHide, 1).SetEase(Ease.InOutQuart);
        _scanner.DOMoveY(_scanner.position.y - _offsetHide, 1).SetEase(Ease.InOutQuart);
        _back.DOMoveY(_hand.position.y + _offsetHide, 1).SetEase(Ease.InOutQuart);

    }



}
