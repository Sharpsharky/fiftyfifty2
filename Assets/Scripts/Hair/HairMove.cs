using Inputs;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HairMove : MonoBehaviour, IStickListener, IButtonListener
{
    [SerializeField] private float yMaxPosDeviation = 7.1f;
    [SerializeField] private float hairMoveSpeed = 9.8f;
    [SerializeField] private float towerMoveSpeed = 5f;
    private float yMaxPos, yMinPos;

    private Rigidbody2D hairRB;
    private Transform hairBone;
    private Tower tower;

    bool canGoLeft = true, canGoRight = true, canGoUp = true, canGoDown = true;

    public void Awake()
    {
        hairBone = transform.Find( "bone_1" );
        hairRB = hairBone.GetComponent<Rigidbody2D>();
        GetComponent<PInput>().PlayerNumber = 1;
    }

    public void Start()
    {
        yMinPos = Camera.main.ScreenToWorldPoint( new Vector3( 0, Screen.height, 0 ) ).y + 0.1f;
        yMaxPos = yMinPos + yMaxPosDeviation;
    }

    public void OnStickChange(JoystickDoubleAxis stick)
    {
    }

    public void OnStickDeadZone(JoystickDoubleAxis stick)
    {
        if (stick.Code == AxisCode.LeftStick) {
            if (hairRB != null)
                hairRB.velocity = new Vector2( 0, 0 );
        }
    }

    public void OnStickHold(JoystickDoubleAxis stick)
    {
        if (stick.Code == AxisCode.LeftStick && hairRB != null) {
            if (stick.X > 0) {
                if (canGoRight)
                    hairRB.velocity = new Vector2( stick.X * Time.deltaTime * hairMoveSpeed, hairRB.velocity.y );
                else {
                    hairRB.velocity = new Vector2( 0, hairRB.velocity.y );

                }
            } else {
                if (canGoLeft)
                    hairRB.velocity = new Vector2( stick.X * Time.deltaTime * hairMoveSpeed, hairRB.velocity.y );
                else {
                    hairRB.velocity = new Vector2( 0, hairRB.velocity.y );

                }
            }


            if (stick.Y > 0) {
                if (canGoUp)
                    hairRB.velocity = new Vector2( hairRB.velocity.x, stick.Y * Time.deltaTime * hairMoveSpeed );
                else {
                    hairRB.velocity = new Vector2( hairRB.velocity.x, 0 );

                }
            } else {
                if (canGoDown)
                    hairRB.velocity = new Vector2( hairRB.velocity.x, stick.Y * Time.deltaTime * hairMoveSpeed );
                else {
                    hairRB.velocity = new Vector2( hairRB.velocity.x, 0 );

                }
            }
        } else if (stick.Code == AxisCode.RightStick) {
            tower.MoveX( -stick.X * Time.deltaTime * towerMoveSpeed );
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hairBone != null) {
            if (hairBone.position.x >= GameManager.CameraXBound.Right) {
                canGoRight = false;
            } else {
                canGoRight = true;
            }

            if (hairBone.position.x <= GameManager.CameraXBound.Left) {
                canGoLeft = false;
            } else {
                canGoLeft = true;
            }


            if (hairBone.position.y >= yMaxPos) {
                canGoUp = false;
            } else if (hairBone.position.y < yMaxPos) {
                canGoUp = true;
            }
            if (hairBone.position.y <= yMinPos) {
                canGoDown = false;
            } else if (hairBone.position.y > yMinPos) {
                canGoDown = true;

            }
        }

    }

    public void OnButtonPressed(ButtonCode code)
    {
        if ( code == ButtonCode.Back ) {
            SceneManager.LoadScene("the_last_one");
        }
    }

    public void OnButtonReleased(ButtonCode code)
    {
       
    }

    public void OnButtonHeld(ButtonCode code)
    {
       
    }

    public Tower Tower { private get => tower; set => tower = value; }
}
