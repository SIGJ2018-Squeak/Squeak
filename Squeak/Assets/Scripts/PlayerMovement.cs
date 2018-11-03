using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float movementSpeedUpRate;
    public float movementCoolDownRateGrounded;
    private float _horizontalVelocity;

    public float jumpSpeed;
    private float _verticalVelocity;

    public LayerMask isGround;
    public float groundCheckRadius;
    public Transform groundDetector;
    private bool _onGround;

    public bool gravityReversed;
    public float gravity;
    public float restingGravityVelocity;
    public float terminalVelocity;

    private float _facingX;
    private float _facingY;

    // Use this for initialization
    void Start()
    {
        _facingX = 1f;
        _facingY = -1f;
    }

    // Update is called once per frame
    void Update()
    {
        CollisionCheck();
        Move();
    }

    private void CollisionCheck()
    {
        _onGround = Physics2D.OverlapCircle(groundDetector.position, groundCheckRadius, isGround);
    }

    private void Move()
    {
        // horizontal
        if (Input.GetAxis("Horizontal") > 0f)
        {
            _horizontalVelocity = Mathf.Min(_horizontalVelocity + movementSpeedUpRate, movementSpeed);

            // face forward
            _facingX = 1f;
        }
        else if (Input.GetAxis("Horizontal") < 0f)
        {
            _horizontalVelocity = Mathf.Max(_horizontalVelocity - movementSpeedUpRate, -movementSpeed);

            // face backwards
            _facingX = -1f;
        }
        else
        {
            if (_horizontalVelocity > 0)
            {
                _horizontalVelocity = Mathf.Max(_horizontalVelocity - movementCoolDownRateGrounded, 0f);
            }
            else if (_horizontalVelocity < 0)
            {
                _horizontalVelocity = Mathf.Min(_horizontalVelocity + movementCoolDownRateGrounded, 0f);
            }
        }

        // vertical
        if (_onGround && Input.GetButtonDown("Jump"))
        {
            if(gravityReversed)
            {
                _verticalVelocity = -jumpSpeed;
            }
            else
            {
                _verticalVelocity = jumpSpeed;
            }

        }
        else if (gravityReversed)
        {
            if(_onGround)
            {
                _verticalVelocity = restingGravityVelocity;
            }
            else
            {
                _verticalVelocity = Mathf.Min(_verticalVelocity += gravity, terminalVelocity);
            }

            // upside down
            _facingY = -1f;
        }
        else
        {
            if(_onGround)
            {
                _verticalVelocity = -restingGravityVelocity;
            }
            else 
            {
                _verticalVelocity = Mathf.Max(_verticalVelocity -= gravity, -terminalVelocity);
            }

            // rightside up
            _facingY = 1f;
        }

        // orient
        transform.localScale = new Vector3(_facingX, _facingY, 1f);

        // move
        transform.Translate(1f * Time.deltaTime * _horizontalVelocity, 1f * Time.deltaTime * _verticalVelocity, 0f);
    }
}
