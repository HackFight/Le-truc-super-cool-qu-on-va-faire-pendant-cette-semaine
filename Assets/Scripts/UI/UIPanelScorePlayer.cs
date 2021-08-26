using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelScorePlayer : MonoBehaviour
{
    [Header("Player Profile")]
    [SerializeField] private GameObject _playerProfile;
    [SerializeField] private Image _playerProfileImage;
    [SerializeField] private List<Sprite> _playerSprites;

    [Header("Trophy")] 
    [SerializeField] private GameObject _prefabTrophyIcon;
    [SerializeField] private GameObject _trophiesList;

    private int _nbTrophies;
    private int _playerID;
    private int _score;


    public void Init(int nbTrophies, int playerID, int score)
    {
        _nbTrophies = nbTrophies;
        _playerID = playerID;
        _score = score;

        for (int i = 0; i < nbTrophies; i++)
        {
            var trophyIconInstance = Instantiate(_prefabTrophyIcon);
            trophyIconInstance.transform.parent = _trophiesList.transform;

            if (i >= _score)
            {
                trophyIconInstance.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 0.2f);
            }

            _playerProfileImage.sprite = _playerSprites[_playerID];
        }

        transform.localScale = Vector3.one;
    }
}
