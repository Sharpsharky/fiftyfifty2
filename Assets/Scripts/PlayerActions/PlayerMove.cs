using Inputs;
using UnityEngine;

[RequireComponent( typeof( PInput ) )]
public class PlayerMove : MonoBehaviour, IStickListener, IButtonListener
{
    [SerializeField] private float moveSpeed = 7.4f;
    [SerializeField] private float jumpPower = 200f;

    public LayerMask groundLayers;

    private Rigidbody2D rb;
    private bool isGrounded = true;
    private PlayerGrab playerGrab;
    private Vector2 startScale;
    private Animator _animator;
    private BoxCollider2D coll;

    private bool isJumping = false, iswalking = false,  isIdling = false;

    public void Awake()
    {
        _animator = GetComponent<Animator>();
        playerGrab = GetComponent<PlayerGrab>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        startScale = transform.localScale;
    }

    public void Update()
    {
      //  Debug.Log("isGrounded:" + isGrounded);

        SpeedUpFalling();

   //     Debug.Log("isJumping:"+ isJumping + " isIdling " + isIdling+ " iswalking " + iswalking);


    }

    public void OnStickChange(JoystickDoubleAxis axis)
    {

    }

    public void SpeedUpFalling()
    {
        if (rb.velocity.y < 0) {
            rb.velocity = new Vector2( rb.velocity.x, rb.velocity.y * 1.04f );
        }
    }

    public void OnStickHold(JoystickDoubleAxis axis)
    {
        if (axis.Code == AxisCode.LeftStick) {
            isIdling = false;

            if (isJumping == false)
            {
                if (iswalking == false)
                {
                    if (isGrounded)
                    {
                        _animator.SetTrigger("Walk");
                        iswalking = true;
                    }
                }
            }
            if (playerGrab.isGrabbingHair != true) {
                float x = axis.GetAxisX();

                rb.velocity = new Vector2( 0, rb.velocity.y );
                transform.position = transform.position + new Vector3( x * moveSpeed * Time.deltaTime, 0 );

            }
        }
    }

    public void OnStickDeadZone(JoystickDoubleAxis axis) {
        if (axis.Code == AxisCode.LeftStick)
        {
            if (isJumping == false)
            {
                if (isIdling == false)
                {
                    Debug.Log("StartIdle");
                    _animator.SetTrigger("Idle");
                    isIdling = true;
                    iswalking = false;

                }
            }
        }

    }


    void Jump()
    {

        if (!isGrounded ) {
            return;
        }
        isJumping = true;
        iswalking = false;
        isIdling = false;

        _animator.SetTrigger("Jump");
        rb.AddForce( transform.up * jumpPower );
        isGrounded = false;
        gameObject.transform.parent = null;
    }

    public virtual bool IsTouchingGround()
    {
        return Physics.CheckBox( new Vector3( coll.bounds.center.x, coll.bounds.min.y, coll.bounds.center.z ),
            new Vector3( coll.bounds.extents.x * 0.85f, coll.bounds.extents.y * 0.1f, coll.bounds.extents.z * 0.85f ),
            coll.transform.rotation, groundLayers );
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (playerGrab.isGrabbingHair == false) {
            if (col.gameObject.tag.Equals( "Ground" )) {

                if (isJumping == true) isJumping = false; 
                gameObject.transform.parent = col.gameObject.transform;
                transform.eulerAngles = new Vector3( 0, 0, 0 );
                playerGrab.isChangingRotation = false;
                isGrounded = true;

            }
        }
    }

    public void OnButtonPressed(ButtonCode code)
    {
        if ( code == ButtonCode.A  && playerGrab.isGrabbingHair != true) {
            Jump();
        }
    }

    public void OnButtonReleased(ButtonCode code)
    {
        
    }

    public void OnButtonHeld(ButtonCode code)
    {
        
    }

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float JumpPower { get => jumpPower; set => jumpPower = value; }
}
