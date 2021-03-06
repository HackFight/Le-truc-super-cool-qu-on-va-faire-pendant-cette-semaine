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

        eggScript = FindObjectOfType<Egg>();
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
    }

    public void AddPlayer()
    {
        _playerScores.Add(0);

        var menuScore = FindObjectOfType<MenuScore>();

        menuScore.PlayerConnection(_playerScores.Count);
    }

    public void NextMatch()
    {
        playerManager.NextMatch();

        chrono = 0;

        eggScript.Reset();
    }
}
