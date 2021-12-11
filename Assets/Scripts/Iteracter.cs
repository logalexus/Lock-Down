using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Iteracter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private CheckQRUI _checkQRUI;
    
    private bool isIteractable;
    private Citizen _citizen;

    private void Start()
    {
        //Player.Instance.CompleteInteract += () => isIteractable = true;
    }


    private void Update()
    {
        if (isIteractable && Input.GetKeyDown(KeyCode.E))
        {
            isIteractable = false;
            AudioController.Instance.PlaySFX(AudioController.Instance.Sounds.Click);
            Player.Instance.OnBeginInteract();
            _citizen.StopMove();
            _checkQRUI.StartDialog(_citizen);
            _citizen.Checked = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.transform.TryGetComponent(out Citizen citizen) && !citizen.Checked)
        {
            isIteractable = true;
            _citizen = citizen;
            _sprite.DOFade(1, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Citizen citizen))
        {
            _sprite.DOFade(0, 0.5f);
            isIteractable = false;
        }
    }
}
