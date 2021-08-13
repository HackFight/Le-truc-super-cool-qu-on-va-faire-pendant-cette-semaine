using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Scores")] 
    [SerializeField] private int _winingScore = 5;

    public float chrono;
    private Egg eggScript;
    public float timeToOpenEgg = 30.0f;

    private List<int> _playerScores;

    private PlayerManager playerManager;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        _playerScores = new List<int>();
    }

    public void IncrementChrono()
    {
        chrono += Time.deltaTime;
    }

    public void IncrementScore(int score, int playerID)
    {
        _playerScores[playerID] += score;
    }

    public void ShowScoreScreen()
    {
        var menuScore = FindObjectOfType<MenuScore>();

        if (!menuScore)
        {
            Debug.LogError("The scene needs a MenuScore");
            return;
        }

        if (menuScore.IsActive())
        {
            return;
        }

        menuScore.Init(_playerScores.Count, _winingScore, _playerScores);

        //Count score
        for (var index = 0; index < _playerScores.Count; index++)
        {
            var playerScore = _playerScores[index];

            if (playerScore == _winingScore)
            {

            }
        }
    }

    public void AddPlayer()
    {
        _playerScores.Add(0);
    }

    public void NextMatch()
    {

    }
}
