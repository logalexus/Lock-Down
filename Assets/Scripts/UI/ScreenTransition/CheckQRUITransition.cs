using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckQRUITransition : MonoBehaviour
{
    [SerializeField] private RectTransform _dialog;
    [SerializeField] private Button _scanButton;
    [SerializeField] private RectTransform _progressBar;


    private float _distance = 350;
    private float _duration = 1f;

    public void ShowDialog(TweenCallback tweenCallback)
    {
        _dialog.DOMove(new Vector3(_dialog.position.x, _dialog.position.y + _distance), _duration).SetEase(Ease.InOutQuart).OnComplete(tweenCallback);
    }

    public void HideDialog()
    {
        _dialog.DOMove(new Vector3(_dialog.position.x, _dialog.position.y - _distance), _duration).SetEase(Ease.InOutQuart);
    }

    public void HideDialog(TweenCallback tweenCallback)
    {
        _dialog.DOMove(new Vector3(_dialog.position.x, _dialog.position.y - _distance), _duration).SetEase(Ease.InOutQuart).OnComplete(tweenCallback);
    }

    public void ShowScanButton()
    {
        _scanButton.interactable = true;
        _scanButton.GetComponent<CanvasGroup>().DOFade(1, 1).SetEase(Ease.InOutQuart);
    }
    public void HideScanButton()
    {
        _scanButton.GetComponent<CanvasGroup>().DOFade(0, 1).SetEase(Ease.InOutQuart);
    }

    public void ShowProgressBar()
    {
        _progressBar.DOMove(new Vector3(_progressBar.position.x, _progressBar.position.y + _distance), _duration).SetEase(Ease.InOutQuart);
    }

    public void HideProgressBar()
    {
        _progressBar.DOMove(new Vector3(_progressBar.position.x, _progressBar.position.y - _distance), _duration).SetEase(Ease.InOutQuart);
    }


}
