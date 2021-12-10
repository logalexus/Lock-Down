using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class TextSpawn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private string[] _sentences;
    [SerializeField] private Button _close;

    private float _distance = 280f;
    private float _duration = 1f;

    private int _index;
    private float _dialogueSpeed = 0.05f;
    private bool _isTyping = false;
    private bool _isIteractable = true;


    private void Start()
    {
        _close.onClick.AddListener(() =>
        {
            Close();
        });
        NextSentence();
        transform.DOMove(new Vector3(transform.position.x, transform.position.y + _distance), _duration);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isIteractable)
        {
            NextSentence();
        }
    }

    void NextSentence()
    {
        if (_index <= _sentences.Length - 1)
        {
            if (!_isTyping)
            {
                _dialogueText.text = "";
                _isTyping = true;
                StartCoroutine(WriteSentence());
            }
        }
        else
        {
            Close();
        }
    }

    IEnumerator WriteSentence()
    {
        foreach (char character in _sentences[_index].ToCharArray())
        {
            _dialogueText.text += character;
            yield return new WaitForSeconds(_dialogueSpeed);
        }
        _index++;
        _isTyping = false;
    }

    void Close()
    {
        if (_isIteractable)
        {
            _isIteractable = false;
            GameController.Instance.OnStartGame();
            transform.DOMove(new Vector3(transform.position.x, transform.position.y - _distance), _duration).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}
