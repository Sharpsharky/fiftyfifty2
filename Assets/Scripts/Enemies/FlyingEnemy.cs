using UnityEngine;

public abstract class FlyingEnemy : MonoBehaviour
{

    protected Rigidbody2D rb;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public abstract void StartMoving();
    public abstract void StopMoving();

    public bool ShouldDisapear()
    {
        if (Direction == -1 && GameManager.CameraXBound.Left - 3.5f >= transform.position.x)
            return true;
        return GameManager.CameraXBound.Right + 3.5f <= transform.position.x;
    }


    public int Direction { get; set; } = 1;
    public float MoveSpeed { get; set; } = 7.5f;
}
