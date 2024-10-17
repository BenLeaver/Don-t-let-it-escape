using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int TrapPlayer = 1; //Refers to which player is placing traps this round
    public int GamePlayer = 2; //Refers to which player is playing the game this round
    public GameObject TrapUI;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioManager>().Play("Music1");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        //Update this if adding random scene selection
        SceneManager.LoadScene("Scene1");
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        //need to wait for a frame for the scene to load
        yield return null;
        TrapPlayer = 1;
        GamePlayer = 2;
        StartTrapPlacementPhase();
    }

    public void StartTrapPlacementPhase()
    {
        TrapUI = GameObject.Find("TrapUICanvas");
        GetComponent<AudioManager>().Stop("Music1");
        GetComponent<AudioManager>().Stop("Music4");
        GetComponent<AudioManager>().Play("Music2");
        TrapUI.GetComponent<Canvas>().enabled = true;
        GameObject.Find("Player").GetComponent<PlayerManager>().StartPlacementPhase();
        TrapUI.GetComponent<TrapManager>().TrapPanel.SetActive(true);
        TrapUI.GetComponent<TrapManager>().UpdateTrapText("Player " + TrapPlayer.ToString() + "'s turn to place a trap");
        GameObject.Find("Start").GetComponent<SpriteRenderer>().enabled = true;
    }

    public void EndTrapPlacementPhase()
    {
        GetComponent<AudioManager>().Play("TrapPlace");
        GetComponent<AudioManager>().Stop("Music2");
        GetComponent<AudioManager>().Play("Music4");
        TrapUI.GetComponent<Canvas>().enabled = false;
        GameObject.Find("Start").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Player").GetComponent<PlayerManager>().StartGamePhase();
    }

    public void RoundComplete()
    {
        GetComponent<AudioManager>().Play("Win");
        GameObject.Find("Player").GetComponent<PlayerManager>().ResetToStart();
        int temp = TrapPlayer;
        TrapPlayer = GamePlayer;
        GamePlayer = temp;
        StartTrapPlacementPhase();
    }

    public void GameLost()
    {
        GetComponent<AudioManager>().Play("Death");
        Debug.Log("Player" + TrapPlayer + " wins!");
        GetComponent<AudioManager>().Stop("Music4");
        GetComponent<AudioManager>().Play("Music1");
        SceneManager.LoadScene("EndMenu");
        StartCoroutine(LoadEndScreen());
    }

    IEnumerator LoadEndScreen()
    {
        yield return null;
        GameObject.Find("Canvas").GetComponent<MenuManager>().UpdateWinText(TrapPlayer.ToString());
    }
}
