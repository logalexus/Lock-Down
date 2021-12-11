using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Iteracter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private CheckQRGame _checkQRGame;
    [SerializeField] private CheckQRUI _checkQRUI;


    private bool isIteractable;


    private void Update()
    {
        if (isIteractable && Input.GetKeyDown(KeyCode.E))
        {
            AudioController.Instance.PlaySFX(AudioController.Instance.Sounds.Click);
            enabled = false;
            Player.Instance.OnBeginInteract();
            _checkQRGame.BeginGame();
            _checkQRUI.Open();
            //Player.Instance.GetComponent<PlayerMove>().enabled = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.transform.TryGetComponent(out Citizen c))
        {
            isIteractable = true;
            _sprite.DOFade(1, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Citizen c))
        {
            _sprite.DOFade(0, 0.5f);
            isIteractable = false;
        }
    }
}
