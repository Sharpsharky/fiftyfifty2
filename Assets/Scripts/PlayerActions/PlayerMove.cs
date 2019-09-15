using Inputs;
using UnityEngine;

[RequireComponent( typeof( PInput ) )]
public class PlayerMove : MonoBehaviour, IStickListener, IButtonListener
{
    [SerializeField] private float moveSpeed = 7.4f;
    [SerializeField] private float jumpPower = 200f;

    private Rigidbody2D rb;
    private bool isGrounded = true;
    private PlayerGrab playerGrab;

    public void Awake()
    {
        playerGrab = GetComponent<PlayerGrab>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        SpeedUpFalling();
    }

    public void OnStickChange(JoystickDoubleAxis axis)
    {

    }

    public void SpeedUpFalling()
    {
        if (rb.velocity.y < 0) {
            rb.velocity = new Vector2( rb.velocity.x, rb.velocity.y * 1.09f );
        }
    }

    public void OnStickHold(JoystickDoubleAxis axis)
    {
        if (axis.Code == AxisCode.LeftStick && playerGrab.isGrabbingHair != true) {
            float x = axis.GetAxisX();
            transform.position = transform.position + new Vector3( x * Time.deltaTime * MoveSpeed, 0 );
        }
    }

    public void OnStickDeadZone(JoystickDoubleAxis axis) { }


    void Jump()
    {
        if (!isGrounded) {
            return;
        }

        rb.AddForce( transform.up * jumpPower );
        isGrounded = false;
        gameObject.transform.parent = null;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (playerGrab.isGrabbingHair == false) {
            if (col.gameObject.tag.Equals( "Ground" )) {

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
