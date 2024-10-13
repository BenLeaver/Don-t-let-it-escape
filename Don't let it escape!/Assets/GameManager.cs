using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int CurrentPlayer = 1;
    public GameObject TrapUI;

    // Start is called before the first frame update
    void Start()
    {
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
        TrapUI.GetComponent<Canvas>().enabled = true;
        GameObject.Find("Player").GetComponent<PlayerManager>().StartPlacementPhase();
        TrapUI.GetComponent<TrapManager>().TrapPanel.SetActive(true);
    }

    public void EndTrapPlacementPhase()
    {
        TrapUI.GetComponent<Canvas>().enabled = false;
        GameObject.Find("Player").GetComponent<PlayerManager>().StartGamePhase();
    }
}
