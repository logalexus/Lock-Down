using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CheckQRUI : MonoBehaviour
{
    [SerializeField] private CheckQRUITransition _checkQRUITransition;
    [SerializeField] private CheckQRGame _checkQRGame;
    [SerializeField] private TextSpawn _textSpawn;
    [SerializeField] private Button _scanButton;
    [SerializeField] private Button _menu;
    [SerializeField] private Button _begin;
    [SerializeField] private GameObject _learnPanel;
    

    private void Start()
    {
        _scanButton.onClick.AddListener(() =>
        {
            _scanButton.interactable = false;
            _checkQRUITransition.HideDialog();
            _checkQRGame.BeginGame();
        });
        _menu.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
        _begin.onClick.AddListener(() =>
        {
            _learnPanel.SetActive(false);
            GameController.Instance.OnStartGame();
        });
        _checkQRGame.CompletePlay += (result) =>
        {
            _checkQRUITransition.ShowDialog(()=> { });
            _checkQRUITransition.HideScanButton();
            _textSpawn.ContinueDialog(result);
        };
    }

    public void StartDialog(Citizen citizen)
    {
        _checkQRUITransition.ShowDialog(()=>
        {
            _textSpawn.Begin(citizen);
        });
    }
    

    
}
