using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool TrapPlacementMode = true;
    public Transform StartTransform;

    [SerializeField] private float _placementSpeed = 10f;
    [SerializeField] private float _gameMoveSpeed = 2.5f;
    [SerializeField] private float _jumpForce = 4f;
    [SerializeField] private float maxVelocity = 10f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpInterval = 3f;

    private float jumpTime;
    private bool canJump;


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
        if (!canJump)
        {
            if(jumpTime >= jumpInterval)
            {
                canJump = true;
                jumpTime = 0f;
            }
            jumpTime += Time.deltaTime;
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
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(_placementSpeed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void GameMovement()
    {
        //Handles movement during game phase
        //Needs to be called from fixed update as rigidbody movement is used
        if (Input.GetKey(KeyCode.D) && maxVelocity > rb.velocity.x)
        {
            rb.AddForce(new Vector2(_gameMoveSpeed, 0), ForceMode2D.Impulse);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if(Input.GetKey(KeyCode.A) && -maxVelocity < rb.velocity.x)
        {
            rb.AddForce(new Vector2(-_gameMoveSpeed, 0), ForceMode2D.Impulse);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && IsGrounded() && canJump)
        {
            //Jump
            canJump = false;
            GameObject.Find("GameManager").GetComponent<AudioManager>().Play("Jump");
            rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
           
        }
    }

    private void FixedUpdate()
    {
        if(!TrapPlacementMode)
        {
            //Check whether velocity is at maximum to prevent player getting too fast
            GameMovement();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }

    public void StartGamePhase()
    {
        //Enables rigidbody and spawns player at start position
        rb.isKinematic = false;
        TrapPlacementMode = false;
        ResetToStart();
        //Resetting transparency
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 1f;
        GetComponent<SpriteRenderer>().color = tmp;
    }

    public void StartPlacementPhase()
    {
        TrapPlacementMode = true;
        rb.isKinematic = true;
        //Making sprite transparent
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 0.5f;
        GetComponent<SpriteRenderer>().color = tmp;
    }

    public void ResetToStart()
    {
        transform.position = StartTransform.position;
        rb.velocity = new Vector3(0, 0, 0);
    }
}
