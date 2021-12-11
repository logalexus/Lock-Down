using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class TextSpawn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _speakerText;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private CheckQRGame _checkQRGame;
    [SerializeField] private CheckQRUITransition _checkQRUITransition;
    

    private int _index = 0;
    private float _dialogueSpeed = 0.05f;
    private bool _isTyping = false;
    private bool _isIteractable = false;
    private bool _isValidQR = false;
    private Citizen _citizen;

    private List<string> _policeStartSentences;
    private List<string> _policeFineSentences;
    private List<string> _policeSuccessSentences;
    private List<string> _policeWrongQRSentences;
    private List<string> _citizenNonQRSentences;
    private List<string> _citizenNonQREndSentences;
    private List<string> _citizenQRSentences;
    private List<string> _citizenQREndSentences;
    private List<string> _policeAskMaskSentences;
    private List<string> _citizenAskMaskSentences;
    private List<string> _policeFineMaskSentences;

    private void Start()
    {
        _policeAskMaskSentences = new List<string>() { "Здраствуйте, почему вы без маски?", "Здраствуйте, где ваша маска?" };
        _citizenAskMaskSentences = new List<string>() { "Она дома...", "А зачем мне маска, я же не в помещении!?", "Какая маска?" };
        _policeFineMaskSentences = new List<string>() { "Я выписываю вам штраф, отправляйтесь домой", "Во время локдауна выходить из дома без маски запрещено, идите домой", "Идите домой, пока я не проверил еще и ваш QR-код" };

        _policeStartSentences = new List<string>(){ "Здраствуйте, предъявите Ваш QR-код!"};
        _policeFineSentences = new List<string>() { "Извините, но я вынужден вас оштрафовать и отправить обратно домой", "Нельзя выходить из дома без QR-кода! Заплатите штраф и возвращайтесь домой" };
        _policeWrongQRSentences = new List<string>() { "Ваш код подделка, Вы оштрафованы, теперь отправляйтесь домой", "Ваш код недействителен, заплатите штраф и возвращайтесь домой" };
        _policeSuccessSentences = new List<string>() { "Всё верно, можете идти", "QR действителен, можете идти" };
        _citizenQRSentences = new List<string>(){ "Да, конечно, вот!", "Вот, смотрите", "Всегда с собой, сканируйте" };
        _citizenQREndSentences = new List<string>() { "Отлично, до свидания", "Хорошо, спасибо"};
        _citizenNonQREndSentences = new List<string>() { "Как так, нееет", "Ну и ладно" };
        _citizenNonQRSentences = new List<string>() { "У меня его нет", "У меня нет такого", "Еще не выдали" };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isIteractable)
            NextSentence();
    }

    public void Begin(Citizen citizen)
    {
        _speakerText.text = "Вы: ";
        _citizen = citizen;
        _isIteractable = true;
        NextSentence();
    }

    public void ContinueDialog(bool CheckQRResult)
    {
        _isIteractable = true;
        _isValidQR = CheckQRResult;
        NextSentence();
    }

    private void NextSentence()
    {
        if (!_isTyping)
        {
            switch (_index)
            {
                case 0:
                    if (!_citizen.haveMask)
                        StartCoroutine(WriteSentence(GetRandomPhrase(_policeAskMaskSentences)));
                    else
                        StartCoroutine(WriteSentence(GetRandomPhrase(_policeStartSentences)));
                    break;
                case 1:
                    if (!_citizen.haveMask)
                    {
                        StartCoroutine(WriteSentence(GetRandomPhrase(_citizenAskMaskSentences)));
                        break;
                    }
                    if (_citizen.HasQRCode)
                        StartCoroutine(WriteSentence(GetRandomPhrase(_citizenQRSentences)));
                    else
                        StartCoroutine(WriteSentence(GetRandomPhrase(_citizenNonQRSentences)));
                    break;
                case 2:
                    if (!_citizen.haveMask)
                    {
                        StartCoroutine(WriteSentence(GetRandomPhrase(_policeFineMaskSentences)));
                        _index++;
                        break;
                    }
                    if (_citizen.HasQRCode)
                    {
                        StartCoroutine(WriteSentence("Сейчас просканирую"));
                        _checkQRUITransition.ShowScanButton();
                        _isIteractable = false;
                    }
                    else
                    {
                        StartCoroutine(WriteSentence(GetRandomPhrase(_policeFineSentences)));
                    }
                    break;
                case 3:
                    if (_citizen.HasQRCode)
                    {
                        if (_isValidQR)
                            StartCoroutine(WriteSentence(GetRandomPhrase(_policeSuccessSentences)));
                        else
                            StartCoroutine(WriteSentence(GetRandomPhrase(_policeWrongQRSentences)));
                    }
                    else
                    {
                        StartCoroutine(WriteSentence(GetRandomPhrase(_citizenNonQREndSentences)));
                    }
                    break;
                case 4:
                    CompliteDialog();
                    break;

            }
        }
    }

    private void CompliteDialog()
    {
        if (!_isValidQR || !_citizen.HasQRCode || !_citizen.haveMask)
        {
            _citizen.GoToHome();
            Player.Instance.ImpostersFound++;
        }
        _checkQRUITransition.HideDialog(() => _dialogueText.text = "");
        _index = 0;
        _isTyping = false;
        _speakerText.text = "Вы: ";
        StopAllCoroutines();
        Player.Instance.OnCompleteInteract();
    }

    private string GetRandomPhrase(List<string> phrases)
    {
        return phrases[Random.Range(0, phrases.Count)];
    }

    IEnumerator WriteSentence(string sentence)
    {
        if (_index % 2 == 0) _speakerText.text = "Вы: ";
        else _speakerText.text = "Гражданин: ";
        _dialogueText.text = "";
        _isTyping = true;
        foreach (char character in sentence.ToCharArray())
        {
            _dialogueText.text += character;
            yield return new WaitForSeconds(_dialogueSpeed);
        }
        _index++;
        _isTyping = false;
    }
    
}
