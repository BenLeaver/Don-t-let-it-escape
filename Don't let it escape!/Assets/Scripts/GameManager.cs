using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int TrapPlayer = 1; //Refers to which player is placing traps this round
    public int GamePlayer = 2; //Refers to which player is playing the game this round
    public GameObject TrapUI;

    // Start is called before the first frame update
    void Start()
    {
        TrapUI = GameObject.Find("TrapUICanvas");
        StartTrapPlacementPhase();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateLevel()
    {
        //Add level generation code here

    }

    public void StartTrapPlacementPhase()
    {
        Debug.Log("Player " + TrapPlayer + " turn"); //TODO: Make this display as UI text
        TrapUI.GetComponent<Canvas>().enabled = true;
        GameObject.Find("Player").GetComponent<PlayerManager>().StartPlacementPhase();
        TrapUI.GetComponent<TrapManager>().TrapPanel.SetActive(true);
        GameObject.Find("Start").GetComponent<SpriteRenderer>().enabled = true;
    }

    public void EndTrapPlacementPhase()
    {
        TrapUI.GetComponent<Canvas>().enabled = false;
        GameObject.Find("Start").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Player").GetComponent<PlayerManager>().StartGamePhase();
    }

    public void RoundComplete()
    {
        GameObject.Find("Player").GetComponent<PlayerManager>().ResetToStart();
        int temp = TrapPlayer;
        TrapPlayer = GamePlayer;
        GamePlayer = temp;
        StartTrapPlacementPhase();
    }

    public void GameLost()
    {
        //TODO: Make this display as UI
        Debug.Log("Player" + TrapPlayer + " wins!");
    }
}
