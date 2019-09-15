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
    private Vector3 startScale;
    private Animator _animator;
    private BoxCollider2D coll;
    private float cameraMiddleY;
    private PlayerAudio playerAudio;

    private bool isJumping = false, iswalking = false, isIdling = false;

    public void Awake()
    {
        _animator = GetComponent<Animator>();
        playerGrab = GetComponent<PlayerGrab>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        playerAudio = GetComponent<PlayerAudio>();
        startScale = transform.localScale;
    }

    public void Start()
    {
    }

    public void Update()
    {
        SpeedUpFalling();
        if (transform.position.y > GameManager.CameraYBound.Right-0.5f ) {
            float dy = transform.position.y - GameManager.CameraYBound.Right;
            GameManager.Tower.MoveY(-dy * Time.deltaTime);
        }
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

            if (isJumping == false) {
                if (iswalking == false) {
                    if (isGrounded) {
                        _animator.SetTrigger( "Walk" );
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

    public void OnStickDeadZone(JoystickDoubleAxis axis)
    {
        if (axis.Code == AxisCode.LeftStick) {
            if (isJumping == false) {
                if (isIdling == false) {
                    Debug.Log( "StartIdle" );
                    _animator.SetTrigger( "Idle" );
                    isIdling = true;
                    iswalking = false;

                }
            }
        }

    }


    void Jump()
    {
        if (!isGrounded) {
            return;
        }
        isJumping = true;
        iswalking = false;
        isIdling = false;

        _animator.SetTrigger( "Jump" );
        rb.AddForce( transform.up * jumpPower );
        isGrounded = false;
        ChangeParent( null );
        playerAudio.PlayJumpShot();
    }

    public bool IsOnGround()
    {
        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll( transform.position, Vector2.down, coll.bounds.extents.y );
        // you can increase RaycastLength and adjust direction for your case
        foreach (var hited in hit) {
            if (hited.collider.gameObject == gameObject) //Ignore my character
                continue;
            // Don't forget to add tag to your ground
            if (hited.collider.gameObject.tag == "Ground") { //Change it to match ground tag
                return true;
            }
        }
        return false;
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

                if (isJumping == true)
                    isJumping = false;
                ChangeParent( col.gameObject.transform );
                transform.eulerAngles = new Vector3( 0, 0, 0 );
                playerGrab.isChangingRotation = false;
                isGrounded = true;
            }
        }
    }

    public void ChangeParent(Transform parent)
    {
        transform.parent = parent;
        transform.localScale = startScale;
    }

    public void OnButtonPressed(ButtonCode code)
    {
        if (code == ButtonCode.A && playerGrab.isGrabbingHair != true) {
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
