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
        _policeAskMaskSentences = new List<string>() { "�����������, ������ �� ��� �����?", "�����������, ��� ���� �����?" };
        _citizenAskMaskSentences = new List<string>() { "��� ����...", "� ����� ��� �����, � �� �� � ���������!?", "����� �����?" };
        _policeFineMaskSentences = new List<string>() { "� ��������� ��� �����, ������������� �����", "�� ����� �������� �������� �� ���� ��� ����� ���������, ����� �����", "����� �����, ���� � �� �������� ��� � ��� QR-���" };

        _policeStartSentences = new List<string>(){ "�����������, ���������� ��� QR-���!"};
        _policeFineSentences = new List<string>() { "��������, �� � �������� ��� ����������� � ��������� ������� �����", "������ �������� �� ���� ��� QR-����! ��������� ����� � ������������� �����" };
        _policeWrongQRSentences = new List<string>() { "��� ��� ��������, �� �����������, ������ ������������� �����", "��� ��� ��������������, ��������� ����� � ������������� �����" };
        _policeSuccessSentences = new List<string>() { "�� �����, ������ ����", "QR ������������, ������ ����" };
        _citizenQRSentences = new List<string>(){ "��, �������, ���!", "���, ��������", "������ � �����, ����������" };
        _citizenQREndSentences = new List<string>() { "�������, �� ��������", "������, �������"};
        _citizenNonQREndSentences = new List<string>() { "��� ���, �����", "�� � �����" };
        _citizenNonQRSentences = new List<string>() { "� ���� ��� ���", "� ���� ��� ������", "��� �� ������" };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isIteractable)
            NextSentence();
    }

    public void Begin(Citizen citizen)
    {
        _speakerText.text = "��: ";
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
                        StartCoroutine(WriteSentence("������ �����������"));
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
        _speakerText.text = "��: ";
        StopAllCoroutines();
        Player.Instance.OnCompleteInteract();
    }

    private string GetRandomPhrase(List<string> phrases)
    {
        return phrases[Random.Range(0, phrases.Count)];
    }

    IEnumerator WriteSentence(string sentence)
    {
        if (_index % 2 == 0) _speakerText.text = "��: ";
        else _speakerText.text = "���������: ";
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
