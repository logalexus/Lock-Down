using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class CheckQRUI : IteractPanel
{
    [SerializeField] private CheckQRUITransition checkQRUITransition;
    
    private void Start()
    {
        
    }

    public override void Open(GameObject caller)
    {
        base.Open(caller);
        checkQRUITransition.OpenAnim();
    }

    public override void Close()
    {
        checkQRUITransition.CloseAnim().OnComplete(()=> 
        {
            Player.Instance.GetComponent<Iteracter>().enabled = true;
            Player.Instance.GetComponent<PlayerMove>().enabled = true;
            base.Close();
        });
    }

    
}
