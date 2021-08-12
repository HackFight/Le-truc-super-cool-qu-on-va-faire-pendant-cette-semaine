using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject tutoPanel;
    private bool isTutoOpen;

    void Start()
    {
        
        
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
        isTutoOpen = true;
        tutoPanel.SetActive(true);
    }
}
