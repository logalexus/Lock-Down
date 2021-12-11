using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class FinalScreenGoodWork : IteractPanel
{
    [SerializeField] private FinalScreenGoodWorkTransition _finalScreenGoodWorkTransition;
    [SerializeField] private List<IteractPanel> _panels;
    [SerializeField] private Button _restart;

    private void Start()
    {
        _restart.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        });
    }

    public override void Open(GameObject caller)
    {
        base.Open(caller);
        foreach (var item in _panels)
            item.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
        _finalScreenGoodWorkTransition.OpenAnim();

    }

    public override void Close()
    {
        StopAllCoroutines();
        _finalScreenGoodWorkTransition.CloseAnim().OnComplete(() =>
        {
            base.Close();
        });
    }
}
