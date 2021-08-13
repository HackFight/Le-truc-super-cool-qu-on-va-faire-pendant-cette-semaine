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

    private void Update()
    {
        if (isTutoOpen && Input.GetAxis("Cancel") == 1)
        {
            tuto.Play("CloseTuto");
        }

        if (tuto.GetCurrentAnimatorStateInfo(0).IsTag("Ok"))
        {
            isTutoOpen = false;
            tutoPanel.SetActive(false);
        }
    }

    public void LoadGame()
    {
        if (!isTutoOpen)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void LoadOptions()
    {
        if (!isTutoOpen)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void GoBack()
    {
        if (!isTutoOpen)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void OpenTuto()
    {
        if (!isTutoOpen)
        {
            isTutoOpen = true;
            tuto.SetBool("isTutoOpen", true);
            tutoPanel.SetActive(true);
        }
    }
}
