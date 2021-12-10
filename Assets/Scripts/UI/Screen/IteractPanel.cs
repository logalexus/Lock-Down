using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IteractPanel : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _caller;


    public void Awake()
    {
        _canvas.enabled = false;
    }

    public virtual void Open(GameObject caller)
    {
        _canvas.enabled = true;
        _caller = caller;
    }

    public virtual void Close()
    {
        _canvas.enabled = false;
    }

    public void SetInteractable(bool value)
    {
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;

    }
}
