using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource battle_intro;
    public AudioSource battle_soft;
    public AudioSource battle_hard;
    public AudioSource battle_golem;
    public AudioSource walkcycle_run_1;
    public AudioSource walkcycle_run_2;
    public AudioSource walkcycle_run_3;
    public AudioSource walkcycle_run_4;

    public List<AudioSource> shoots;
    public List<AudioSource> dashes1;
    public List<AudioSource> dashes2;
    public List<AudioSource> dashes3;
    public List<AudioSource> dashes4;

    private GameManager gameManager;
    private PlayerManager playerManager;
    private Egg eggScript;
    private PlayerMovement playerMovement;
    public DataBase dataBase;

    public bool isMusicActive = true;

    int statePlay = 0;
    void Start()
    {
        statePlay = 0;

        playerManager = FindObjectOfType<PlayerManager>();
        gameManager = FindObjectOfType<GameManager>();
        eggScript = FindObjectOfType<Egg>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        dataBase = FindObjectOfType<DataBase>();

        isMusicActive = dataBase.isMusicActive;

        if (isMusicActive)
        {
            battle_intro.Play();
        }
    }
    void Update()
    {
        isMusicActive = dataBase.isMusicActive;

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
                //battle_soft.volume = Mathf.Clamp(gameManager.timeToOpenEgg, 0f, 1f);
                //battle_hard.volume = 1 - Mathf.Clamp(gameManager.timeToOpenEgg, 0f, 1f);
                
            }
        }
    }

    public void PlayShoot()
    {
        int index = Random.Range(0, shoots.Count);

        shoots[index].Play();
    }
    public void PlayDash(int playerId)
    {
        if (playerId == 0)
        {
            int index = Random.Range(0, dashes1.Count);

            dashes1[index].Play();
        }
        else if (playerId == 1)
        {
            int index = Random.Range(0, dashes2.Count);

            dashes2[index].Play();
        }
        else if (playerId == 2)
        {
            int index = Random.Range(0, dashes3.Count);

            dashes3[index].Play();
        }
        else if (playerId == 3)
        {
            int index = Random.Range(0, dashes4.Count);

            dashes4[index].Play();
        }
        
    }
    public void Run(int playerId)
    {
        if (playerId == 0)
        {
            walkcycle_run_1.Play();
        }
        else if (playerId == 1)
        {
            walkcycle_run_2.Play();
        }
        else if (playerId == 2)
        {
            walkcycle_run_3.Play();
        }
        else if (playerId == 3)
        {
            walkcycle_run_4.Play();
        }
    }
    public void StopRun(int playerId)
    {
        if (playerId == 0)
        {
            walkcycle_run_1.Stop();
        }
        else if (playerId == 1)
        {
            walkcycle_run_2.Stop();
        }
        else if (playerId == 2)
        {
            walkcycle_run_3.Stop();
        }
        else if (playerId == 3)
        {
            walkcycle_run_4.Stop();
        }
    }
    public bool IsRunning(int playerId)
    {
        if (playerId == 0)
        {
            return walkcycle_run_1.isPlaying;
        }
        else if (playerId == 1)
        {
            return walkcycle_run_2.isPlaying;
        }
        else if (playerId == 2)
        {
            return walkcycle_run_3.isPlaying;
        }
        else if (playerId == 3)
        {
            return walkcycle_run_4.isPlaying;
        }

        return true;
    }
}
