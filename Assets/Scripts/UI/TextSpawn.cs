using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class TextSpawn : MonoBehaviour
{
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
    

    private void Start()
    {
        _policeStartSentences = new List<string>(){ "�����������, ���������� ��� QR-���!"};
        _policeFineSentences = new List<string>() { "��������, �� � �������� ��� ����������� � ��������� ������� �����", "������ �������� �� ���� ��� QR-����! ��������� ����� � ������������� �����" };
        _policeWrongQRSentences = new List<string>() { "��� ��� ��������, �� �����������, ������ ������������� �����", "��� ��� ��������������, ��������� ����� � ������������� �����" };
        _policeSuccessSentences = new List<string>() { "�� �����, ������ ����", "QR ������������, ������ ����" };
        _citizenQRSentences = new List<string>(){ "��, �������, ���!", "���, ��������", "������ � �����, ����������" };
        _citizenQREndSentences = new List<string>() { "�������, �� ��������", "������, ���cb��"};
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
                    StartCoroutine(WriteSentence(GetRandomPhrase(_policeStartSentences)));
                    break;
                case 1:
                    if (_citizen.HasQRCode)
                        StartCoroutine(WriteSentence(GetRandomPhrase(_citizenQRSentences)));
                    else
                        StartCoroutine(WriteSentence(GetRandomPhrase(_citizenNonQRSentences)));
                    break;
                case 2:
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
                            StartCoroutine(WriteSentence(GetRandomPhrase(_citizenQREndSentences)));
                        else
                            StartCoroutine(WriteSentence(GetRandomPhrase(_policeWrongQRSentences)));
                    }
                    else
                    {
                        StartCoroutine(WriteSentence(GetRandomPhrase(_citizenNonQREndSentences)));
                        CompliteDialog();
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
        if (!_isValidQR || !_citizen.HasQRCode)
            _citizen.GoToHome();
        _checkQRUITransition.HideDialog(() => _dialogueText.text = "");
        _index = 0;
        Player.Instance.OnCompleteInteract();
    }

    private string GetRandomPhrase(List<string> phrases)
    {
        return phrases[Random.Range(0, phrases.Count)];
    }

    IEnumerator WriteSentence(string sentence)
    {
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
