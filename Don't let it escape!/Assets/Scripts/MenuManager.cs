using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TMP_Text winText;

    public void Play()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().StartGame();
        GameObject.Find("GameManager").GetComponent<AudioManager>().Play("Select");
    }

    public void Quit()
    {
        GameObject.Find("GameManager").GetComponent<AudioManager>().Play("Select");
        Application.Quit();
        Debug.Log("Quit");
    }

    public void LoadMainMenu()
    {
        GameObject.Find("GameManager").GetComponent<AudioManager>().Play("Select");
        SceneManager.LoadScene("MainMenu");
    }

    public void UpdateWinText(string winningPlayer)
    {
        winText.text = "Player " + winningPlayer + " wins!";
    }
}
