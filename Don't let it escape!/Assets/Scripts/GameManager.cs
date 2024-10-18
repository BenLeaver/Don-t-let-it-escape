using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int TrapPlayer = 1; //Refers to which player is placing traps this round
    public int GamePlayer = 2; //Refers to which player is playing the game this round
    public GameObject TrapUI;
    public GameObject TimeUI; 
    public float gameTimer = 30f;
    public float maxGameTime = 30f;
    public bool inGame = false;
    private TMP_Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioManager>().Play("Music1");
        if(SceneManager.GetActiveScene().name == "Scene1" || SceneManager.GetActiveScene().name == "Scene2")
        {
            //For testing
            StartCoroutine(LoadGame());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inGame)
        {
            gameTimer -= Time.deltaTime;
            int seconds = (int) Mathf.Ceil(gameTimer);
            timeText.text = seconds.ToString();
            if(gameTimer <= 0f)
            {
                GameLost();
            }
        }
    }

    public void StartGame()
    {
        int rInt = Random.Range(1, 3); //1 to 2
        string sceneName = "Scene" + rInt.ToString();
        SceneManager.LoadScene(sceneName);
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        //need to wait for a frame for the scene to load
        yield return null;
        TimeUI = GameObject.Find("TimeCanvas");
        timeText = GameObject.Find("TimeCanvas/Time").GetComponent<TMP_Text>();
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
        TimeUI.GetComponent<Canvas>().enabled = false;
        GameObject.Find("Player").GetComponent<PlayerManager>().StartPlacementPhase();
        TrapUI.GetComponent<TrapManager>().TrapPanel.SetActive(true);
        TrapUI.GetComponent<TrapManager>().UpdateTrapText("Player " + TrapPlayer.ToString() + "'s turn to place a trap");
        GameObject.Find("Start").GetComponent<SpriteRenderer>().enabled = true;
    }

    public void EndTrapPlacementPhase()
    {
        inGame = true;
        GetComponent<AudioManager>().Play("TrapPlace");
        GetComponent<AudioManager>().Stop("Music2");
        GetComponent<AudioManager>().Play("Music4");
        TrapUI.GetComponent<Canvas>().enabled = false;
        TimeUI.GetComponent<Canvas>().enabled = true;
        GameObject.Find("Start").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Player").GetComponent<PlayerManager>().StartGamePhase();
        gameTimer = maxGameTime;
    }

    public void RoundComplete()
    {
        inGame = false;
        GetComponent<AudioManager>().Play("Win");
        GameObject.Find("Player").GetComponent<PlayerManager>().ResetToStart();
        int temp = TrapPlayer;
        TrapPlayer = GamePlayer;
        GamePlayer = temp;
        StartTrapPlacementPhase();
    }

    public void GameLost()
    {
        inGame = false;
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
