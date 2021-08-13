using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource battle_intro;
    public AudioSource battle_soft;
    public AudioSource battle_hard;
    public AudioSource battle_golem;
    public AudioSource walkcycle_run;
    public AudioSource walkcycle_turn;
    public AudioSource walkcycle_turn2;

    private GameManager gameManager;
    private PlayerManager playerManager;
    private Egg eggScript;
    private PlayerMovement playerMovement;
    public bool isMusicActive = true;

    int statePlay = 0;
    void Start()
    {
        statePlay = 0;

        playerManager = FindObjectOfType<PlayerManager>();
        gameManager = FindObjectOfType<GameManager>();
        eggScript = FindObjectOfType<Egg>();
        playerMovement = FindObjectOfType<PlayerMovement>();

        if (isMusicActive)
        {
            battle_intro.Play();
        }
    }
    void Update()
    {
        if (isMusicActive)
        {
            if (battle_intro.isPlaying == false && statePlay == 0)
            {
                battle_soft.Play();
                battle_hard.Play();
                statePlay++;
            }
            else if (gameManager.chrono >= gameManager.timeToOpenEgg && statePlay == 1)
            {
                battle_soft.Stop();
                battle_hard.Stop();
                battle_golem.Play();
                statePlay++;
            }
            if (statePlay == 1 && eggScript.isGrabed)
            {
                battle_soft.volume = Mathf.Clamp(gameManager.timeToOpenEgg, 0f, 1f);
                battle_hard.volume = 1 - Mathf.Clamp(gameManager.timeToOpenEgg, 0f, 1f);
            }
        }
    }
}
