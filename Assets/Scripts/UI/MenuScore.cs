using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScore : MonoBehaviour
{
    [Header("Players")] 
    [SerializeField] private GameObject _prefabPlayerScoreMenu;
    [SerializeField] private GameObject _playerListParent;

    [Header("Menu")] 
    [SerializeField] private GameObject _menuScore;

    private int _nbPlayer;
    private int _nbTrophy;

    void Start()
    {
        _menuScore.SetActive(false);
    }

    public void Init(int nbPlayer, int nbTrophy)
    {
        _menuScore.SetActive(true);

        _nbPlayer = nbPlayer;
        _nbTrophy = nbTrophy;

        for (int i = 0; i < _nbPlayer; i++)
        {
            var instance = Instantiate(_prefabPlayerScoreMenu);
            instance.transform.parent = _playerListParent.transform;

            instance.GetComponent<UIPanelScorePlayer>().Init(_nbTrophy, i);
        }
    }

    public bool IsActive()
    {
        return _menuScore.activeSelf;
    }
}
