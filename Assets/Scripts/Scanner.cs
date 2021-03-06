using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Scanner : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private CheckQRGame _checkQRGame;


    private bool _isScanning = false;
    private Image _fillRect;

    private void Start()
    {
        _fillRect = _slider.fillRect.GetComponent<Image>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isScanning && collision.TryGetComponent(out QR qr))
        {
            _isScanning = false;
            StopAllCoroutines();
            _fillRect.color = Color.red;
            AudioController.Instance.PlaySFX(AudioController.Instance.Sounds.Error);
            Sequence seq = DOTween.Sequence();
            seq.Join(DOTween.To(()=>_slider.value, x=> _slider.value = x, 0, 1).SetEase(Ease.InOutQuart));
            seq.Join(_slider.transform.DOShakePosition(1, 10, 10, 90, false, false));
            seq.OnComplete(() =>
            {
                _isScanning = true;
                _fillRect.color = Color.green;
                StartCoroutine(ProccessScan());
            });
        }
    }
    
    public void StartScane()
    {
        _isScanning = true;
        _slider.value = 0;
        StartCoroutine(ProccessScan());
    }

    private IEnumerator ProccessScan()
    {
        while(true)
        {
            if (_slider.value == 100)
            {
                _isScanning = false;
                StopAllCoroutines();
                _checkQRGame.CompleteGame(UnityEngine.Random.Range(0, 10) <= 7);
                break;
            }
            if (_isScanning)
                _slider.value += 1;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
