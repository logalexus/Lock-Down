using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class WorkTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerUI;
    [SerializeField] private FinalScreenGoodWork _finalScreenGoodWork;

    private float time = 300;
    private float _timeLeft = 0f;

    private void Start()
    {
        _timeLeft = time;
        GameController.Instance.StartGame += () => StartCoroutine(StartTimer());
    }
    private IEnumerator StartTimer()
    {
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            UpdateTimeText();
            yield return null;
        }
    }

    private void UpdateTimeText()
    {
        if (_timeLeft < 0)
            _timeLeft = 0;

        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        _timerUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if(_timeLeft == 0)
        {
            _finalScreenGoodWork.Open(gameObject);
            GameController.Instance.OnGameOver();
        }
    }
    
    
}
