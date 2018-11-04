using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float movementSpeedUpRate;
    public float movementCoolDownRateGrounded;
    public float burstSpeed;
    public bool burstAvailable = false;
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

    public AudioClip squeak;
    public AudioClip sonicSqueak;

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (burstAvailable)
            {
                burstAvailable = false;
                if (sonicSqueak != null)
                {
                    AudioSource.PlayClipAtPoint(sonicSqueak, transform.position);
                }
                SpeedBurst();

            }
            else
            {
                if (squeak != null)
                {
                    AudioSource.PlayClipAtPoint(squeak, transform.position);
                }
            }
           
        }
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

    public void SpeedBurst()
    {
        Debug.Log("SpeedBurst");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(-1f* _facingX, 0f), burstSpeed * Time.deltaTime * movementSpeed, isGround);
        
        if (hit.collider == null)
        {

            transform.Translate(-1f* burstSpeed * Time.deltaTime * movementSpeed * _facingX, 0f, 0f);
        }
        else
        {
            float travelDistance;
            float colliderWidth = GetComponent<BoxCollider2D>().size.x;
            if (_facingX < 0)
            {
                travelDistance = hit.point.x - (colliderWidth/2);
            }
            else
            {
                travelDistance = hit.point.x + (colliderWidth / 2);
            }
            
            transform.position = new Vector3(travelDistance,transform.position.y,transform.position.z);
        }
    }
}
