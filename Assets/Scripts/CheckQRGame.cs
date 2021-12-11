using UnityEngine;
using System.Collections;

public class CheckQRGame : MonoBehaviour
{
    [SerializeField] private ShakeHand _shakeHand;
    [SerializeField] private Scanner _scanner;


    private CheckQRGameTransition _checkQRGameTransition;

    private void Start()
    {
        _checkQRGameTransition = GetComponent<CheckQRGameTransition>();
        Player.Instance.CompleteInteract += () => CompleteGame();
    }

    public void BeginGame()
    {
        _checkQRGameTransition.ShowGame(() =>
        {
            _scanner.StartScane();
            _shakeHand.StartShake();
        });
    }

    public void CompleteGame()
    {
        _shakeHand.StopShake();
        _checkQRGameTransition.HideGame(() =>
        {
            
        });
    }
}
