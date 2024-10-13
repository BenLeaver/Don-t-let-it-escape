using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool TrapPlacementMode = true;
    public Transform StartTransform;

    [SerializeField]
    private float _placementSpeed = 10f;
    [SerializeField]
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        StartTransform = GameObject.Find("Start").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(TrapPlacementMode)
        {
            PlacementMovement();
        }
        else
        {
            GameMovement();
        }
    }

    private void PlacementMovement()
    {
        //Player should be unaffected by gravity and should be able to move freely around the level
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, _placementSpeed * Time.deltaTime, 0);
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, _placementSpeed * -Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(_placementSpeed * -Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(_placementSpeed * Time.deltaTime, 0, 0);
        }
    }

    private void GameMovement()
    {
        //Add game movement code here including the ability to jump
        //Probably best to use rigidbody movement
    }

    public void StartGamePhase()
    {
        //Enables rigidbody and spawns player at start position
        rb.isKinematic = false;
        TrapPlacementMode = false;
        transform.position = StartTransform.position;
    }

    public void StartPlacementPhase()
    {
        TrapPlacementMode = true;
        rb.isKinematic = true;
    }
}
