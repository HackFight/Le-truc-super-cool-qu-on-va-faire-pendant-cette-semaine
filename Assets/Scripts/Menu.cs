using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject tutoPanel;
    private bool isTutoOpen;
    public Animator tuto;

    public GameObject optionsPanel;
    private bool isOptionOpen;
    public Animator options;

    public DataBase dataBase;

    public bool isMusicActive = true;

    private void Update()
    {
        dataBase.isMusicActive = isMusicActive;

        if (isTutoOpen && Input.GetAxis("Cancel") == 1)
        {
            tuto.Play("CloseTuto");
        }

        if (tuto.GetCurrentAnimatorStateInfo(0).IsTag("Ok"))
        {
            isTutoOpen = false;
            tutoPanel.SetActive(false);
        }

        if (isOptionOpen && Input.GetAxis("Cancel") ==  1)
        {
            options.Play("CloseOption");
        }

        if (options.GetCurrentAnimatorStateInfo(0).IsTag("Ok"))
        {
            isOptionOpen = false;
            optionsPanel.SetActive(false);
        }
    }

    public void LoadGame()
    {
        if (!isTutoOpen && !isOptionOpen)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void OpenOptions()
    {
        if (!isTutoOpen && !isOptionOpen)
        {
            isOptionOpen = true;
            optionsPanel.SetActive(true);
        }
    }

    public void GoBack()
    {
        if (!isTutoOpen && !isOptionOpen)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void OpenTuto()
    {
        if (!isTutoOpen && !isOptionOpen)
        {
            isTutoOpen = true;
            tutoPanel.SetActive(true);
        }
    }

    public void QuitGame()
    {
        if (!isTutoOpen && !isOptionOpen)
        {
            Application.Quit();
        }
    }

    public void ToggleMusic()
    {
        if (isMusicActive)
        {
            isMusicActive = false;
        }
        else
        {
            isMusicActive = true;
        }
    }
}
