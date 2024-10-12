using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool TrapPlacementMode = true;

    [SerializeField]
    private float _placementSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
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
        
    }
}
