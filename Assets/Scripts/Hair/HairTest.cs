using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairTest : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = Vector2.right * Time.deltaTime * 100 * speed;
    }
}
