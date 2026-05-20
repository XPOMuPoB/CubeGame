using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatsScript : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] GameObject _inputUI;
    [SerializeField] GameObject _statUI;
    [SerializeField] TMP_InputField _nicknameField;


    [Header("OutTexts")]
    [SerializeField] Text NickText;
    [SerializeField] Text CountCast;
    [SerializeField] Text CountScore;
    [SerializeField] Text DateRecord;

    private GameManager _gameManager;
    private PlayerData _playerData;

    private string _inputNick;
    private bool NulOrEmpty = false;    
    private void Start()
    {
        _nicknameField.characterLimit = 10;
        _gameManager = FindAnyObjectByType<GameManager>();
        _playerData = FindAnyObjectByType<PlayerData>();
    }

    public void BtnNextClick()
    {
        if (NulOrEmpty == true)
        {
            _inputUI.SetActive(false);
            _statUI.SetActive(true);
            _playerData.SaveData(_inputNick, _gameManager.CountCast, _gameManager.CountScore);
        }

        if (_gameManager == null)
        {
            Debug.LogError("GameManager не найден!");
            return;
        }

        NickText.text = "Статистика игрока: " + _inputNick;
        CountCast.text = "Количество бросков: " + _gameManager.CountCast;
        CountScore.text = "Количество очков: " + _gameManager.CountScore;
        DateRecord.text = "Дата рекорда:" + DateTime.Now;

        if (_playerData == null)
        {
            Debug.LogError("_playerData не найден!");
            return;
        }


    }

    public void ReadNickName(string s)
    {
        NulOrEmpty = String.IsNullOrEmpty(s);

        if (NulOrEmpty)
        {
            Debug.Log("Пусто");
            NulOrEmpty = false;
        }
        else
        {
            if (NulOrEmpty = String.IsNullOrWhiteSpace(s))
            {
                Debug.Log("Пусто");
                NulOrEmpty = false;
            }
            else
            {
                _inputNick = s;
                NulOrEmpty = true;
                Debug.Log("Ник: " + s);
            }
        }
    }
}
