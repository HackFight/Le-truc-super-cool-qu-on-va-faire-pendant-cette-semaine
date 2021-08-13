using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScore : MonoBehaviour
{
    [Header("Players")] 
    [SerializeField] private GameObject _prefabPlayerScoreMenu;
    [SerializeField] private GameObject _playerListParent;

    private int _nbPlayer;
    private int _nbTrophy;

    public void Init(int nbPlayer, int nbTrophy)
    {
        _nbPlayer = nbPlayer;
        _nbTrophy = nbTrophy;

        for (int i = 0; i < _nbPlayer; i++)
        {
            var instance = Instantiate(_prefabPlayerScoreMenu);
            instance.transform.parent = _playerListParent.transform;

            instance.GetComponent<UIPanelScorePlayer>().Init(_nbTrophy, i);
        }
    }
}
