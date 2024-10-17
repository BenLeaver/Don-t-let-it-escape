using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        //Update this if adding random scene selection
        SceneManager.LoadScene("Scene1");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
