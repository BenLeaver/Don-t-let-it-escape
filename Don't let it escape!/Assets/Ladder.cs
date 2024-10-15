using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().RoundComplete();
        }
    }
}
