using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool TrapPlacementMode = true;
    public Transform StartTransform;

    [SerializeField] private float _placementSpeed = 10f;
    [SerializeField] private float _gameMoveSpeed = 3f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float maxVelocity = 10f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


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
    }

    private void PlacementMovement()
    {
        //Handles movement during placement phase
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
        //Handles movement during game phase
        //Needs to be called from fixed update as rigidbody movement is used
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector2(_gameMoveSpeed, 0), ForceMode2D.Impulse);
        }
        if(Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector2(-_gameMoveSpeed, 0), ForceMode2D.Impulse);
        }
        if(Input.GetKey(KeyCode.W) && IsGrounded())
        {
            //Jump
            rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if(!TrapPlacementMode)
        {
            if(maxVelocity > rb.velocity.magnitude)
            {
                //Check whether velocity is at maximum to prevent player getting too fast
                GameMovement();
            }
            else
            {
                //Ensures only magnitude of velocity is changed not direction
                rb.velocity = rb.velocity.normalized * maxVelocity;
            }
            
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void StartGamePhase()
    {
        //Enables rigidbody and spawns player at start position
        rb.isKinematic = false;
        TrapPlacementMode = false;
        ResetToStart();
    }

    public void StartPlacementPhase()
    {
        TrapPlacementMode = true;
        rb.isKinematic = true;
    }

    public void ResetToStart()
    {
        transform.position = StartTransform.position;
        rb.velocity = new Vector3(0, 0, 0);
    }
}
