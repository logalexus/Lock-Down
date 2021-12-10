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
        _checkQRGameTransition.HideGame(() =>
        {
            _scanner.StartScane();
            _shakeHand.StartShake();
        });
    }
}
