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
    private ParticleSystem sonicParticle;

    public float jumpSpeed;
    private float _verticalVelocity;

    public LayerMask isGround;
    public float groundCheckRadius;
    public Transform groundDetector;
    public Transform ceilingDetector;
    private bool _onGround;

    public bool gravityReversed;
    public float gravity;
    public float terminalVelocity;

    private bool _jumping;
    private bool _falling;
    private bool _squeaking;

    public AudioClip squeak;
    public AudioClip sonicSqueak;

    private float _facingX;
    private float _facingY;

    private Animator _animator;

    // Use this for initialization
    void Start()
    {
        _jumping = false;
        _falling = false;
        _squeaking = false;

        _facingX = 1f;
        _facingY = -1f;

        _animator = GetComponent<Animator>();
        sonicParticle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CollisionCheck();

        _squeaking = false;
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (burstAvailable)
            {
                _squeaking = true;

                burstAvailable = false;
                if (sonicSqueak != null)
                {
                    AudioSource.PlayClipAtPoint(sonicSqueak, transform.position);
                }
                if (sonicParticle != null)
                {
                    if (_facingX > 0)
                    {
                        sonicParticle.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                    }
                    else
                    {
                        sonicParticle.gameObject.transform.localScale = new Vector3(-1f, -1f, -1f);
                    }
                    sonicParticle.Play();
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
        _animator.SetBool("PlayerSqueaking", _squeaking);
    }

    private void CollisionCheck()
    {
        if(_falling && !_jumping)
        {
            bool touchingGround = Physics2D.OverlapCircle(groundDetector.position, groundCheckRadius, isGround);
            bool touchingCeiling = Physics2D.OverlapCircle(ceilingDetector.position, groundCheckRadius, isGround);

            _onGround = touchingGround || touchingCeiling;
        }
        else
        {
            _onGround = Physics2D.OverlapCircle(groundDetector.position, groundCheckRadius, isGround);
        }
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
            _jumping = true;

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
                _jumping = false;
                _falling = false;

                _verticalVelocity = 0f;

                // upside down
                _facingY = -1f;
            }
            else
            {
                _falling = true;

                // if mouse just falling must revese mouse
                if (!_jumping)
                {
                    _facingY = 1f;
                } else 
                {
                    // upside down
                    _facingY = -1f;
                }

                _verticalVelocity = Mathf.Min(_verticalVelocity += gravity, terminalVelocity);
            }
        }
        else
        {
            if (_onGround)
            {
                _jumping = false;
                _falling = false;

                _verticalVelocity = 0f;

                // rightside up
                _facingY = 1f;
            }
            else
            {
                _falling = true;

                // if mouse just falling must revese mouse
                if (!_jumping)
                {
                    _facingY = -1f;
                }
                else
                {
                    // rightside up
                    _facingY = 1f;
                }

                _verticalVelocity = Mathf.Max(_verticalVelocity -= gravity, -terminalVelocity);
            }
        }

        _animator.SetFloat("PlayerXSpeed", Mathf.Abs(_horizontalVelocity));
        _animator.SetBool("PlayerJumping", _jumping);
        _animator.SetBool("PlayerFalling", _falling);

        // orient
        transform.localScale = new Vector3(_facingX, _facingY, 1f);

        // move
        transform.Translate(1f * Time.deltaTime * _horizontalVelocity, 1f * Time.deltaTime * _verticalVelocity, 0f);
    }

    public void SpeedBurst()
    {
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

    public void ReverseGravity()
    {
        _jumping = false;
        _falling = true;

        gravityReversed = !gravityReversed;
    }
}
