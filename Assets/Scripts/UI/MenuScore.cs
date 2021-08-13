using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScore : MonoBehaviour
{
    [Header("Players")] 
    [SerializeField] private GameObject _prefabPlayerScoreMenu;
    [SerializeField] private GameObject _playerListParent;

    [Header("Menu")] 
    [SerializeField] private GameObject _menuScore;
    [SerializeField] private GameObject _buttonNextMatch;
    [SerializeField] private string _menuSceneName;
    [SerializeField] private GameObject _buttonMenu;

    private int _nbPlayer;
    private int _nbTrophy;

    void Start()
    {
        _menuScore.SetActive(false);
    }

    public void Init(int nbPlayer, int nbTrophy, List<int> scores)
    {
        _menuScore.SetActive(true);

        _nbPlayer = nbPlayer;
        _nbTrophy = nbTrophy;

        bool isOver = false;

        for (int i = 0; i < _nbPlayer; i++)
        {
            var instance = Instantiate(_prefabPlayerScoreMenu);
            instance.transform.parent = _playerListParent.transform;

            instance.GetComponent<UIPanelScorePlayer>().Init(_nbTrophy, i, scores[i]);

            if (scores[i] == nbTrophy)
            {
                isOver = true;
            }
        }

        //Set button
        if (isOver)
        {
            _buttonMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_buttonMenu);

            _buttonNextMatch.SetActive(false);
        }
        else
        {
            _buttonMenu.SetActive(false);

            _buttonNextMatch.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_buttonNextMatch);

        }
    }

    public bool IsActive()
    {
        return _menuScore.activeSelf;
    }

    public void NextMatch()
    {
        _menuScore.SetActive(false);

        FindObjectOfType<GameManager>().NextMatch();
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(_menuSceneName);
    }
}
