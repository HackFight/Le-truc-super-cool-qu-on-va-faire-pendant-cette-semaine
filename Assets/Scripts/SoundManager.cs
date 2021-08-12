using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource battle_intro;
    public AudioSource battle_soft;
    public AudioSource battle_hard;
    public AudioSource battle_golem;

    public GameManager gameManager;
    int statePlay = 0;
    void Start()
    {
        battle_intro.Play();
    }
    void Update()
    {
        if (battle_intro.isPlaying == false && statePlay == 0)
        {
            battle_soft.Play();
            battle_hard.Play();
            statePlay ++;
        }
        else if (gameManager.chrono >= gameManager.timeToOpenEgg && statePlay == 1)
        {
            battle_soft.Stop();
            battle_hard.Stop();
            battle_golem.Play();
            statePlay ++;
        }
        if (statePlay == 1)
        {
            battle_soft.volume = Mathf.Clamp(gameManager.chrono, 0f, 1f);
            battle_hard.volume = 1 - Mathf.Clamp(gameManager.chrono, 0f, 1f);
        }
    }
}
