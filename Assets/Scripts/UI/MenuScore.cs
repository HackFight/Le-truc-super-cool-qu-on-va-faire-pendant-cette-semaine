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
    [SerializeField] private GameObject _buttonStart;

    private int _nbPlayer;
    private int _nbTrophy;

    void Start()
    {
        //_menuScore.SetActive(false);
    }

    public void PlayerConnection(int nbPlayer)
    {
        _menuScore.SetActive(true);

        foreach (Transform child in _playerListParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        _nbPlayer = nbPlayer;

        for (int i = 0; i < _nbPlayer; i++)
        {
            var instance = Instantiate(_prefabPlayerScoreMenu);
            instance.transform.parent = _playerListParent.transform;

            instance.GetComponent<UIPanelScorePlayer>().Init(_nbTrophy, i, 0);
        }

        _buttonMenu.SetActive(false);
        _buttonNextMatch.SetActive(false);
        _buttonStart.SetActive(true);

        EventSystem.current.SetSelectedGameObject(_buttonStart);

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

            Debug.Log("Score[" + i + "] = " + scores[i]);
            if (scores[i] >= nbTrophy)
            {
                Debug.Log("Player " + i + " won !!!");
                isOver = true;
            }
        }

        //Set button
        if (isOver)
        {
            _buttonMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_buttonMenu);

            _buttonNextMatch.SetActive(false);

            _buttonStart.SetActive(false);
        }
        else
        {
            _buttonMenu.SetActive(false);

            _buttonNextMatch.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_buttonNextMatch);

            _buttonStart.SetActive(false);

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

        foreach (Transform child in _playerListParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(_menuSceneName);
    }

    public void StartGame()
    {
        foreach (Transform child in _playerListParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        _menuScore.SetActive(false);
        FindObjectOfType<PlayerManager>().StartGame();
    }
}
