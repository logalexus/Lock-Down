using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class CheckQRGame : MonoBehaviour
{
    [SerializeField] private ShakeHand _shakeHand;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private CheckQRGameTransition _checkQRGameTransition;
    [SerializeField] private CheckQRUITransition _checkQRUITransition;


    public UnityAction BeginPlay;
    public UnityAction<bool> CompletePlay;

    

    public void BeginGame()
    {
        _checkQRUITransition.ShowProgressBar();
        _checkQRGameTransition.ShowGame(() =>
        {
            _scanner.StartScane();
            _shakeHand.StartShake();
            BeginPlay?.Invoke();
        });
    }

    public void CompleteGame(bool result)
    {
        CompletePlay?.Invoke(result);
        _shakeHand.StopShake();
        _checkQRUITransition.HideProgressBar();
        _checkQRGameTransition.HideGame();
    }
}
