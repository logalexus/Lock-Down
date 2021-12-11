using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class TextSpawnForFinal : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private string[] _sentences;
    

    private int _index;
    private float _dialogueSpeed = 0.05f;
    private bool _isTyping = false;

    public void NextSentence()
    {
        if (_index <= _sentences.Length - 1)
        {
            if (!_isTyping)
            {
                if (Player.Instance.ImpostersFound >= 10)
                    StartCoroutine(WriteSentence($"Хорошая работа, ты выявил более 10 нарушителей!"));
                else
                    StartCoroutine(WriteSentence($"Вы не справились со своей работой, попробуйте еще раз!"));
            }
        }
    }

    IEnumerator WriteSentence(string sen)
    {
        _dialogueText.text = "";
        _isTyping = true;
        foreach (char character in sen.ToCharArray())
        {
            _dialogueText.text += character;
            yield return new WaitForSeconds(_dialogueSpeed);
        }
        _index++;
        _isTyping = false;
    }
    
}
